using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using prbd_1920_g04.Model;
using PRBD_Framework;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerDetailAddView.xaml
    /// </summary>
    public partial class PlayerDetailAddView : UserControlBase
    {
        public Player Player { get; set; }

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

        private Position position;
        public Position Position {
            get {
                return position;
            }

            set {
                position = value;
                RaisePropertyChanged(nameof(Position));
            }
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
            if(!LastNameAndFirstNameExists() && !JerseyNumberExiste() && Validate() && Secretary.Fonction.ToString().Equals("Secretary"))
            {
                Player.FirstName = firstName;
                Player.LastName = lastName;
                Player.Email = email;
                Player.Password = password;
                Player.Age = age;
                Player.Adresse = adresse;
                Player.Height = hght;
                Player.Weight = weight;
                Player.PicturePath = PicturePath;
                Player.JerseyNumber = jerseyNumber;
                Player.Position = Position;
                Player.Fonction = Fonction.Player;
                Secretary.CreatePlayer(Player);
                Player = App.Model.Players.Create();

                firstNameTextBox.Text = null;
                lastNameTextBox.Text = null;
                passwordTextBox.Text = null;
                emailTextBox.Text = null;
                adresseTextBox.Text = null;
                PicturePath = null;
                ageTextBox.Text = "0";
                heightTextBox.Text = "0";
                jerseyNumberTextBox.Text = "0";
                weightTextBox.Text = "0";

                App.NotifyColleagues(AppMessages.MSG_PLAYER_ADDED);
                App.NotifyColleagues(AppMessages.MSG_UPDATE_SELECT_PLAYERS_FOR_MATCH);
                App.NotifyColleagues(AppMessages.MSG_CONSOLE_MSG, new Model.Message(false, "You have added a player, you are at : " + new ObservableCollection<Player>(App.Model.Players).Count() + " players available."));
            }
        }

        private bool CanSaveOrCancelAction()
        {
            try
            {
                if (!string.IsNullOrEmpty(firstNameTextBox.Text) && firstNameTextBox.Text.Length >= 3 && firstNameTextBox.Text.Length <= 20
                    && !string.IsNullOrEmpty(lastNameTextBox.Text) && lastNameTextBox.Text.Length >= 3 && lastNameTextBox.Text.Length <= 20
                    && !string.IsNullOrEmpty(passwordTextBox.Text) && passwordTextBox.Text.Length >= 8 
                    && !string.IsNullOrEmpty(emailTextBox.Text) && emailTextBox.Text.Length >= 8 && emailTextBox.Text.Length <= 40
                    && !string.IsNullOrEmpty(adresseTextBox.Text) && adresseTextBox.Text.Length >= 10 && adresseTextBox.Text.Length <= 100
                    && (Int32.Parse(ageTextBox.Text) >= 7 && Int32.Parse(ageTextBox.Text) <= 50)
                    && (Int32.Parse(jerseyNumberTextBox.Text) >= 1 && Int32.Parse(jerseyNumberTextBox.Text) <= 99)
                    && (Int32.Parse(heightTextBox.Text) >= 165 && Int32.Parse(heightTextBox.Text) <= 210)
                    && (Int32.Parse(weightTextBox.Text) >= 60 && Int32.Parse(weightTextBox.Text) <= 110)
                    && !LastNameAndFirstNameExists() && !JerseyNumberExiste() && Secretary.Fonction.ToString().Equals("Secretary"))
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
                try
                    {
                     if (p.Category.MinAge <= Int32.Parse(ageTextBox.Text) && p.Category.MaxAge >= Int32.Parse(ageTextBox.Text)
                                                && p.JerseyNumber.Equals(Int32.Parse(jerseyNumberTextBox.Text)))
                     {
                        return true;
                     }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex);
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
            }else if (FirstName.Length > 20)
            {
                AddError("FirstName", Properties.Resources.Error_LengthInferiorEqual20);
            }

            //LastName
            if (string.IsNullOrEmpty(LastName) || LastName.Length < 3)
            {
                AddError("LastName", Properties.Resources.Error_LengthGreaterEqual3);
            }else if (LastName.Length > 20)
            {
                AddError("LastName", Properties.Resources.Error_LengthInferiorEqual20);
            }

            //email
            if (string.IsNullOrEmpty(email) || email.Length < 8)
            {
                AddError("Email", Properties.Resources.Error_LengthGreaterEqual8);
            }else if (email.Length > 40)
            {
                AddError("Email", Properties.Resources.Error_LengthInferiorEqual40);
            }

            //password
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                AddError("Password", Properties.Resources.Error_LengthGreaterEqual8);
            }else if (password.Length > 40)
            {
                AddError("Password", Properties.Resources.Error_LengthInferiorEqual40);
            }

            //adresse
            if (string.IsNullOrEmpty(adresse) || adresse.Length < 10)
            {
                AddError("Adresse", Properties.Resources.Error_LengthGreaterEqual8);
            }else if (adresse.Length > 100)
            {
                AddError("Adresse", Properties.Resources.Error_LengthInferiorEqual100);
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
