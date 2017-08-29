using System;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

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
                Debug.Write(ex.StackTrace);
                return -1;
            }
        }
    }
}