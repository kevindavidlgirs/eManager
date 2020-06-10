using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Statistics {
        [ForeignKey("Player")]
        public int StatisticsId { get; set; }
        public int GoalsScored { get; set; }
        public int OwnGoals { get; set; }
        public int GoalsConceced { get; set; }
        public int Assists { get; set; }
        public int Injuries { get; set; }
        public int Fouls { get; set; }
        public virtual Player Player { get; set; }
    }
}
