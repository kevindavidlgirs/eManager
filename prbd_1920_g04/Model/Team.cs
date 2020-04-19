using PRBD_Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04
{
    public class Team 
    {
        [Key]
        public string Name { get; set; }
        public List<Player> Player { get; set; }         

    }
}
