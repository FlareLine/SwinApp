using System;
using SQLite;
using System.IO;

namespace SwinApp.Library.Analytics
{
    public static class Analytics
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SwinAppAnalytics.db");
    }
}