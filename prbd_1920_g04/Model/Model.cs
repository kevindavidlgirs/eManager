using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.RightsManagement;
using PRBD_Framework;

namespace prbd_1920_g04.Model
{
    public enum Fonction
    {
        Admin = 1,
        Secretary = 2,
        Coach = 3,
        Player = 4,
        Delegate = 5
    }

    public class Model : DbContext {
        public Model() : base("prbd_1920_g04") {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<Model>());
        }

        //Déclarer DbSet<User> (User est une classe abstraite) et les autres permet d'indiquer à EF qu'il y a de l'héritage
        //https://authfix-le-gaulois.tech/heritage-entity-framework-core/
        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Delegate> Delegates { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Team> Teams { get; set; }
        //Déclarer DbSet<Staff> (User est une classe abstraite) et les autres permet d'indiquer à EF qu'il y a de l'héritage

        //public object Staff { get; internal set; }
/*      
        public Admin CreateAdmin(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Admin)
        {
            var admin = Admins.Create(); 
            admin.FristName = firstName;
            admin.LastName  = lastName;
            admin.Email = email;
            admin.Password = password;
            admin.Age = age;
            admin.Adresse = adresse;
            admin.PicturePath = picturePath;
            admin.Fonction = fonction;
            Admins.Add(admin);
            return admin;
        }*/

        public Secretary CreateSecretary(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
        {
            var secretary = Secretaries.Create();
            secretary.FristName = firstName;
            secretary.LastName = lastName;
            secretary.Email = email;
            secretary.Password = password;
            secretary.Age = age;
            secretary.Adresse = adresse;
            secretary.PicturePath = picturePath;
            secretary.Fonction = fonction;
            Secretaries.Add(secretary);
            return secretary;
        }

        /*     public Delegate CreateDelegate(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
             {
                 var dlg = Delegates.Create();
                 dlg.FristName = firstName;
                 dlg.LastName = lastName;
                 dlg.Email = email;
                 dlg.Password = password;
                 dlg.Age = age;
                 dlg.Adresse = adresse;
                 dlg.PicturePath = picturePath;
                 dlg.Fonction = fonction;
                 Delegates.Add(dlg);
                 return dlg;
             }
             */

        public Coach CreateCoach(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Coach)
        {
            var coach = Coaches.Create();
            coach.FristName = firstName;
            coach.LastName = lastName;
            coach.Email = email;
            coach.Password = password;
            coach.Age = age;
            coach.Adresse = adresse;
            coach.PicturePath = picturePath;
            coach.Fonction = fonction;
            Coaches.Add(coach);
            return coach;
        }

        public Player CreatePlayer(string firstName, string lastName, string email, 
                                   string password, int age, string adresse,
                                   int cut, double weight, string picturePath, int jerseyNumber,
                                   Fonction fonction = Fonction.Player){
            var player = Players.Create();
            player.FristName = firstName;
            player.LastName = lastName;
            player.Email = email;
            player.Password = password;
            player.Age = age;
            player.Adresse = adresse;
            player.Height = cut;
            player.Weight = weight;
            player.PicturePath = picturePath;
            player.JerseyNumber = jerseyNumber;
            player.Fonction = fonction;

            Players.Add(player);
            return player;
        }

        public Match CreateMatch(DateTime date, string place, string home, string adversary, Team team) {
            var match = Matchs.Create();
            match.Date = date; // Temporaire.
            match.Place = place;
            match.Home = home;
            match.Adversary = adversary;
            match.Squad = team; //pose problème

            Matchs.Add(match);

            return match;
        }

        public Team CreateTeam(string name) {
            var team = Teams.Create();
            team.Name = name;
            Teams.Add(team);

            return team;
        }
        
        public void CreateTeams() {
            App.Model.CreateTeam("U7");
            App.Model.CreateTeam("U8");
            App.Model.CreateTeam("U9");
            App.Model.CreateTeam("U10");
            App.Model.CreateTeam("U11");
            App.Model.CreateTeam("U13");
            App.Model.CreateTeam("U15");
            App.Model.CreateTeam("U17");
            App.Model.CreateTeam("U21");
            App.Model.CreateTeam("A1");
            App.Model.CreateTeam("A2");

            App.Model.SaveChanges();
        }

        /*public void SeedData()
        {
            if (Administrator.Count() == 0 && Secretary.Count() == 0 && Players.Count() == 0)
            {
                Console.Write("Seeding users... ");
                var admin = CreateAdmin("admin", "admin", "admin@gmail.com", "admin", 44, "Rue de l'administration", "/path", Fonction.Admin);
                var secretary = CreateSecretary("secretaire", "secretaire", "secretaire@gmail.com", "secretaire", 30, "Rue du document", "/path", Fonction.Secretary);
                var player1 = CreatePlayer("Mbilo", "Hervé", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
                var player2 = CreatePlayer("Girs", "Kevin", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
                SaveChanges();
                Console.WriteLine("ok");
            }
        }
        */


    }

    class TestBD
    {
        static void Main(string[] args)
        {
                //model.Database.Log = Console.Write;
                
                // On commence par Désolidariser tous les équipes des matchs (c'est du bricolage mais ça fonctionne.)
                foreach (var match in App.Model.Matchs)
                {
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
                var player1 = sec.CreatePlayer("Dupont", "André", "player@gmail.com", "player", 20, "Avenue du ballon",175, 72.5, "/path", 10, Fonction.Player);
                var player2 = sec.CreatePlayer("Tartine", "Martine", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
                var player3 = sec.CreatePlayer("Dubois", "Charles", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
                var player4 = sec.CreatePlayer("Noyce", "Robert", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 10, Fonction.Player);
                var player5 = sec.CreatePlayer("Viton", "Cerf", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 7, Fonction.Player);
                var player6 = sec.CreatePlayer("Robert", "Caillau", "player@gmail.com", "player", 20, "Avenue du ballon", 175, 72.5, "/path", 9, Fonction.Player);
                Console.WriteLine(sec + "\n" + player1 + "\n" + player2 + "\n" + player3);

                // Le secrétaire encode le match.
                var match1 = sec.AddMatch(new DateTime(2020, 04, 28), "Epfc Stadium", "Epfc", "EPHEC", "A1");

                //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe A1
                coach.selectPlayer(match1, player1);
                coach.selectPlayer(match1, player2);
                coach.selectPlayer(match1, player3);


                var match2 = sec.AddMatch(new DateTime(2020, 05, 12), "Epfc Stadium", "Epfc", "EPHEC", "U11");
                //Console.WriteLine(match + " = {" + match.Date + "," + match.Place + "," + match.Home + "," + match.Adversary + "," + match.Squad);

                //Le coach sélectionne des nouveaux joueurs pour le match avec l'équipe U11
                coach.selectPlayer(match2, player4);
                coach.selectPlayer(match2, player5);
                coach.selectPlayer(match2, player6);

                App.Model.SaveChanges();

                
                //var playerUpdate = sec.UpdatePlayer("Dubois", "Charles", "player@gmail.com", "player", 25, "Avenue du ballon", "A2", 175, 70.5, "/path", 11);

                //Console.WriteLine("'Dubois' after update \n"+playerUpdate);
                //sec.DeletePlayer("David");
                //var playersList = new List<Player> { player1, player2, player3 };
               
                //Console.ReadLine();
        }
    }
}