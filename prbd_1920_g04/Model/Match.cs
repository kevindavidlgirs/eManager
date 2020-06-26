using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Match{
        [Key]
        public DateTime DateMatch { get; set; }
        public string Home { get; set; }
        public string Adversary { get; set; }
        public string HomeVsAdversary { get => Home + " vs " + Adversary; }
        public string Place { get; set; }
        public int GoalsHome { get; set; }
        public int GoalsAdversary { get; set; }
        public bool IsOver { get; set; }
        public bool TeamIsCompete { get; set; }
        public string PicturePathHome { get; set; }
        public string PicturePathAdversary { get; set; }
        [NotMapped]
        public string AbsolutePicturePathHome
        {
            get
            {
                return PicturePathHome != null ? App.IMAGE_PATH + "\\" + PicturePathHome : null;
            }
        }
        [NotMapped]
        public string AbsolutePicturePathAdversary
        {
            get
            {
                return PicturePathAdversary != null ? App.IMAGE_PATH + "\\" + PicturePathAdversary : null;
            }
        }

        public virtual Category Category { get; set; }

        public virtual ICollection<Player> Teams { get; set; } = new HashSet<Player>();

        public int NumberOfPlayers()
        {
            return Teams.Count();
        }

        public void DeleteCategorie() {
           
            if (Category != null) {
                Category = null;
            }
            App.Model.Matchs.Remove(this);
        }
        public Match()
        {
            DateMatch = DateTime.Now;
        }
    }
}
