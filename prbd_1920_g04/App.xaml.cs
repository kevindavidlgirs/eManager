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

namespace prbd_1920_g04 {
    public enum Fonction {
        Admin = 1,
        Secretary = 2,
        Coach = 3,
        Player = 4,
        Delegate = 5
    }
    public enum AppMessages {
        MSG_NEW_MATCH,
        MSG_EDIT_MATCH,
        MSG_MATCH_CHANGED,
        MSG_MATCH_DELETED,
    }

    public partial class App : ApplicationBase {
        public static Model.Model Model { get; private set; } = new Model.Model();

        public static readonly string IMAGE_PATH =
            Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../images");

        //public static Member CurrentUser { get; set; }

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
            //model.Database.Log = Console.Write;

            // On commence par Désolidariser tous les équipes des matchs (c'est du bricolage mais ça fonctionne.)
            foreach (var match in App.Model.Matchs) {
                match.DeleteTeam();
            }

            App.Model.Matchs.RemoveRange(App.Model.Matchs); // On supprime les matchs
                                                            //model.Teams.RemoveRange(model.Teams); // On supprime les équipes
                                                            //App.Model.Members.RemoveRange(App.Model.Players);
            App.Model.Members.RemoveRange(App.Model.Members);

            App.Model.SaveChanges();

            //model.CreateTeams(); //Toutes les équipes sont créées

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
            Console.WriteLine(sec + "\n" + player1 + "\n" + player2 + "\n" + player3);

            // Le secrétaire encode le match.
            var match1 = sec.AddMatch(new DateTime(2020, 04, 28), "Epfc Stadium", "EPFC", "EPHEC", "A1");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe A1
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
            coach.selectPlayer(match3, player6);

            var match4 = sec.AddMatch(new DateTime(2020, 05, 3), "Epfc Stadium", "EPFC", "EPHEC", "U7");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
            coach.selectPlayer(match4, player4);
            coach.selectPlayer(match4, player5);
            coach.selectPlayer(match4, player6);

            var match5 = sec.AddMatch(new DateTime(2020, 05, 5), "Epfc Stadium", "EPFC", "EPHEC", "U13");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
            coach.selectPlayer(match5, player4);
            coach.selectPlayer(match5, player5);
            coach.selectPlayer(match5, player6);

            var match6 = sec.AddMatch(new DateTime(2020, 05, 7), "Epfc Stadium", "EPFC", "EPHEC", "U9");
            //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
            coach.selectPlayer(match6, player4);
            coach.selectPlayer(match6, player5);
            coach.selectPlayer(match6, player6);





            App.Model.SaveChanges();


            //var playerUpdate = sec.UpdatePlayer("Dubois", "Charles", "player@gmail.com", "player", 25, "Avenue du ballon", "A2", 175, 70.5, "/path", 11);

            //Console.WriteLine("'Dubois' after update \n"+playerUpdate);
            //sec.DeletePlayer("David");
            //var playersList = new List<Player> { player1, player2, player3 };

            //Console.ReadLine();
        }
    }
}
