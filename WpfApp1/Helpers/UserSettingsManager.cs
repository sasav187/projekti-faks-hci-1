using System;
using System.IO;
using Newtonsoft.Json;

namespace ProdavnicaApp.Helpers
{
    public class UserSettings
    {
        public string PreferredTheme { get; set; }
        public string PreferredLanguage { get; set; }
    }

    public static class UserSettingsManager
    {
        // Vraća root folder gde su svi folderi projekta (npr. iznad bin/Debug/netX.X)
        private static string GetProjectRootFolderPath()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = new DirectoryInfo(baseDir);

            // Ide dva nivoa iznad (iz bin/Debug/netX.X u root projekta)
            if (directoryInfo.Parent != null && directoryInfo.Parent.Parent != null)
                return directoryInfo.Parent.Parent.FullName;

            // fallback - ako ne može, vrati current baseDir
            return baseDir;
        }

        // Vraća putanju foldera za čuvanje (možeš i izbaciti folder i direktno koristiti root)
        private static string GetSettingsFolderPath()
        {
            string rootFolder = GetProjectRootFolderPath();

            string folder = Path.Combine(rootFolder, "UserSettings");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }

        private static string GetSettingsFilePath()
        {
            var folder = GetSettingsFolderPath();

            string safeEmail = SessionManager.CurrentUser?.Email?.Replace("@", "_at_").Replace(".", "_dot_") ?? "default";

            return Path.Combine(folder, $"{safeEmail}_settings.json");
        }

        public static void Save(UserSettings settings)
        {
            if (SessionManager.CurrentUser == null) return;

            string path = GetSettingsFilePath();
            File.WriteAllText(path, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        public static UserSettings Load()
        {
            if (SessionManager.CurrentUser == null) return new UserSettings();

            string path = GetSettingsFilePath();
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(path));
            }

            return new UserSettings();
        }
    }
}
