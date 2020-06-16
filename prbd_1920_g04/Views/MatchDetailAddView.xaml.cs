using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using PRBD_Framework;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour MatchDetailAdd.xaml
    /// </summary>
    public partial class MatchDetailAddView : UserControlBase {
        public Model.Match Match { get; set; }
        public Model.Secretary Secretary {get; set;}

        private ImageHelper imageHelperHome;

        private ImageHelper imageHelperAdversary;

        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

        private ObservableCollection<Model.Category> categories;
        public ObservableCollection<Model.Category> Categories { get => categories; set => SetProperty(ref categories, value); }

        //Binding des TextBox START
        private string home;
        public string Home
        {
            get => home;
            set => SetProperty<string>(ref home, value, () => Validate());
        }

        private string adversary;
        public string Adversary {
            get => adversary;
            set => SetProperty<string>(ref adversary, value, () => Validate());
        }

        //(n'est pas un textBox)
        private DateTime dateMatch = DateTime.Now;
        public DateTime DateMatch {
            get => dateMatch;
            set => SetProperty<DateTime>(ref dateMatch, value, () => Validate());
        }

        private string place;
        public string Place {
            get => place;
            set => SetProperty<string>(ref place, value, () => Validate());
        }

        private string category;
        public string Category {
            get
            {
                return category;
            }
            set
            {
                category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }
        public string PicturePathHome
        {
            get { return Match.AbsolutePicturePathHome; }
            set
            {
                Match.PicturePathHome = value;
                RaisePropertyChanged(nameof(PicturePathHome));
            }
        }
        public string PicturePathAdversary
        {
            get { return Match.AbsolutePicturePathAdversary; }
            set
            {
                Match.PicturePathAdversary = value;
                RaisePropertyChanged(nameof(PicturePathAdversary));
            }
        }
        //Binding des TextBox END

        public ICommand Save { get; set; }
        public ICommand ClearImageHome { get; set; }
        public ICommand LoadImageHome { get; set; }
        public ICommand ClearImageAdversary { get; set; }
        public ICommand LoadImageAdversary { get; set; }
        private void ClearImageActionHome()
        {
            PicturePathHome = null;
            imageHelperHome.Clear();
        }
        private void LoadImageHomeAction()
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog().ToString().Equals("OK"))
            {
                var filename = fd.FileName;
                if (filename != null && File.Exists(filename))
                {
                    imageHelperHome.Load(fd.FileName);
                    PicturePathHome = imageHelperHome.CurrentFile;
                }
            }
        }

        private void ClearImageActionAdversary()
        {
            PicturePathAdversary = null;
            imageHelperAdversary.Clear();
        }
        private void LoadImageAdversaryAction()
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog().ToString().Equals("OK"))
            {
                var filename = fd.FileName;
                if (filename != null && File.Exists(filename))
                {
                    imageHelperAdversary.Load(fd.FileName);
                    PicturePathAdversary = imageHelperAdversary.CurrentFile;
                }
            }
        }

        private void SaveAction() {
            if(Secretary.Fonction.ToString().Equals("Secretary"))
            {
                Match.Home = home;
                Match.Adversary = adversary;
                Match.DateMatch = dateMatch;
                Match.Place = Place;
                Secretary.AddMatch(Match, category);
                Match = App.Model.Matchs.Create();

                homeTextBox.Text = null;
                adversaryTextBox.Text = null;
                placeTextBox.Text = null;
                PicturePathHome = null;
                PicturePathAdversary = null;
                comboBoxCategory.SelectedIndex = -1;

                Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs);
                App.NotifyColleagues(AppMessages.MSG_MATCH_ADDED);
                App.NotifyColleagues(AppMessages.MSG_MATCH_SAVED, Home + " vs " + Adversary);
                Console.WriteLine("Count of Matchs : " + Matchs.Count);
            }
        }

        private bool CanSaveOrCancelAction() {
            if (adversaryTextBox.Text.Length >= 2 && adversaryTextBox.Text.Length <= 10
                && homeTextBox.Text.Length >= 2 && homeTextBox.Text.Length <= 10
                && placeTextBox.Text.Length >= 2 && placeTextBox.Text.Length <= 20
                && comboBoxCategory.SelectedIndex != -1 && Validate() && Secretary.Fonction.ToString().Equals("Secretary"))
            {
                return true;
            }
            return false;
        }

        public override bool Validate()
        {
            ClearErrors();

            //Home
            if (string.IsNullOrEmpty(Home) || Home.Length < 2)
            {
                AddError("Home", Properties.Resources.Error_LengthGreaterEqual2);
            }else if (Home.Length > 10)
            {
                AddError("Home", Properties.Resources.Error_LengthInferiorEqual10);
            }

            //Adversary
            if (string.IsNullOrEmpty(Adversary) || Adversary.Length < 2)
            {
                AddError("Adversary", Properties.Resources.Error_LengthGreaterEqual2);
            }else if(Adversary.Length > 10)
            {
                AddError("Adversary", Properties.Resources.Error_LengthInferiorEqual10);

            }

            //Place
            if (string.IsNullOrEmpty(Place) || Place.Length < 2)
            {
                AddError("Place", Properties.Resources.Error_LengthGreaterEqual2);
            }else if (Place.Length > 20)
            {
                AddError("Place", Properties.Resources.Error_LengthInferiorEqual10);
            }

            //DateMatch
            if (new ObservableCollection<Model.Match>(App.Model.Matchs.Where(m => m.DateMatch.Equals(DateMatch))).Count > 0)
            {
                AddError("DateMatch", Properties.Resources.Error_DateMatchExiste);
            }
            RaiseErrors();
            return !HasErrors;
        }

        public MatchDetailAddView() {
            DataContext = this;
            Secretary = (Model.Secretary) App.CurrentUser;
            Match = App.Model.Matchs.Create();
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs);

            ClearImageHome = new RelayCommand(ClearImageActionHome);
            LoadImageHome = new RelayCommand(LoadImageHomeAction);
            ClearImageAdversary = new RelayCommand(ClearImageActionAdversary);
            LoadImageAdversary = new RelayCommand(LoadImageAdversaryAction);

            //Créer une classe pour gérer la redondance
            imageHelperHome = new ImageHelper(App.IMAGE_PATH, Match.PicturePathHome);
            imageHelperAdversary = new ImageHelper(App.IMAGE_PATH, Match.PicturePathAdversary);
            //Créer une classe pour gérer la redondance

            Categories = new ObservableCollection<Model.Category>(App.Model.Category.OrderBy(m => m.Name));
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            InitializeComponent();
        }
    }
}

