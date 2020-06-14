﻿using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Match{
        [Key]
        public DateTime DateMatch { get; set; }
        public string Home { get; set; }
        public string Adversary { get; set; }
        public string Place { get; set; }
        public int GoalsHome { get; set; }
        public int GoalsAdversary { get; set; }
        public bool IsOver { get; set; }
        public bool IsComplete { get; set; }
        public string PicturePathHome { get; set; }
        public string PicturePathAdversary { get; set; }
        public virtual Team TeamPlaying { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
        public int NumberOfPlayers()
        {
            return Players.Count();
        }
        
        public Match() {
            DateMatch = DateTime.Now;
        }

        public void DeleteTeam() {
           
            if (TeamPlaying != null) {
                TeamPlaying = null;
            }

            App.Model.Matchs.Remove(this);
        }

    }
}
