using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04
{
    public class Player : User
    {
        public int Cut { get; set; }

        public double Weight { get; set; }

        //Attribut intéressant ?
        public int JerseyNumber { get; set; }

        public string Equipe { get; set; }
    }
}
