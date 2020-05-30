using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Match {
        [Key]
        public DateTime DateMatch { get; set; }
        public string Home { get; set; }
        public string Adversary { get; set; }
        public string Place { get; set; }
        public int GoalsHome { get; set; }
        public int GoalsAdversary { get; set; }
        public bool IsOver { get; set; }
        public virtual Team TeamPlaying { get; set; }
        public List<int> PlayersId { get; set; } = new List<int>();
        public int NumberOfPlayers()
        {
            return PlayersId.Count();
        }
        public bool IsComplete()
        {
            return PlayersId.Count() >= 11;
        }

        public Match() {
            IsOver = false;
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
