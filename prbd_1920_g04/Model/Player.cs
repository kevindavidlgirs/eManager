﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public override string ToString()
        {
            return $"<User: Name={LastName}, FirstName={FirstName}, Email={Email}, Age={Age}, Adresse={Adresse}, Role={Fonction.ToString()}>";
        }

        public virtual ICollection<Match> Matchs { get; set; } = new HashSet<Match>();

        //Devrait changer
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        //Devrait changer

        public virtual ICollection<Statistics> StatsList { get; set; } = new HashSet<Statistics>();
        public virtual Statistics Stats { get; set; }
        protected Player() { }
    }
}
