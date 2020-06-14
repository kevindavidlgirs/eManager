﻿using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string AbsolutePicturePath
        {
            get
            {
                return PicturePath != null ? App.IMAGE_PATH + "\\" + PicturePath : null;
            }
        }
        public override string ToString()
        {
            return $"<User: Name={LastName}, FirstName={FirstName}, Email={Email}, Age={Age}, Adresse={Adresse}, Role={Fonction.ToString()}>";
        }
        public virtual ICollection<Sanction> Sanctions { get; set; } = new HashSet<Sanction>();
        public virtual ICollection<Performance> Peformances { get; set; } = new HashSet<Performance>();
        public virtual ICollection<Injury> Injuries { get; set; } = new HashSet<Injury>();
        public virtual ICollection<Match> Matchs { get; set; } = new HashSet<Match>();
        public virtual ICollection<Team> Teams  { get; set; } = new HashSet<Team>();
        public virtual Statistics Stats { get; set; }
        protected Player() { }
    }
}
