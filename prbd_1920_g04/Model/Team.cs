﻿using PRBD_Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model
{
    public class Team 
    {
        [Key]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();

        public int NumberOfPlayers() {
            return Players.Count();
        }

        public bool IsComplete()
        {
            return Players.Count() >= 11;
        }
    }
}