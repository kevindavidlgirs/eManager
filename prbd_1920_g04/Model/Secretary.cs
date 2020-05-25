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

            var player = App.Model.CreatePlayer(firstName, lastname,email, password, age, adresse, height, weight, picturePath, jerseyNumber, fonction);
            App.Model.SaveChanges();
            return player;
        }
        /*
                public Player UpdatePlayer(string firstName,string lastname, string email, string password, int age, string adresse, Team team, int cut, double weight, string picturePath, int jerseyNumber)
                {
                    Model m = new Model();
                    if(res != null)
                    {
                        res.FristName = firstName;
                        res.LastName = lastname;
                        res.Email = email;
                        res.Password = password;
                        res.Age = age;
                        res.Adresse = adresse;
                        res.Team = team;
                        res.Cut = cut;
                        res.Weight = weight;
                        res.PicturePath = picturePath;
                        res.JerseyNumber = jerseyNumber;
                        m.SaveChanges();
                    }
                    return res;
                }
        */
        //var res = m.Players.SingleOrDefault(u => u.FristName == firstName);
        public bool DeletePlayer(string name)
        {
            var user = App.Model.Members.Find(name);
            if (user != null)
            {
                App.Model.Members.Remove(user);
               // m.SaveChanges();
                return true;
            }
            return false;
            
        }

        //IMPOSSIBLE D'AJOUTER PLUS D'UN MATCH!
        public Match AddMatch(DateTime date, string place, string home, string adversary, string categorie) { 
            Match match = null;
            var team = App.Model.Teams.Find(categorie); // On récupère la catégorie.
            if (team.IsComplete()) {
                match = App.Model.CreateMatch(date, place, home, adversary, team);
                App.Model.SaveChanges();
                return match;
            }
            return match;
        }

        //Cette méthode est déjà présente dans la classe "Coach" (considérons que c'est un coach qui fait ce choix)
        //A voir si le temps de l'implémenter.
        public bool AddPlayerInTeam(int id, string categorie)
        {
            var team = App.Model.Teams.Find(categorie);
            var player = App.Model.Players.Find(id);
            if(player.TeamName == null && !team.Players.Contains(player))
            {
                team.Players.Add(player);
                player.TeamName = categorie;
                return true;
            }
            return false;
        }
    }
}
