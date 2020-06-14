using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model
{
    public class Secretary : Member {

        public Player CreatePlayer(string firstName, string lastname, string email, string password, int age, string adresse, int height, double weight, string picturePath, int jerseyNumber, Fonction fonction = Fonction.Player)
        {
            bool playerExiste = false;
            var teams = App.Model.Teams;
            Player player = null;
            foreach (var t in teams)
            {
                foreach(var p in t.Players)
                {
                    if(p.FirstName.Equals(firstName) && p.LastName.Equals(lastname))
                    {
                        playerExiste = true;
                    }
                    else if (t.MinAge <= age && t.MaxAge >= age && p.JerseyNumber == jerseyNumber)
                    {
                        playerExiste = true;
                    }
                }
            }
            foreach(var t in App.Model.Teams)
            { 
                if (age >= t.MinAge && age <= t.MaxAge && !playerExiste 
                    && firstName.Length >= 3 && LastName.Length >= 3 && email.Length >= 8 && password.Length >= 8
                    && adresse.Length >= 10 && (jerseyNumber > 0 && jerseyNumber < 100)
                    && (height >= 165 && height <= 210) && (weight >= 60 && weight <= 110))
                {
                    player = App.Model.CreatePlayer(firstName, lastname, email, password, age, adresse, height, weight, picturePath, jerseyNumber, fonction);
                    t.Players.Add(player);
                    player.Teams.Add(t);
                }
            }
            App.Model.SaveChanges();
            return player;
        }

        public Match AddMatch(DateTime date, string place, string home, string adversary, string categorie) { 
            Match match = null;
            var team = App.Model.Teams.Find(categorie);

            /* Pourquoi interdire la création de rencontres identiques à partir du moment où elles n'ont pas lieu au même moment.
             * Dans la réalité ça se produit tout le temps : match aller / match retour.
             * De plus cela supprime une fonctionnalité dans détail match.
             * On en parle au tel si tu veux ?
            var q = from m in App.Model.Matchs
                    where m.Home.Equals(home) && m.Adversary.Equals(adversary)
                    select m;
            */

            if (team != null) {
                match = App.Model.CreateMatch(date, place, home, adversary, team);
                App.Model.SaveChanges();
                return match;
            }
            return match;
        }

        public Player AddPlayerInMatchs(int id, Match mtch)
        {
            var match = App.Model.Matchs.Find(mtch.DateMatch);
            var player = App.Model.Players.Find(id);
            if(!match.Players.Contains(player))
            {
                match.Players.Add(player);
                Console.WriteLine("Player : "+ player.FirstName +" "+ player.LastName + " added in -> " + match.Home);
                return player;
            }
            return player;
        }
    }
}
