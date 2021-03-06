﻿using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model
{
    public class Secretary : Member {

        public Player CreatePlayer(Player p)
        {
            return CreatePlayer(p.FirstName, p.LastName, p.Email, p.Password, p.Age, p.Adresse, p.Height, p.Weight, p.PicturePath, p.JerseyNumber,p.Position);
        }

        public Player CreatePlayer(string firstName, string lastname, string email, string password, 
            int age, string adresse, int height, double weight, string picturePath, int jerseyNumber, 
             Position position, Fonction fonction = Fonction.Player) {

            bool playerExiste = false;
            var categories = App.Model.Category;
            Player player = null;
            foreach (var t in categories)
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
            foreach(var c in categories)
            { 
                if (!playerExiste  && age >= c.MinAge && age <= c.MaxAge 
                    && firstName.Length >= 3 && firstName.Length <= 20 
                    && LastName.Length >= 3 && LastName.Length <= 20 
                    && email.Length >= 8 && email.Length <= 40 
                    && password.Length >= 8 && password.Length <= 40
                    && adresse.Length >= 10 && adresse.Length <= 100
                    && (jerseyNumber > 0 && jerseyNumber < 100)
                    && (height >= 165 && height <= 210) && (weight >= 60 && weight <= 110))
                {
                    player = App.Model.CreatePlayer(firstName, lastname, email, password, age, adresse, height, weight, picturePath, jerseyNumber, position, fonction);
                    c.Players.Add(player);
                    player.Category = c;
                }
            }
            App.Model.SaveChanges();
            return player;
        }

        public Match AddMatch(Match m, string cat)
        {
            return AddMatch(m.DateMatch, m.Place, m.Home, m.Adversary, cat);
        }

        public Match AddMatch(DateTime date, string place, string home, string adversary, string categorie) { 
            Match match = null;
            var cat = App.Model.Category.Find(categorie);
            if (cat != null && place.Length >= 2 && place.Length <= 20 && home.Length >= 2 && home.Length <= 10
                && adversary.Length >= 2 && adversary.Length <= 10 && new ObservableCollection<Match>(App.Model.Matchs.Where(m => m.DateMatch.Equals(date))).Count == 0) {
                match = App.Model.CreateMatch(date, place, home, adversary, cat);
                App.Model.SaveChanges();
                return match;
            }
            return match;
        }

        public Player AddPlayerInMatchs(int id, Match mtch)
        {
            var match = App.Model.Matchs.Find(mtch.DateMatch);
            var player = App.Model.Players.Find(id);
            if(!match.Teams.Contains(player) && match.Teams.Count < 11 && !match.IsOver)
            {
                match.Teams.Add(player);
                return player;
            }
            return player;
        }

        public Player RemovePlayerInMatchs(int id, Match mtch)
        {
            var match = App.Model.Matchs.Find(mtch.DateMatch);
            var player = App.Model.Players.Find(id);
            if (match.Teams.Contains(player) && !match.IsOver)
            {
                match.Teams.Remove(player);
                return player;
            }
            return player;
        }
    }
}
