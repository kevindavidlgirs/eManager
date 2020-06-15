using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using prbd_1920_g04.Model;
using PRBD_Framework;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerDetailAddView.xaml
    /// </summary>
    public partial class PlayerDetailAddView : UserControlBase
    {
        //Devrait être enlever ou être utilisé de facon plus approfondie !
        public Player Player { get; set; }
        //Devrait être enlever ou être utilisé de facon plus approfondie !

        public Secretary Secretary { get; set; }
        
        private ImageHelper imageHelper;

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty<string>(ref firstName, value, () => Validate());
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty<string>(ref lastName, value, () => Validate());
        }

        private int age;
        public int Age
        {
            get => age;
            set => SetProperty<int>(ref age, value, () => Validate());
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty<string>(ref email, value, () => Validate());
        }

        private string adresse;
        public string Adresse
        {
            get => adresse;
            set => SetProperty<string>(ref adresse, value, () => Validate());
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty<string>(ref password, value, () => Validate());
        }


        private int hght;
        public int Hght
        {
            get => hght;
            set => SetProperty<int>(ref hght, value, () => Validate());
        }

        private double weight;
        public double Weight
        {
            get => weight;
            set => SetProperty<double>(ref weight, value, () => Validate());
        }

        private int jerseyNumber;
        public int JerseyNumber
        {
            get => jerseyNumber;
            set => SetProperty<int>(ref jerseyNumber, value, () => Validate());
        }
        public string PicturePath
        {
            get { return Player.AbsolutePicturePath; }
            set
            {
                Player.PicturePath = value;
                RaisePropertyChanged(nameof(PicturePath));
            }
        }
        public ICommand Save { get; set; }
        public ICommand ClearImage { get; set; }
        public ICommand LoadImage { get; set; }
        private void SaveAction()
        {
            if(!LastNameAndFirstNameExists() && !JerseyNumberExiste() && Validate())
            {
                Secretary.CreatePlayer(firstName, lastName, email, password, age, adresse, hght, weight, PicturePath, jerseyNumber);
                App.NotifyColleagues(AppMessages.MSG_PLAYER_ADDED);
                Console.WriteLine("Vous avez ajoutez un joueur, vous êtes à : " + new ObservableCollection<Player>(App.Model.Players).Count() + " joueurs.");
            }
        }

        private bool CanSaveOrCancelAction()
        {
            try
            {
                if (!string.IsNullOrEmpty(firstNameTextBox.Text) && firstNameTextBox.Text.Length >= 3
                    && !string.IsNullOrEmpty(lastNameTextBox.Text) && lastNameTextBox.Text.Length >= 3
                    && !string.IsNullOrEmpty(passwordTextBox.Text) && passwordTextBox.Text.Length >= 8
                    && (Int32.Parse(ageTextBox.Text) >= 7 && Int32.Parse(ageTextBox.Text) <= 50) 
                    && !string.IsNullOrEmpty(emailTextBox.Text) && emailTextBox.Text.Length >= 8
                    && !string.IsNullOrEmpty(adresseTextBox.Text) && adresseTextBox.Text.Length >= 10
                    && (Int32.Parse(jerseyNumberTextBox.Text) > 0 && Int32.Parse(jerseyNumberTextBox.Text) < 100)
                    && (Int32.Parse(heightTextBox.Text) >= 165 && Int32.Parse(heightTextBox.Text) <= 210)
                    && (Int32.Parse(weightTextBox.Text) >= 60 && Int32.Parse(weightTextBox.Text) <= 110)
                    && !LastNameAndFirstNameExists() && !JerseyNumberExiste())
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
        private void ClearImageAction()
        {
            PicturePath = null;
            imageHelper.Clear();
        }
        private void LoadImageAction()
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog().ToString().Equals("OK"))
            {
                var filename = fd.FileName;
                if (filename != null && File.Exists(filename))
                {
                    imageHelper.Load(fd.FileName);
                    PicturePath = imageHelper.CurrentFile;
                }
            }
        }
        private bool LastNameAndFirstNameExists()
        {
            var plysList = new ObservableCollection<Player>(App.Model.Players);
            foreach (var p in plysList)
            {
                if (p.FirstName == firstNameTextBox.Text && p.LastName == lastNameTextBox.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private bool JerseyNumberExiste()
        {
            var plysList = new ObservableCollection<Player>(App.Model.Players);
            foreach (var p in plysList)
            {
                foreach (var t in p.Teams)
                {
                    try
                    {
                        if (t.MinAge <= Int32.Parse(ageTextBox.Text) && t.MaxAge >= Int32.Parse(ageTextBox.Text)
                                                && p.JerseyNumber.Equals(Int32.Parse(jerseyNumberTextBox.Text)))
                        {
                            return true;
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return false;
        }

        //TODO : Affichage à peaufiner
        public override bool Validate()
        {
            ClearErrors();

            //FirstName
            if (string.IsNullOrEmpty(FirstName) || FirstName.Length < 3)
            {
                AddError("FirstName", Properties.Resources.Error_LengthGreaterEqual3);
            }

            //LastName
            if (string.IsNullOrEmpty(LastName) || LastName.Length < 3)
            {
                AddError("LastName", Properties.Resources.Error_LengthGreaterEqual3);
            }

            //email
            if (string.IsNullOrEmpty(email) || email.Length < 8)
            {
                AddError("Email", Properties.Resources.Error_LengthGreaterEqual8);
            }

            //password
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                AddError("Password", Properties.Resources.Error_LengthGreaterEqual8);
            }

            //adresse
            if (string.IsNullOrEmpty(adresse) || adresse.Length < 10)
            {
                AddError("Adresse", Properties.Resources.Error_LengthGreaterEqual8);
            }

            //JerseyNumber
            if (jerseyNumber < 1)
            {
                AddError("JerseyNumber", Properties.Resources.Error_NumberGreaterEqual1);
            }else if(jerseyNumber > 99)
            {
                AddError("JerseyNumber", Properties.Resources.Error_NumberInferiorEqual99);
            }else if (JerseyNumberExiste())
            {
                AddError("JerseyNumber", Properties.Resources.Error_CombinationAgeAndJerseyNumber);
            }

            //Age
            if (age < 7)
            {
                AddError("Age", Properties.Resources.Error_NumberGreaterEqual7);
            }
            else if (age > 50)
            {
                AddError("Age", Properties.Resources.Error_NumberInferiorEqual50);
            }
            

            //Weight
            if (weight < 60)
            {
                AddError("Weight", Properties.Resources.Error_NumberGreaterEqual60);
            }
            else if (weight > 110)
            {
                AddError("Weight", Properties.Resources.Error_NumberInferiorEqual110);

            }

            //Height
            if (hght < 165)
            {
                AddError("Hght", Properties.Resources.Error_NumberGreaterEqual165);
            }
            else if (hght > 210)
            {
                AddError("Hght", Properties.Resources.Error_NumberInferiorEqual210);
            }
            RaiseErrors();
            return !HasErrors;
        }


        public PlayerDetailAddView()
        {
            DataContext = this;
            Player = App.Model.Players.Create();
            Secretary = (Secretary)App.CurrentUser;
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            ClearImage = new RelayCommand(ClearImageAction);
            LoadImage = new RelayCommand(LoadImageAction);
            imageHelper = new ImageHelper(App.IMAGE_PATH, Player.PicturePath);
            var Players = new ObservableCollection<Player>(App.Model.Players);
            Console.WriteLine("Vous êtes à : " + Players.Count() + " joueurs.");
            InitializeComponent();
        }

    } 
}
