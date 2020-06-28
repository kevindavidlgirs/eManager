using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace prbd_1920_g04.Model
{
    public class Player : Member
    {
        public int Height { get; set; }

        public double Weight { get; set; }

        public int JerseyNumber { get; set; }

        public string PlayerRepresentation { get => this.ToString(); }

        [NotMapped]
        public string AbsolutePicturePath
        {
            get
            {
                return PicturePath != null ? App.IMAGE_PATH + "\\" + PicturePath : null;
            }
        }

        public virtual Category Category { get; set; }

        public virtual ICollection<Match> Matchs { get; set; } = new HashSet<Match>();

        //Trouver une autre solution !
        public Match MatchForCreatePlayersStatsView;
        //Trouver une autre solution !

        public virtual ICollection<Statistics> StatsList { get; set; } = new HashSet<Statistics>();

        public virtual Statistics Stats { get; set; }

        public Position Position { get; set; }

        public Statistics Statistics => GetStatistics();

        private Statistics GetStatistics()
        {
            foreach(var s in StatsList)
            {
                if(s != null && s.Player.Equals(this) && s.Match.Equals(MatchForCreatePlayersStatsView))
                {
                    return s;
                }
            }
            Statistics st = new Statistics(this, MatchForCreatePlayersStatsView);
            StatsList.Add(st);
            return st;
        }


        public override string ToString()
        {
            return "Name: " + LastName + ", Firstname: " + FirstName + ", Age: " + Age +", J.N: "+ JerseyNumber;
        }
        protected Player() { }
    }
}
