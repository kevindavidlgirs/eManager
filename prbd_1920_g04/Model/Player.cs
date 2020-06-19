using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace prbd_1920_g04.Model
{
    public enum Position {
        GoalKeeper,
        Defender,
        Midfielder,
        Forward
    }

    public class Player : Member
    {
        public int Height { get; set; }

        public double Weight { get; set; }

        public int JerseyNumber { get; set; }


        [NotMapped]
        public string AbsolutePicturePath
        {
            get
            {
                return PicturePath != null ? App.IMAGE_PATH + "\\" + PicturePath : null;
            }
        }
        public virtual Category Category { get; set; }

        public virtual ICollection<Statistics> StatsList { get; set; } = new HashSet<Statistics>();
        public virtual Statistics Stats { get; set; }
        public virtual ICollection<Match> Matchs { get; set; } = new HashSet<Match>();

        public Match MatchForCreatePlayersStatsView;

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
            return new Statistics(this, MatchForCreatePlayersStatsView);
        }

        public override string ToString()
        {
            return $"<User: Name={LastName}, FirstName={FirstName}, Email={Email}, Age={Age}, Adresse={Adresse}, Role={Fonction.ToString()}>";
        }
        protected Player() { }
    }
}
