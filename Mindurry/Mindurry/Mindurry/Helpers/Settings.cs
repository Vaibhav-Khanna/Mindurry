// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Mindurry.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private static readonly string stringDefault = string.Empty;

        private const string Token = "Token";
        private const string userId = "UserId";
        private const string role = "Role";


        #endregion

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault(Token, stringDefault); }
            set { AppSettings.AddOrUpdateValue(Token, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(userId, stringDefault); }
            set { AppSettings.AddOrUpdateValue(userId, value); }
        }

        public static string Role
        {
            get { return AppSettings.GetValueOrDefault(role, stringDefault); }
            set { AppSettings.AddOrUpdateValue(role, value); }
        }

        const string DatabaseIdKey = "azure_database";
        static readonly int DatabaseIdDefault = 0;

        public static int DatabaseId
        {
            get { return AppSettings.GetValueOrDefault(DatabaseIdKey, DatabaseIdDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(DatabaseIdKey, value);
            }
        }

        public static int UpdateDatabaseId()
        {
            return DatabaseId++;
        }

    }
}