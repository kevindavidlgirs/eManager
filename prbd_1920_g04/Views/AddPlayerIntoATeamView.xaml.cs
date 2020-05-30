using prbd_1920_g04.Model;
using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPlayerIntoATeamView.xaml
    /// </summary>
    public partial class AddPlayerIntoAMatchView : UserControlBase
    {
        public Model.Secretary Secretary { get; set; }

        private ObservableCollection<Model.Player> players;
        public ObservableCollection<Model.Player> Players { get => players; set => SetProperty(ref players, value); }
        
        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string mtchs;
        public string Mtchs
        {
            get { return mtchs; }
            set
            {
                mtchs = value;
                RaisePropertyChanged(nameof(Mtchs));
            }
        }
        public ICommand Save { get; set; }

        private void SaveAction()
        {
            foreach(var player in checkListBox.SelectedItems)
            {
                Player p = (Player)player;
                Secretary.AddPlayerInMatchs(p.Id, matchSelected);
            }
            ComboBoxPlayers(matchSelected.TeamPlaying);
            SetLabelPlaceAvalaible();
        }

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        private bool CanSaveOrCancelAction()
        {
            if (IsNew)
            {
                return !(Matchs.Count() == 0 && HasErrors);
            }
            return false; 
        }

        private Match matchSelected;
        public Match MatchSelected
        {
            get { return matchSelected; }
            set { matchSelected = value; }
        }

        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            foreach(var m in Matchs)
            {
                if (m.Equals((Model.Match)Mc.SelectedItem))
                {
                    matchSelected = m;
                    ComboBoxPlayers(matchSelected.TeamPlaying);
                    SetLabelPlaceAvalaible();
                    equipeAdverse.Content = "VS " + matchSelected.Adversary + ". Date of meeting : " + matchSelected.DateMatch.Day+" /" + matchSelected.DateMatch.Month;
                    return;
                }
            }
        }

        private void ComboBoxMatchs()
        {
            var tmp  = new ObservableCollection<Model.Match>(App.Model.Matchs);
            Matchs = new ObservableCollection<Model.Match>();
            foreach (var match in tmp)
            {
                if (!match.IsComplete())
                {
                    Matchs.Add(match);
                }
            }
        }

        private void ComboBoxPlayers(Model.Team t)
        {
            var Plys = new ObservableCollection<Model.Player>(App.Model.Players);
            Players = new ObservableCollection<Model.Player>();
            foreach (var p in Plys)
            {
                if (!matchSelected.PlayersId.Contains(p.Id) && p.Age >= t.MinAge && p.Age <= t.MaxAge)
                {
                    Players.Add(p);
                }
            }
            isNew = Players.Count > 0;
            checkListBox.SelectedItems.Clear();
        } 

        private void SetLabelPlaceAvalaible()
        {
            int placeAvalaible = 11 - matchSelected.NumberOfPlayers();
            if(placeAvalaible < 1)
            {
                teamIsComplete.Content = "The team is complete !";
            }
            else
            {
                teamIsComplete.Content = "This team still has at least " + placeAvalaible + " places.";
            }
        }

        public AddPlayerIntoAMatchView()
        {
            DataContext = this;
            Secretary = (Model.Secretary)App.CurrentUser;
            ComboBoxMatchs();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            App.Register<Model.Match>(this, AppMessages.MSG_MATCH_CHANGED, match => { ComboBoxMatchs(); Players = null; equipeAdverse.Content = "Please select a team."; teamIsComplete.Content = ""; });
            App.Register<Model.Match>(this, AppMessages.MSG_PLAYER_ADDED, player => { ComboBoxMatchs(); Players = null; equipeAdverse.Content = "Please select a team."; teamIsComplete.Content = ""; });
            InitializeComponent();
        }
    }
}
