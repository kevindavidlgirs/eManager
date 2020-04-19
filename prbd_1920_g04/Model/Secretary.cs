using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04
{
    public class Secretary : User
    {
        public Player CreatePlayer(string name, string firstName, string email, string password, int age, string adresse, string equipe, int cut, double weight, string picturePath, int jerseyNumber, Fonction fonction = Fonction.Player)
        {
            Model m = new Model();
            var player = m.CreatePlayer(name, firstName, email, password, age, adresse, equipe, cut, weight, picturePath, jerseyNumber, Fonction.Player);
            m.SaveChanges();
            return player;
        }

        public Player UpdatePlayer(string name, string firstName, string email, string password, int age, string adresse, string equipe, int cut, double weight, string picturePath, int jerseyNumber)
        {
            Model m = new Model();
            var res = m.Player.SingleOrDefault(u => u.Name == name);
            if(res != null)
            {
                res.Name = name;
                res.FirstName = firstName;
                res.Email = email;
                res.Password = password;
                res.Age = age;
                res.Adresse = adresse;
                res.Equipe = equipe;
                res.Cut = cut;
                res.Weight = weight;
                res.PicturePath = picturePath;
                res.JerseyNumber = jerseyNumber;
                m.SaveChanges();
            }
            return res;
        }

       
        public bool DeletePlayer(string name)
        {
            Model m = new Model();
            var user = m.User.Find(name);
            if (user != null)
            {
                m.User.Remove(user);
                m.SaveChanges();
                return true;
            }
            return false;
            
        }

        public Team CreateTeam(string name, List<Player> pl)
        {
            Model m = new Model();
            var team = m.CreateTeam(name, pl);
            m.SaveChanges();
            return team;
        }

        //public void Browse() { }

    }
}
