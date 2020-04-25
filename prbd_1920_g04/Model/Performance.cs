﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model {
    public class Performance {
        [Key]
        public string Name { get; set; }
        public int Quantity { get; set; }

        public virtual Player Player { get; set; }
    }
}
