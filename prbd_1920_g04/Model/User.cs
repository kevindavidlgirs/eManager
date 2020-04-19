using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRBD_Framework;

namespace prbd_1920_g04
{
    public abstract class User : EntityBase<Model>
    {
        //Attention la primary key devrait changer car en mon sens ce n'est pas logique de se baser sur un nom de famille. (c'est de ma faute :) )
        [Key]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        //Nous avions oublié le password dans l'analyse
        public string Password { get; set; }
        public int Age { get; set; }
        public string Adresse { get; set; }
        public string PicturePath { get; set; }
        
        public Fonction Fonction { get; set; }

        //Fonction delete à enlever car n'est pas présente dans l'analyse (juste pour les tests mais ne fonctionne pas pour l'instant)
        public void Delete()
        {
            //Model m = new Model(); ????
            Model.User.Remove(this);
        }
        public override string ToString()
        {
            return $"<User: Name={Name}, FirstName={FirstName}, Email={Email}, Age={Age}, Adresse={Adresse}, Role={Fonction.ToString()}>";
        }

    }
}