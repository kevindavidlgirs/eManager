using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.RightsManagement;
using PRBD_Framework;

namespace prbd_1920_g04
{
    public enum Fonction
    {
        Admin = 0,
        Secretary = 1,
        Coach = 2,
        Player = 3,
        Delegate = 4
    }

    public class Model : DbContext
    {
        public Model() : base("prbd_1920_g04")
        {
            // la base de données est supprimée et recréée quand le modèle change
            Database.SetInitializer<Model>(new DropCreateDatabaseIfModelChanges<Model>());
        }

        //Déclarer DbSet<User> (User est une classe abstraite) et les autres permet d'indiquer à EF qu'il y a de l'héritage
        //https://authfix-le-gaulois.tech/heritage-entity-framework-core/
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Secretary> Secretary { get; set; }
        public DbSet<Delegate> Delegate { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Player> Player { set; get; }
        //Déclarer DbSet<User> (User est une classe abstraite) et les autres permet d'indiquer à EF qu'il y a de l'héritage

        public DbSet<Team> Team { set; get; }
        public Admin CreateAdmin(string name, string firstName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Admin)
        {
            var admin = Admin.Create();
            admin.Name = name;
            admin.FirstName = firstName;
            admin.Email = email;
            admin.Password = password;
            admin.Age = age;
            admin.Adresse = adresse;
            admin.PicturePath = picturePath;
            admin.Fonction = fonction;
            Admin.Add(admin);
            return admin;
        }

        public Secretary CreateSecretary(string name, string firstName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
        {
            var secretary = Secretary.Create();
            secretary.Name = name;
            secretary.FirstName = firstName;
            secretary.Email = email;
            secretary.Password = password;
            secretary.Age = age;
            secretary.Adresse = adresse;
            secretary.PicturePath = picturePath;
            secretary.Fonction = fonction;
            Secretary.Add(secretary);
            return secretary;
        }

        public Delegate CreateDelegate(string name, string firstName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
        {
            var dlg = Delegate.Create();
            dlg.Name = name;
            dlg.FirstName = firstName;
            dlg.Email = email;
            dlg.Password = password;
            dlg.Age = age;
            dlg.Adresse = adresse;
            dlg.PicturePath = picturePath;
            dlg.Fonction = fonction;
            Delegate.Add(dlg);
            return dlg;
        }

        public Coach CreateCoach(string name, string firstName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
        {
            var coach = Coach.Create();
            coach.Name = name;
            coach.FirstName = firstName;
            coach.Email = email;
            coach.Password = password;
            coach.Age = age;
            coach.Adresse = adresse;
            coach.PicturePath = picturePath;
            coach.Fonction = fonction;
            Coach.Add(coach);
            return coach;
        }
        public Player CreatePlayer(string name, string firstName, string email, string password, int age, string adresse, string equipe, int cut, double weight, string picturePath, int jerseyNumber, Fonction fonction = Fonction.Player)
        {
            var player = Player.Create();
            player.Name = name;
            player.FirstName = firstName;
            player.Email = email;
            player.Password = password;
            player.Age = age;
            player.Adresse = adresse;
            player.Equipe = equipe;
            player.Cut = cut;
            player.Weight = weight;
            player.PicturePath = picturePath;
            player.JerseyNumber = jerseyNumber;
            player.Fonction = fonction;
            Player.Add(player);
            return player;
        }

        public Team CreateTeam(string name, List<Player>  pl) 
        {
            var team = Team.Create();

            //Je me suis arrêté ici, mais ce n'est pas correct car la primary key "name" ne peut pas être dupliqué
            //Aussi il faut créer une relation avec la table player... A voir.
            //team.Name = name;
            //team.Player.AddRange(p1);

            Team.Add(team);
            return team;
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
            using (var model = new Model())
            {
                model.Database.Log = Console.Write;
                //Ne fonctionne plus, je supprime la BD à la main pour l'instant 
                foreach (var user in model.User)
                {
                    user.Delete();
                }
                model.SaveChanges();
                //Ne fonctionne plus, je supprime la BD à la main pour l'instant 

                var adm = model.CreateAdmin("admin", "admin", "admin@gmail.com", "admin", 44, "Rue de l'administration", "/path", Fonction.Admin);
                var sec = model.CreateSecretary("secretaire", "secretaire", "secretaire@gmail.com", "secretaire", 30, "Rue du document", "/path", Fonction.Secretary);
                var player1 = sec.CreatePlayer("Dupont", "André", "player@gmail.com", "player", 20, "Avenue du ballon", "A1", 175, 72.5, "/path", 10, Fonction.Player);
                var player2 = sec.CreatePlayer("Tartine", "Martine", "player@gmail.com", "player", 20, "Avenue du ballon","A1", 175, 72.5, "/path", 7, Fonction.Player);
                var player3 = sec.CreatePlayer("Dubois", "Charles", "player@gmail.com", "player", 20, "Avenue du ballon", "A2", 175, 72.5, "/path", 9, Fonction.Player);
                model.SaveChanges();
                Console.WriteLine(adm+"\n"+sec+"\n"+player1+"\n"+player2+"\n"+player3);
                var playerUpdate = sec.UpdatePlayer("Dubois", "Charles", "player@gmail.com", "player", 25, "Avenue du ballon", "A2", 175, 70.5, "/path", 11);
                Console.WriteLine("'Dubois' after update \n"+playerUpdate);
                //sec.DeletePlayer("David");
                var playersList = new List<Player> { player1, player2, player3 };
                
                //Je me suis arrêté ici
                //var team = sec.CreateTeam("Win", playersList);
               
                Console.ReadLine();
            }
        }
    }
}