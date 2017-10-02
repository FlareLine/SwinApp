using System;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace SwinApp.Library.Analytics
{
    public static class Analytics
    {
        /// <summary>
        /// The endpoint the Analytics data will be sent to
        /// </summary>
        private const string POST_ENDPOINT = "http://localhost:3000/analytics";

        /// <summary>
        /// The threshold count of logs that needs to be reached before the system
        /// posts the Analytics data to the server
        /// </summary>
        private const int LOG_THRESHOLD = 5;

        /// <summary>
        /// The filepath for the analytics database
        /// </summary>
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SwinAppAnalytics.db");

        /// <summary>
        /// The database connection
        /// </summary>
        private static SQLiteAsyncConnection conn;

        /// <summary>
        /// If true, analytics data will attempt to be sent on the startup of the application
        /// </summary>
        public const bool DELIVER_ON_STARTUP = false;

        /// <summary>
        /// Asynchronously logs an AppEvent in the SQLite database
        /// </summary>
        /// <param name="e">AppEvent to log</param>
        /// <returns>Number of rows added to database</returns>
        /// <remarks>Should return 1 for most calls</remarks>
        public static async Task<int> LogEventAsync(AppEvent e)
        {
            try
            {
                await SwinDB.ConnAsync.CreateTableAsync<AppEvent>();

                Debug.Write($"PATH: {path}");

                Debug.Write($"Inserted new {e.Type} event!");

                return await conn.InsertAsync(e);
            }
            catch (Exception ex)
            {
                Debug.Write("Couldn't insert event!");
                Debug.Write(ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// RetrieveLog wrapper without return limit
        /// </summary>
        /// <returns>List of all AppEvents stored in database</returns>
        public static async Task<List<AppEvent>> RetrieveLogAsync() => await RetrieveLogAsync(null);

        /// <summary>
        /// RetrieveLog wrapper with a return length limit
        /// </summary>
        /// <param name="lim">Limit number of returned rows</param>
        /// <returns>Returns AppEvents up to the specified limit, or no limit if <paramref name="lim"/> is <see langword="null"/></returns>
        public static async Task<List<AppEvent>> RetrieveLogAsync(int? lim)
        {

            await SwinDB.ConnAsync.CreateTableAsync<AppEvent>();
            AsyncTableQuery<AppEvent> query = (lim == null ? conn.Table<AppEvent>() : conn.Table<AppEvent>().Where(a => a.Id < lim));

            return await query.ToListAsync();
        }

        /// <summary>
        /// Method to clear the Analytics log
        /// </summary>
        /// <returns>Success or failure</returns>
        public static async Task<int> ClearLogAsync()
        {
            return await SwinDB.ConnAsync.DeleteAllAsync(new TableMapping(typeof(AppEvent)));
        }

        /// <summary>
        /// Post the log to a valid endpoint asynchronously
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> DeliverLogAsync()
        {
            var dbContents = await SwinDB.ConnAsync.Table<AppEvent>().ToListAsync();
            bool hasSent = false;

            if (dbContents.Count >= LOG_THRESHOLD)
            {
                try
                {
                    using (var http = new HttpClient())
                    {
                        var response = await http.PostAsync(POST_ENDPOINT, new StringContent(JsonConvert.SerializeObject(dbContents)));
                        hasSent = response.IsSuccessStatusCode;
                        if (hasSent)
                            await ClearLogAsync();
                    }
                }
                catch (Exception e)
                {
                    await LogEventAsync(new AppEvent(EventType.OTHER_EVENT, DateTime.Now, $"Error Posting {dbContents.Count} logs, Exception: {e.Message}"));
                }
            }

            return hasSent;
        }
    }
}
