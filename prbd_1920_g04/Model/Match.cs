using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Match {
        [Key]
        public DateTime Date { get; set; }
        public string Home { get; set; }
        public string Adversary { get; set; }
        public string Place { get; set; }


        public virtual Team TeamPlaying { get; set; }

        public void DeleteTeam() {
           
            if (TeamPlaying != null) {
                TeamPlaying = null;
            }

            App.Model.Matchs.Remove(this);
        }
    }
}
