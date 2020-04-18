using PRBD_Framework;
using prbd_1920_g04.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace prbd_1920_g04
{
    public enum AppMessages {
        MSG_DISPLAY_MEMBER,
    }

    public partial class App : ApplicationBase {
        public static Model Model { get; private set; } = new Model();

        public static readonly string IMAGE_PATH =
            Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../images");
        public static Member CurrentUser { get; set; }

        public static void CancelChanges() {
            Model.Dispose(); // Retire de la mémoire le modèle actuel
            Model = new Model(); // Recréation d'un nouveau modèle à partir de la DB
        }
        public App() {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            ColdStart();
        }

        private void ColdStart() {
            Model.SeedData();
        }
    }
}
