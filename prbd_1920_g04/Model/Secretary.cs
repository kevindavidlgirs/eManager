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

            Model m = new Model();

            var player = m.CreatePlayer(firstName, lastname,email, password, age, adresse, height, weight, picturePath, jerseyNumber, fonction);
            m.SaveChanges();
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
            Model m = new Model();
            var user = m.Members.Find(name);
            if (user != null)
            {
                m.Members.Remove(user);
               // m.SaveChanges();
                return true;
            }
            return false;
            
        }

        public Match AddMatch(DateTime date, string place, string home, string adversary, string categorie) { 
            Model m = new Model();
            Match match = null;
            var team = m.Teams.Find(categorie); // On récupère la catégorie.
            if (team != null) {
                match = m.CreateMatch(date, place, home, adversary, team);
                m.SaveChanges();
                return match;
            }

            return match;
        }

        //public void Browse() { }
    }
}
