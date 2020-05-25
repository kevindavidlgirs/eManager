using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using PRBD_Framework;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerDetailAddView.xaml
    /// </summary>
    public partial class PlayerDetailAddView : UserControlBase
    {

        public Model.Player Player { get; set; }
        public Model.Secretary Secretary { get; set; }


        //Binding textBox (start)
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaiseErrorsChanged(nameof(Email));
            }
        }

        private string adresse;
        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                adresse = value;
                RaiseErrorsChanged(nameof(Adresse));
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaiseErrorsChanged(nameof(Password));
            }
        }

        //Utilisation de new car nous effectuons un masquage nous devrions utiliser un autre mot pour rester dans la  
        //bonne pratique.
        private int height;
        public new int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                RaiseErrorsChanged(nameof(Height));
            }
        }

        private double weight;
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                RaisePropertyChanged(nameof(Weight));
            }
        }

        private int jerseyNumber;
        public int JerseyNumber
        {
            get
            {
                return jerseyNumber;
            }
            set
            {
                jerseyNumber = value;
                RaisePropertyChanged(nameof(JerseyNumber));
            }
        }
        //Binding TextBox (end)

        //Bouton save
        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        public ICommand Save { get; set; }
        //Bouton save

        //Devrait fermer l'onglet
        public ICommand Cancel { get; set; }
        //Devrait fermer l'onglet

        private void SaveAction()
        {
            Player = Secretary.CreatePlayer(firstName, lastName, email, password, age, adresse, height, weight, null, jerseyNumber);
            IsNew = false;
            var Players = new ObservableCollection<Model.Player>(App.Model.Players);
            Console.WriteLine("Vous avez ajoutez un joueur, vous êtes à : "+ Players.Count()+" joueurs.");
        }

        private void CancelAction()
        {
            
        }

        private bool CanSaveOrCancelAction()
        {
            if (IsNew)
            {
                return !(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) 
                    && (age == 0) && string.IsNullOrEmpty(password) && (jerseyNumber == 0) && HasErrors);
            }
            var change = (from c in App.Model.ChangeTracker.Entries<Model.Player>()
                          where c.Entity == Player
                          select c).FirstOrDefault();
            return change != null && change.State != EntityState.Unchanged;
        }

        public PlayerDetailAddView()
        {
            DataContext = this;
            IsNew = true;
            Secretary = (Model.Secretary)App.CurrentUser;
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            //Cancel = new RelayCommand(CancelAction);
            var Players = new ObservableCollection<Model.Player>(App.Model.Players);
            Console.WriteLine("Vous êtes à : " + Players.Count() + " joueurs.");
            InitializeComponent();
        }

        /*
         * NOTE : Nous devrions ajouter une photo.
         */
    } 
}
