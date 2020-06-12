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
        MSG_ADD_PLAYER_TO_A_TEAMS,
        MSG_ADD_PLAYER_TO_A_TEAM,
        MSG_TEAM_CHANGED,
        MSG_MATCH_SAVED,
        MSG_ADD_RESULT_TO_MATCH,
        MSG_UPDATE_MATCH,
        MSG_TEAM_COMPLET,
        MSG_ADD_STATS_TO_PLAYER
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
            App.Model.Stats.RemoveRange(App.Model.Stats);
            App.Model.Members.RemoveRange(App.Model.Members);
            App.Model.Teams.RemoveRange(App.Model.Teams);
            App.Model.SaveChanges();
            App.Model.CreateTeams(); 

            //var adm = model.CreateAdmin("admin", "admin", "admin@gmail.com", "admin", 44, "Rue de l'administration", "/path", Fonction.Admin);
            var sec = App.Model.CreateSecretary("secretaire", "secretaire", "secretaire@gmail.com", "secretaire", 30, "Rue du document", "/path", Fonction.Secretary);
            var coach = App.Model.CreateCoach("coach", "coach", "coach@gmail.com", "coach", 30, "Rue du document", "/path", Fonction.Coach);

            // Le secrétaire encode des nouveaux joueurs.
            var player1 = sec.CreatePlayer("Dupont", "André", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 1, Fonction.Player);
            var player2 = sec.CreatePlayer("Jean-François", "Gillet", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 2, Fonction.Player);
            var player3 = sec.CreatePlayer("Dubois", "Charles", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 3, Fonction.Player);
            var player4 = sec.CreatePlayer("Noyce", "Robert", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 4, Fonction.Player);
            var player5 = sec.CreatePlayer("Viton", "Cerf", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 5, Fonction.Player);
            var player6 = sec.CreatePlayer("Dimitri", "Rogan", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 6, Fonction.Player);

            var player7 = sec.CreatePlayer("Arthur", "Borus", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player8 = sec.CreatePlayer("Marc", "Ddodo", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 8, Fonction.Player);
            var player9 = sec.CreatePlayer("Clarence", "Moto", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
            var player10 = sec.CreatePlayer("Riddick", "Khan", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
            var player11 = sec.CreatePlayer("Albert", "Romano", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 11, Fonction.Player);
            var player12 = sec.CreatePlayer("Roberto", "Firmino", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 12, Fonction.Player);

            var player13 = sec.CreatePlayer("Zinho", "Vanheusden", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 1, Fonction.Player);
            var player14 = sec.CreatePlayer("Dimitri", "Lavalée", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 2, Fonction.Player);
            var player15 = sec.CreatePlayer("Moussa", "Sissako", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 3, Fonction.Player);
            var player16 = sec.CreatePlayer("Noë", "Dussenne", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 4, Fonction.Player);
            var player17 = sec.CreatePlayer("Duje", "Cop", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 5, Fonction.Player);

            var player18 = sec.CreatePlayer("Gojko", "Cimirot", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 6, Fonction.Player);
            var player19 = sec.CreatePlayer("Mehdi", "Carcela", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
            var player20 = sec.CreatePlayer("Denis", "Dragus", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 8, Fonction.Player);
            var player21 = sec.CreatePlayer("John", "Nekadio", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
            var player22 = sec.CreatePlayer("Eden", "Hazard", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);

            // Le secrétaire encode le match.
            var match1 = sec.AddMatch(new DateTime(2020, 04, 28), "Epfc Stadium", "EPFC", "SUPINFO", "A1");
            var match2 = sec.AddMatch(new DateTime(2020, 05, 05), "Epfc Stadium", "EPFC", "HE2B", "A1");
            var match3 = sec.AddMatch(new DateTime(2020, 07, 13), "Epfc Stadium", "EPFC", "EPHEC", "U11");
            var match4 = sec.AddMatch(new DateTime(2020, 03, 18), "Epfc Stadium", "EPFC", "ESMO", "U17");
            var match5 = sec.AddMatch(new DateTime(2020, 02, 29), "Epfc Stadium", "EPFC", "HELB", "A1");
            var match6 = sec.AddMatch(new DateTime(2020, 12, 04), "Epfc Stadium", "EPFC", "UCLOUVAIN", "U13");
            var match7 = sec.AddMatch(new DateTime(2020, 05, 07), "Epfc Stadium", "EPFC", "ESI", "U15");
            var match8 = sec.AddMatch(new DateTime(2020, 03, 12), "Epfc Stadium", "EPFC", "INRACI", "A2");
            var match9 = sec.AddMatch(new DateTime(2020, 08, 21), "Epfc Stadium", "EPFC", "ESA", "A7");
            var match10 = sec.AddMatch(new DateTime(2020, 07, 06), "Epfc Stadium", "EPFC", "ICT", "U13");
            var match11 = sec.AddMatch(new DateTime(2020, 05, 28), "Epfc Stadium", "EPFC", "HELB", "A2");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe A1
            /*
            coach.selectPlayer(match1, player1);
            coach.selectPlayer(match1, player2);
            coach.selectPlayer(match1, player3);

            var match2 = sec.AddMatch(new DateTime(2020, 05, 10), "Epfc Stadium", "EPFC", "EPHEC", "A2");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
            coach.selectPlayer(match2, player4);
            coach.selectPlayer(match2, player5);
            coach.selectPlayer(match2, player6);

            var match3 = sec.AddMatch(new DateTime(2020, 05, 12), "Epfc Stadium", "EPFC", "EPHEC", "U11");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
            coach.selectPlayer(match3, player4);
            coach.selectPlayer(match3, player5);
            coach.selectPlayer(match3, player6);*/

            App.Model.SaveChanges();
           
            //Console.ReadLine();
        }
    }
}
