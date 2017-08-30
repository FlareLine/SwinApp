using System;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace SwinApp.Library.Analytics
{
    public static class Analytics
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SwinAppAnalytics.db");

        private static SQLiteAsyncConnection conn;

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
                if (conn == null) conn = new SQLiteAsyncConnection(path);

                await conn.CreateTableAsync<AppEvent>();

                Debug.Write($"PATH: {path}");

                Debug.Write($"Inserted new {e.Type} event!");

                return await conn.InsertAsync(e);
            } catch (Exception ex)
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
        public static async Task<List<AppEvent>> RetrieveLog()
        {
            return await RetrieveLog(null);
        }

        /// <summary>
        /// RetrieveLog wrapper with a return length limit
        /// </summary>
        /// <param name="lim">Limit number of returned rows</param>
        /// <returns>Returns AppEvents up to the specified limit, or no limit if <paramref name="lim"/> is <see langword="null"/></returns>
        public static async Task<List<AppEvent>> RetrieveLog(int? lim)
        {
            if (conn == null) conn = new SQLiteAsyncConnection(path);

            AsyncTableQuery<AppEvent> query = (lim == null ? conn.Table<AppEvent>() : conn.Table<AppEvent>().Where(a => a.Id < lim));

            return await query.ToListAsync();
        }

        /// <summary>
        /// Method to clear the Analytics log
        /// </summary>
        /// <returns>Success or failure</returns>
        public static async Task<int> ClearLog()
        {
            if (conn == null) conn = new SQLiteAsyncConnection(path);

            return await conn.DeleteAllAsync(new TableMapping(typeof(AppEvent)));
        }
    }
}