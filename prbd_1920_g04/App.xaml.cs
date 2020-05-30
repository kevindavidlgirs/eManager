using PRBD_Framework;
using System;
using System.IO;
using System.Reflection;

namespace prbd_1920_g04 {
    public enum Fonction {
        Admin = 1,
        Secretary = 2,
        Coach = 3,
        Player = 4,
        Delegate = 5
    }
    public enum AppMessages {
        MSG_SHOW_MATCH,
        MSG_NEW_MATCH,
        MSG_EDIT_MATCH,
        MSG_MATCH_CHANGED,
        MSG_MATCH_DELETED,
        MSG_NEW_PLAYER,
        MSG_PLAYER_ADDED,
        MSG_EDIT_PLAYER,
        MSG_PLAYER_DELETED,
        MSG_CANCEL_ADD_PLAYER,
        MSG_ADD_PLAYER_TO_A_TEAM,
        MSG_TEAM_CHANGED,
        MSG_MATCH_SAVED,
        MSG_ADD_RESULT_TO_MATCH
    }

    public partial class App : ApplicationBase {
        public static Model.Model Model { get; private set; } = new Model.Model();

        //public static readonly string IMAGE_PATH =
        //     Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../images");

        public static Model.Member CurrentUser { get; set; } // Le user connecté

        public static void CancelChanges() {
            //Model.Dispose(); // Retire de la mémoire le modèle actuel
            Model = new Model.Model(); // Recréation d'un nouveau modèle à partir de la DB
        }
        public App() {

            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            //ColdStart();
            TestDB();
            
        }
        /*
                private void ColdStart() {
                    Model.SeedData();
                }*/
        private void TestDB() {

            foreach (var match in App.Model.Matchs) {
                match.DeleteTeam();
            }
            App.Model.Matchs.RemoveRange(App.Model.Matchs); 
            App.Model.Members.RemoveRange(App.Model.Members);
            App.Model.Teams.RemoveRange(App.Model.Teams);
            App.Model.SaveChanges();
            App.Model.CreateTeams(); 

            //var adm = model.CreateAdmin("admin", "admin", "admin@gmail.com", "admin", 44, "Rue de l'administration", "/path", Fonction.Admin);
            var sec = App.Model.CreateSecretary("secretaire", "secretaire", "secretaire@gmail.com", "secretaire", 30, "Rue du document", "/path", Fonction.Secretary);
            var coach = App.Model.CreateCoach("coach", "coach", "coach@gmail.com", "coach", 30, "Rue du document", "/path", Fonction.Coach);

            // Le secrétaire encode des nouveaux joueurs.
            var player1 = sec.CreatePlayer("Dupont", "André", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
            var player2 = sec.CreatePlayer("Tartine", "Martine", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player3 = sec.CreatePlayer("Dubois", "Charles", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
            var player4 = sec.CreatePlayer("Noyce", "Robert", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
            var player5 = sec.CreatePlayer("Viton", "Cerf", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player6 = sec.CreatePlayer("Robert", "Caillau", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
            var player7 = sec.CreatePlayer("West", "Jason", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
            var player8 = sec.CreatePlayer("Sudo", "root", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player9 = sec.CreatePlayer("Calme", "Paix", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
            var player10 = sec.CreatePlayer("Clair", "Juste", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
            var player11 = sec.CreatePlayer("Planet", "Humain", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player12 = sec.CreatePlayer("Univers", "Quantique", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);

            App.Model.SaveChanges();
        }
    }
}
