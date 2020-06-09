using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Statistics {
        [Key]
        public int GoalsScored { get; set; }
        public int OwnGoals { get; set; }
        public int GoalsConceced { get; set; }
        public int Assists { get; set; }
        public int Injuries { get; set; }
        public int Fouls { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
}
