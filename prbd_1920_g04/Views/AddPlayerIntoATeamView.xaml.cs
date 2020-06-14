using prbd_1920_g04.Model;
using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPlayerIntoATeamView.xaml
    /// </summary>
    public partial class AddPlayerIntoAMatchView : UserControlBase
    {
        public Secretary Secretary { get; set; }

        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players { get => players; set => SetProperty(ref players, value); }
        
        private ObservableCollection<Match> matchs;
        public ObservableCollection<Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

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

        private Match matchSelected;
        public Match MatchSelected
        {
            get { return matchSelected; }
            set { matchSelected = value; }
        }

        private bool activeButtonSave;
        public bool ActiveButtonSave
        {
            get { return activeButtonSave; }
            set { activeButtonSave = value; }
        }
        public ICommand Save { get; set; }

        private bool CanSaveOrCancelAction()
        {
            return activeButtonSave;
        }
        private void SaveAction()
        {
            foreach(var player in checkListBox.SelectedItems)
            {
                Player p = (Player)player;
                Secretary.AddPlayerInMatchs(p.Id, matchSelected);
            }
            if (matchSelected.NumberOfPlayers() >= 11)
            {
                matchSelected.IsComplete = true;
                App.Model.SaveChanges();
            }
            CheckedListBoxPlayers(matchSelected.TeamPlaying);
            SetLabelPlaceAvalaible();
            App.NotifyColleagues(AppMessages.MSG_ADD_PLAYER_TO_A_TEAM, new ObservableCollection<Match>(App.Model.Matchs.Where(m => (m.IsComplete))).Count > 0);
        }

        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            foreach(var m in Matchs)
            {
                if (m.Equals((Match)comboboxMatchs.SelectedItem))
                {
                    matchSelected = m;
                    CheckedListBoxPlayers(matchSelected.TeamPlaying);
                    SetLabelPlaceAvalaible();
                    categorie.Content = "Catégorie "+ matchSelected.TeamPlaying.Name;
                    equipeAdverse.Content = "VS " + matchSelected.Adversary + ". Date of meeting : " + matchSelected.DateMatch.Day+" /" + matchSelected.DateMatch.Month;
                    return;
                }
            }
        }

        private void ComboBoxMatchs()
        {
            Matchs = new ObservableCollection<Match>(App.Model.Matchs.Where(m => (m.Players.Count < 99 && !m.IsOver)).OrderBy(m => m.DateMatch));
        }

        private void CheckedListBoxPlayers(Team t)
        {
            var players = new ObservableCollection<Player>(App.Model.Players);
            Players = new ObservableCollection<Player>();
            foreach(var p in players)
            {
   
                if(matchSelected.TeamPlaying.Players.Contains(p) && !matchSelected.Players.Contains(p))
                {
                    Players.Add(p);
                }
            }
            activeButtonSave = Players.Count > 0;
            checkListBox.SelectedItems.Clear();
        } 

        private void SetLabelPlaceAvalaible()
        {
            if(matchSelected.NumberOfPlayers() == 99)
            {
                teamIsComplete.Content = "The match is complete !";
            }
            else if (matchSelected.NumberOfPlayers() > 11)
            {
                teamIsComplete.Content = "The team is complete but this match still has at least " + (99 - matchSelected.NumberOfPlayers()) + " places.";
            }
            else
            {
                teamIsComplete.Content = "This match still has at least " + (99 - matchSelected.NumberOfPlayers()) + " places.";
            }
        }

        public AddPlayerIntoAMatchView()
        {
            DataContext = this;
            Secretary = (Secretary)App.CurrentUser;
            ComboBoxMatchs();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            App.Register(this, AppMessages.MSG_MATCH_IS_OVER, () => { ComboBoxMatchs(); Players = null; equipeAdverse.Content = "Please select a team."; teamIsComplete.Content = ""; categorie.Content = ""; comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_MATCH_ADDED, () => { ComboBoxMatchs(); Players = null; equipeAdverse.Content = "Please select a team."; teamIsComplete.Content = ""; categorie.Content = ""; comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_PLAYER_ADDED, () => { if(matchSelected != null)CheckedListBoxPlayers(matchSelected.TeamPlaying); });
            InitializeComponent();
        }
    }
}
