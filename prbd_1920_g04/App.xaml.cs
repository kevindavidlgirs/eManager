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
        MSG_MATCH_ADDED,
        MSG_MATCH_DELETED,
        MSG_MATCH_IS_OVER,
        MSG_NEW_PLAYER,
        MSG_PLAYER_ADDED,
        MSG_ADD_PLAYER_TO_A_TEAMS,
        MSG_REMOVE_PLAYER_TO_A_TEAM,
        MSG_ADD_PLAYER_TO_A_TEAM,
        MSG_TEAM_CHANGED,
        MSG_MATCH_SAVED,
        MSG_ADD_RESULT_TO_MATCH,
        MSG_UPDATE_MATCH,
        MSG_TEAM_COMPLET,
        MSG_ADD_STATS_TO_PLAYER,
        MSG_REMOVE_STATS_PLAYERS_TAB, 
        MSG_VIEW_PLAYER,
        MSG_CONSOLE_MSG
    }

    public partial class App : ApplicationBase {
        public static Model.Model Model { get; private set; } = new Model.Model();

        public static readonly string IMAGE_PATH = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../images");

        public static Model.Member CurrentUser { get; set; }

        public static void CancelChanges() {
            Model = new Model.Model(); 
        }

        private void TestDB() {
            if(App.Model.Matchs != null){
                foreach (var match in App.Model.Matchs)
                {
                    match.DeleteCategorie();
                }
                App.Model.Matchs.RemoveRange(App.Model.Matchs);
                App.Model.Stats.RemoveRange(App.Model.Stats);
                App.Model.Members.RemoveRange(App.Model.Members);
                App.Model.Category.RemoveRange(App.Model.Category);
                App.Model.SaveChanges();
            }

            App.Model.CreateCategories();
            var sec = App.Model.CreateSecretary("secretaire", "secretaire", "secretaire@gmail.com", "secretaire", 30, "Rue du document", "/path", Fonction.Secretary);

            //U17
            var player1 = sec.CreatePlayer("Dupont", "André", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 1);
            var player2 = sec.CreatePlayer("Jean-François", "Gillet", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 2);
            var player3 = sec.CreatePlayer("Dubois", "Charles", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 3);
            var player4 = sec.CreatePlayer("Noyce", "Robert", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 4);
            var player5 = sec.CreatePlayer("Viton", "Cerf", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 5);
            var player6 = sec.CreatePlayer("Dimitri", "Rogan", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 6);
            var player7 = sec.CreatePlayer("Arthur", "Borus", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 7);
            var player8 = sec.CreatePlayer("Marc", "Ddodo", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 8);
            var player9 = sec.CreatePlayer("Clarence", "Moto", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 9);
            var player10 = sec.CreatePlayer("Riddick", "Khan", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 10);
            var player11 = sec.CreatePlayer("Albert", "Romano", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 11);
            var player12 = sec.CreatePlayer("Roberto", "Firmino", "player@gmail.com", "FootBallplayer", 20, "Avenue du ballon", 175, 72.5, "/path", 12);
            //U15
            var player13 = sec.CreatePlayer("Zinho", "Vanheusden", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 1);
            var player14 = sec.CreatePlayer("Dimitri", "Lavalée", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 2);
            var player15 = sec.CreatePlayer("Moussa", "Sissako", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 3);
            var player16 = sec.CreatePlayer("Noë", "Dussenne", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 4);
            var player17 = sec.CreatePlayer("Duje", "Cop", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 5);
            var player18 = sec.CreatePlayer("Gojko", "Cimirot", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 6);
            var player19 = sec.CreatePlayer("Mehdi", "Carcela", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 7);
            var player20 = sec.CreatePlayer("Denis", "Dragus", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 8);
            var player21 = sec.CreatePlayer("John", "Nekadio", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 9);
            var player22 = sec.CreatePlayer("Eden", "Hazard", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 10);
            var player23 = sec.CreatePlayer("Roberto", "Carlos", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 11);
            var player24 = sec.CreatePlayer("Keylor", "Navas", "player@gmail.com", "FootBallplayer", 16, "Avenue du ballon", 175, 72.5, "/path", 12);

            // Le secrétaire encode le match.
            var match1 = sec.AddMatch(new DateTime(2020, 04, 28), "Epfc Stadium", "EPFC", "SUPINFO", "U8");
            var match2 = sec.AddMatch(new DateTime(2020, 05, 05), "Epfc Stadium", "EPFC", "HE2B", "U9");
            var match3 = sec.AddMatch(new DateTime(2020, 07, 13), "Epfc Stadium", "EPFC", "EPHEC", "U11");
            var match4 = sec.AddMatch(new DateTime(2020, 03, 18), "Epfc Stadium", "EPFC", "ESMO", "U17");
            var match8 = sec.AddMatch(new DateTime(2021, 03, 12), "Epfc Stadium", "EPFC", "ESMO", "U17");
            var match9 = sec.AddMatch(new DateTime(2020, 08, 21), "Epfc Stadium", "EPFC", "ESA", "U10");
            var match10 = sec.AddMatch(new DateTime(2020, 07, 06), "Epfc Stadium", "EPFC", "ICT", "U13");

            App.Model.SaveChanges();

        }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjczMDQ4QDMxMzgyZTMxMmUzMEZRNWNvS3lMeU42UjcrS2MzNmo3bm0wUmJrOFNrbExSTVk2bDI2dUtJT289");
            TestDB();
        }

    }
}
