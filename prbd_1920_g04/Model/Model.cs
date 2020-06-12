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

        public Coach CreateCoach(string firstName, string lastName, string email, string password, int age, string adresse, string picturePath, Fonction fonction = Fonction.Coach)
        {
            var coach = Coaches.Create();
            coach.FirstName = firstName;
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
                                   int height, double weight, string picturePath, int jerseyNumber,
                                   Fonction fonction = Fonction.Player){
            var player = Players.Create();
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

            Players.Add(player);
            return player;
        }

        public Match CreateMatch(DateTime date, string place, string home, string adversary, Team team) {
            var match = Matchs.Create();
            match.DateMatch = date; // Temporaire.
            match.Place = place;
            match.Home = home;
            match.Adversary = adversary;
            match.TeamPlaying = team; //pose problème

            Matchs.Add(match);

            return match;
        }

        public Team CreateTeam(string name, int MinAge, int MaxAge) {
            var team = Teams.Create();
            team.Name = name;
            team.MinAge = MinAge;
            team.MaxAge = MaxAge;
            Teams.Add(team);

            return team;
        }

        public void CreateTeams() {
            App.Model.CreateTeam("U7", 7, 7);
            App.Model.CreateTeam("U8", 8, 8);
            App.Model.CreateTeam("U9", 9, 9);
            App.Model.CreateTeam("U10", 10, 10);
            App.Model.CreateTeam("U11", 11, 12);
            App.Model.CreateTeam("U13", 13, 14);
            App.Model.CreateTeam("U15", 15, 16);
            App.Model.CreateTeam("U17", 17, 20);
            App.Model.CreateTeam("U21", 21, 50);
            App.Model.CreateTeam("A1", 21, 50);
            App.Model.CreateTeam("A2", 21, 50);

            App.Model.SaveChanges();
        }

    }
}