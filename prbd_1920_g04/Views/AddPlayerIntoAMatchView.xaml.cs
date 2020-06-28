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
        public Secretary Secretary { get; set; }
        private ObservableCollection<Player> playersAvalaible;
        public ObservableCollection<Player> PlayersAvalaible { get => playersAvalaible; set => SetProperty(ref playersAvalaible, value); }

        private ObservableCollection<Player> playersAdded;
        public ObservableCollection<Player> PlayersAdded { get => playersAdded; set => SetProperty(ref playersAdded, value); }

        private ObservableCollection<Match> matchs;
        public ObservableCollection<Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }
      
        private Match matchSelected;
        public Match MatchSelected
        {
            get { return matchSelected; }
            set { 
                
                matchSelected = value;
                RaisePropertyChanged(nameof(MatchSelected));
            }
        }

        private bool activeButtonSave;
        public bool ActiveButtonSave
        {
            get { return activeButtonSave; }
            set { activeButtonSave = value; }
        }

        private bool activeButtonRemove;
        public bool ActiveButtonRemove
        {
            get { return activeButtonRemove; }
            set { activeButtonRemove = value; }
        }
        public ICommand Save { get; set; }

        public ICommand Remove { get; set; }

        private bool CanSaveOrCancelAction()

        {
            return activeButtonSave;
        }
        private bool CanRemoveOrCancelAction()
        {
            return activeButtonRemove;
        }

        private void SaveAction()
        {
            foreach(var player in checkListBoxAddPlayer.SelectedItems)
            {
                Player p = (Player)player;
                Secretary.AddPlayerInMatchs(p.Id, matchSelected);
                App.NotifyColleagues(AppMessages.MSG_CONSOLE_MSG, new Model.Message(false, p.FirstName + " " + p.LastName + " added in -> " + matchSelected.Home + ". Date match : " + matchSelected.DateMatch.ToString("dd/MM/yy")));
            }
            if (MatchSelected.NumberOfPlayers() >= 11)
            {
                MatchSelected.TeamIsCompete = true;
                App.Model.SaveChanges();
            }
            CheckedListBoxPlayersAvalaible();
            CheckedListBoxPlayersAdded();
            SetLabels(0);
            //Messages pour l'activation du bouton "addResult"
            App.NotifyColleagues(AppMessages.MSG_ADD_PLAYER_TO_A_TEAM, new ObservableCollection<Match>(App.Model.Matchs.Where(m => (m.TeamIsCompete && !m.IsOver))).Count > 0);
        }

        private void RemoveAction()
        {
            foreach (var player in checkListBoxPlayerSelected.SelectedItems)
            {
                Player p = (Player)player;
                Secretary.RemovePlayerInMatchs(p.Id, matchSelected);
                App.NotifyColleagues(AppMessages.MSG_CONSOLE_MSG, new Model.Message(false, p.FirstName + " " + p.LastName + " is removed from -> " + matchSelected.Home + ". Date match : "+ matchSelected.DateMatch.ToString("dd/MM/yy")));
            }
            if (MatchSelected.NumberOfPlayers() < 11)
            {
                MatchSelected.TeamIsCompete = false;
                App.Model.SaveChanges();
            }
            CheckedListBoxPlayersAdded();
            CheckedListBoxPlayersAvalaible();
            SetLabels(0);
            //Messages pour l'activation du bouton "addResult"
            App.NotifyColleagues(AppMessages.MSG_REMOVE_PLAYER_TO_A_TEAM, new ObservableCollection<Match>(App.Model.Matchs.Where(m => (m.TeamIsCompete && !m.IsOver))).Count > 0);
        }

        private void ComboBoxMatchs()
        {
            Matchs = new ObservableCollection<Match>(App.Model.Matchs.Where(m => (!m.IsOver)).OrderBy(m => m.DateMatch));
        }

        private void CheckedListBoxPlayersAvalaible()
        {
            var players = new ObservableCollection<Player>(App.Model.Players);
            PlayersAvalaible = new ObservableCollection<Player>();
            foreach(var p in players)
            {
   
                if(MatchSelected.Category.Players.Contains(p) && !MatchSelected.Teams.Contains(p))
                {
                    PlayersAvalaible.Add(p);
                }
            }
            activeButtonSave = PlayersAvalaible.Count > 0 && !MatchSelected.TeamIsCompete;

            if (checkListBoxAddPlayer != null) {
                checkListBoxAddPlayer.SelectedItems.Clear();
            }
        }

        private void CheckedListBoxPlayersAdded()
        {
            var players = new ObservableCollection<Player>(App.Model.Players);
            PlayersAdded = new ObservableCollection<Player>();
            foreach (var p in players)
            {

                if (MatchSelected.Category.Players.Contains(p) && MatchSelected.Teams.Contains(p))
                {
                    PlayersAdded.Add(p);
                }
            }
            activeButtonRemove = PlayersAdded.Count > 0 && !MatchSelected.IsOver;

            if (checkListBoxPlayerSelected != null) {
                checkListBoxPlayerSelected.SelectedItems.Clear();
            }
            
        }

        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (MatchSelected != null) {
                CheckedListBoxPlayersAvalaible();
                CheckedListBoxPlayersAdded();
                SetLabels(0);
                return;
            }

            foreach (var m in Matchs)
            {
                if (m.Equals((Match) comboboxMatchs.SelectedItem))
                {
                    MatchSelected = m;
                    CheckedListBoxPlayersAvalaible();
                    CheckedListBoxPlayersAdded();
                    SetLabels(0);
                    return;
                }
            }
        }

        private void CheckedListBox_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            SetLabels(checkListBoxAddPlayer.SelectedItems.Count);
            if (checkListBoxAddPlayer.SelectedItems.Count > 11 || MatchSelected.Teams.Count > 11 || (checkListBoxAddPlayer.SelectedItems.Count + MatchSelected.Teams.Count) > 11)
            {
                foreach (var p in checkListBoxAddPlayer.SelectedItems)
                {
                    if (p.ToString().Equals(e.Item.ToString()))

                    {
                        checkListBoxAddPlayer.SelectedItems.Remove(p);
                        return;
                    }
                }
            }
        }

        private void SetLabels(int playerSelected)
        {
            if (MatchSelected != null && MatchSelected.NumberOfPlayers() == 11)
            {
                checkListLeft.Content = "The match is complete !";
            }
            else
            {
                //checkListLeft.Content = "5";
                checkListLeft.Content = "This match still has at least " + (11 - MatchSelected.NumberOfPlayers()) + " places -  you have selected " + playerSelected + " players.";
            }

            categorie.Content = "Catégorie " + MatchSelected.Category.Name + " players avalaible : " + (MatchSelected.Category.Players.Count - MatchSelected.NumberOfPlayers());
            dateOfMeeting.Content = "Date of meeting : " + MatchSelected.DateMatch.Day + " /" + MatchSelected.DateMatch.Month;
        }

        private void ResetAll()
        {
            checkListLeft.Content = "";
            categorie.Content = "";
            dateOfMeeting.Content = "Please select a team.";
            PlayersAvalaible = null; PlayersAdded = null; activeButtonSave = false; activeButtonRemove = false;
        }

        public AddPlayerIntoAMatchView(Match match = null)
        {
            DataContext = this;
            Secretary = (Secretary)App.CurrentUser;
            MatchSelected = match;
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            Remove = new RelayCommand(RemoveAction, CanRemoveOrCancelAction);
            App.Register<bool>(this, AppMessages.MSG_MATCH_IS_OVER, o => { ComboBoxMatchs(); ResetAll(); comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_MATCH_ADDED, () => { ComboBoxMatchs(); ResetAll(); comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_PLAYER_ADDED, () => { if(MatchSelected != null)CheckedListBoxPlayersAvalaible(); if (MatchSelected != null) SetLabels(0); });
            InitializeComponent();
            if (match != null)
                SetLabels(0);
            ComboBoxMatchs();
        }
    }
}
