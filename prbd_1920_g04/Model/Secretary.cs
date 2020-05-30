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

            var player = App.Model.CreatePlayer(firstName, lastname, email, password, age, adresse, height, weight, picturePath, jerseyNumber, fonction);
            App.Model.SaveChanges();
            return player;
        }

        public Match AddMatch(DateTime date, string place, string home, string adversary, string categorie) { 
            Match match = null;
            var team = App.Model.Teams.Find(categorie); 
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
            if(!match.PlayersId.Contains(player.Id))
            {
                match.PlayersId.Add(player.Id);
                Console.WriteLine("Player : "+ player.FirstName +" "+ player.LastName + " added in -> " + match.Home);
                return player;
            }
            return player;
        }
    }
}
