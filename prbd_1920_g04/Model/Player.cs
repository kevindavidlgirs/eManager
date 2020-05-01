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

        //Attribut intéressant ?
        public int JerseyNumber { get; set; }

        public Position PlayerPosition { get; set; }

        public virtual Team Team { get; set; }

        protected Player() { }

        public virtual ICollection<Sanction> Sanctions { get; set; } = new HashSet<Sanction>();
        public virtual ICollection<Performance> Peformances { get; set; } = new HashSet<Performance>();
        public virtual ICollection<Injury> Injuries { get; set; } = new HashSet<Injury>();
    }
}
