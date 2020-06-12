using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public enum InjuryPlace {
        Consussion,
        Head,
        UpperLimb,
        Torso,
        Perlvis,
        LowerLimb
    }

    public class Injury {
        [Key]
        public InjuryPlace Place { get; set; }
        public int NumberDayAway { get; set; }
    }
}
