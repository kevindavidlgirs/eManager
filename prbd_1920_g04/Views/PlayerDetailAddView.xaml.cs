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
            }
        }


        private int hght;
        public int Hght
        {
            get
            {
                return hght;
            }
            set
            {
                hght = value;
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
            }
        }
        //Binding TextBox (end)

        public ICommand Save { get; set; }

        private void SaveAction()
        {
            Player = Secretary.CreatePlayer(firstName, lastName, email, password, age, adresse, hght, weight, null, jerseyNumber);
            var Players = new ObservableCollection<Model.Player>(App.Model.Players);
            App.NotifyColleagues(AppMessages.MSG_PLAYER_ADDED);
            Console.WriteLine("Vous avez ajoutez un joueur, vous êtes à : "+ Players.Count()+" joueurs.");
        }

        private bool CanSaveOrCancelAction()

        {
            //Changer en requête Linq 
            var plysList= new ObservableCollection<Model.Player>(App.Model.Players);
            var playerExiste = false;
            try
            {
                foreach (var p in plysList)
                {
                    if (p.FirstName == firstNameTextBox.Text && p.LastName == lastNameTextBox.Text)
                    {
                        playerExiste = true;
                    }
                    else
                    { 
                        foreach(var t in p.Teams)
                        {
                            if (t.MinAge <= Int32.Parse(ageTextBox.Text) && t.MaxAge >= Int32.Parse(ageTextBox.Text)
                                && p.JerseyNumber.Equals(Int32.Parse(jerseyNumberTextBox.Text)))
                            {
                               playerExiste = true;
                            }
                                
                        }
                    }
                }

                if (!string.IsNullOrEmpty(firstNameTextBox.Text) && !string.IsNullOrEmpty(lastNameTextBox.Text)
                    && !string.IsNullOrEmpty(passwordTextBox.Text) && (Int32.Parse(ageTextBox.Text) >= 7 && Int32.Parse(ageTextBox.Text) <= 50) 
                    && !string.IsNullOrEmpty(emailTextBox.Text) && !string.IsNullOrEmpty(adresseTextBox.Text)
                    && (Int32.Parse(jerseyNumberTextBox.Text) > 0 && Int32.Parse(jerseyNumberTextBox.Text) < 100)
                    && (Int32.Parse(heightTextBox.Text) >= 165 && Int32.Parse(heightTextBox.Text) <= 210)
                    && (Int32.Parse(weightTextBox.Text) >= 60 && Int32.Parse(weightTextBox.Text) <= 110)
                    && !playerExiste)
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public PlayerDetailAddView()
        {
            DataContext = this;
            Secretary = (Model.Secretary)App.CurrentUser;
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            var Players = new ObservableCollection<Model.Player>(App.Model.Players);
            Console.WriteLine("Vous êtes à : " + Players.Count() + " joueurs.");
            InitializeComponent();
        }

    } 
}
