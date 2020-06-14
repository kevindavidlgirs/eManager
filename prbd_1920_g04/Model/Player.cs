using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Position PlayerPosition { get; set; }

        public string TeamName { get; set; }

        public virtual ICollection<Match> Matchs { get; set; } = new HashSet<Match>();
        public virtual ICollection<Team> Teams  { get; set; } = new HashSet<Team>();
        public virtual Statistics Stats { get; set; }
        protected Player() { }

    }
}
