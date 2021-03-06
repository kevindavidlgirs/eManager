﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PRBD_Framework;

namespace prbd_1920_g04.Model
{

    public abstract class Member : EntityBase<Model>
    {

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Adresse { get; set; }
        public string PicturePath { get; set; }
        public Fonction Fonction { get; set; }

    }
}
