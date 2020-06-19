using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Statistics {
        [Key]
        public int StatisticsId { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceced { get; set; }
        public int Assists { get; set; }
        public int Injuries { get; set; }
        public int Fouls { get; set; }
        public int SumGoodActions { get; set; }
        public int SumBadActions { get; set; }
        public string Date { get; set; }
        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }

        public Statistics()
        {
        }
        public Statistics(Player p, Match m)
        {
            Player = p;
            GoalsScored = 0;
            GoalsConceced = 0;
            Assists = 0;
            Injuries = 0;
            Fouls = 0;
            Match = m;
            
        }
        public Statistics(Statistics s)
        {
            StatisticsId = s.StatisticsId;
            GoalsScored = s.GoalsScored;
            GoalsConceced = s.GoalsConceced;
            Assists = s.Assists;
            Injuries = s.Injuries;
            Fouls = s.Fouls;
            Player = s.Player;
            Match = s.Match;
        }

        public void copyAttr(Statistics s)
        {
            StatisticsId = s.StatisticsId;
            GoalsScored = s.GoalsScored;
            GoalsConceced = s.GoalsConceced;
            Assists = s.Assists;
            Injuries = s.Injuries;
            Fouls = s.Fouls;
        }
    }
}
