using System;
using System.Data.Entity;


namespace prbd_1920_g04.Model
{
    public class Model : DbContext {
        public Model() : base("prbd_1920_g04") {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<Model>());
        }

        //Déclarer DbSet<User> (User est une classe abstraite) et les autres permet d'indiquer à EF qu'il y a de l'héritage
        //https://authfix-le-gaulois.tech/heritage-entity-framework-core/
        public DbSet<Member> Members { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Statistics> Stats { get; set; }

        public Secretary CreateSecretary(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Secretary)
        {
            var secretary = Secretaries.Create();
            secretary.FirstName = firstName;
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

        public Player CreatePlayer(string firstName, string lastName, string email, 
                                   string password, int age, string adresse,
                                   int height, double weight, string picturePath, int jerseyNumber,
                                   Fonction fonction = Fonction.Player){
            var player = Players.Create();
            var statistics = Stats.Create();
            player.FirstName = firstName;
            player.LastName = lastName;
            player.Email = email;
            player.Password = password;
            player.Age = age;
            player.Adresse = adresse;
            player.Height = height;
            player.Weight = weight;
            player.PicturePath = picturePath;
            player.JerseyNumber = jerseyNumber;
            player.Fonction = fonction;
            player.Stats = statistics;
            Players.Add(player);
            return player;
        }

        public Match CreateMatch(DateTime date, string place, string home, string adversary, Category category) {
            var match = Matchs.Create();
            match.DateMatch = date;
            match.Place = place;
            match.Home = home;
            match.Adversary = adversary;
            match.Category = category; 
            Matchs.Add(match);
            return match;
        }

        public Category CreateCategory(string name, int MinAge, int MaxAge) {
            var cat = Category.Create();
            cat.Name = name;
            cat.MinAge = MinAge;
            cat.MaxAge = MaxAge;
            Category.Add(cat);

            return cat;
        }

        public void CreateCategories() {
            App.Model.CreateCategory("U7", 7, 7);
            App.Model.CreateCategory("U8", 8, 8);
            App.Model.CreateCategory("U9", 9, 9);
            App.Model.CreateCategory("U10", 10, 10);
            App.Model.CreateCategory("U11", 11, 12);
            App.Model.CreateCategory("U13", 13, 14);
            App.Model.CreateCategory("U15", 15, 16);
            App.Model.CreateCategory("U17", 17, 20);
            App.Model.CreateCategory("U21", 21, 50);

            App.Model.SaveChanges();
        }
    }
}