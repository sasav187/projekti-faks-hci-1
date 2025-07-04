using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace ProdavnicaApp
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var login = new LoginView();
            login.Show();
        }

        public static void ApplyMaterialTheme(string themeKey)
        {
            var bundledTheme = new BundledTheme();

            switch (themeKey)
            {
                case "LightPurpleLime":
                    bundledTheme.BaseTheme = BaseTheme.Light;
                    bundledTheme.PrimaryColor = PrimaryColor.DeepPurple;
                    bundledTheme.SecondaryColor = SecondaryColor.Lime;
                    break;

                case "DarkBlueAmber":
                    bundledTheme.BaseTheme = BaseTheme.Dark;
                    bundledTheme.PrimaryColor = PrimaryColor.Blue;
                    bundledTheme.SecondaryColor = SecondaryColor.Amber;
                    break;

                case "LightGreenPink":
                    bundledTheme.BaseTheme = BaseTheme.Light;
                    bundledTheme.PrimaryColor = PrimaryColor.Green;
                    bundledTheme.SecondaryColor = SecondaryColor.Pink;
                    break;
            }

            var existingTheme = Current.Resources.MergedDictionaries
                .FirstOrDefault(r => r is BundledTheme);

            if (existingTheme != null)
                Current.Resources.MergedDictionaries.Remove(existingTheme);

            Current.Resources.MergedDictionaries.Insert(0, bundledTheme);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            GC.Collect();
            GC.WaitForPendingFinalizers(); 
        }

    }
}