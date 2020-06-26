using prbd_1920_g04.Model;
using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

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
            set { matchSelected = value; }
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
            }
            if (matchSelected.NumberOfPlayers() >= 11)
            {
                matchSelected.TeamIsCompete = true;
                App.Model.SaveChanges();
            }
            CheckedListBoxPlayersAvalaible();
            CheckedListBoxPlayersAdded();
            SetLabels(0);
            App.NotifyColleagues(AppMessages.MSG_ADD_PLAYER_TO_A_TEAM, new ObservableCollection<Match>(App.Model.Matchs.Where(m => (m.TeamIsCompete && !m.IsOver))).Count > 0);
        }

        private void RemoveAction()
        {
            foreach (var player in checkListBoxPlayerSelected.SelectedItems)
            {
                Player p = (Player)player;
                Secretary.RemovePlayerInMatchs(p.Id, matchSelected);
            }
            if (matchSelected.NumberOfPlayers() < 11)
            {
                matchSelected.TeamIsCompete = false;
                App.Model.SaveChanges();
            }
            CheckedListBoxPlayersAdded();
            CheckedListBoxPlayersAvalaible();
            SetLabels(0);
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
   
                if(matchSelected.Category.Players.Contains(p) && !matchSelected.Teams.Contains(p))
                {
                    PlayersAvalaible.Add(p);
                }
            }
            activeButtonSave = PlayersAvalaible.Count > 0 && !matchSelected.TeamIsCompete;
            checkListBoxAddPlayer.SelectedItems.Clear();
        }

        private void CheckedListBoxPlayersAdded()
        {
            var players = new ObservableCollection<Player>(App.Model.Players);
            PlayersAdded = new ObservableCollection<Player>();
            foreach (var p in players)
            {

                if (matchSelected.Category.Players.Contains(p) && matchSelected.Teams.Contains(p))
                {
                    PlayersAdded.Add(p);
                }
            }
            activeButtonRemove = PlayersAdded.Count > 0 && !matchSelected.IsOver;
            checkListBoxPlayerSelected.SelectedItems.Clear();
        }

        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            foreach (var m in Matchs)
            {
                if (m.Equals((Match)comboboxMatchs.SelectedItem))
                {
                    matchSelected = m;
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
            if (checkListBoxAddPlayer.SelectedItems.Count > 11 || matchSelected.Teams.Count > 11 || (checkListBoxAddPlayer.SelectedItems.Count + matchSelected.Teams.Count) > 11)
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
            if (matchSelected != null && matchSelected.NumberOfPlayers() == 11)
            {
                checkListLeft.Content = "The match is complete !";
            }
            else
            {
                checkListLeft.Content = "This match still has at least " + (11 - matchSelected.NumberOfPlayers()) + " places -  you have selected " + playerSelected + " players.";
            }
            categorie.Content = "Catégorie " + matchSelected.Category.Name + " players avalaible : " + (matchSelected.Category.Players.Count - matchSelected.NumberOfPlayers());
            dateOfMeeting.Content = "Date of meeting : " + matchSelected.DateMatch.Day + " /" + matchSelected.DateMatch.Month;
        }

        private void ResetAll()
        {
            checkListLeft.Content = "";
            categorie.Content = "";
            dateOfMeeting.Content = "Please select a team.";
            PlayersAvalaible = null; PlayersAdded = null; activeButtonSave = false; activeButtonRemove = false;
        }

        public AddPlayerIntoAMatchView()
        {
            DataContext = this;
            Secretary = (Secretary)App.CurrentUser;
            ComboBoxMatchs();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            Remove = new RelayCommand(RemoveAction, CanRemoveOrCancelAction);
            App.Register<bool>(this, AppMessages.MSG_MATCH_IS_OVER, o => { ComboBoxMatchs(); ResetAll(); comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_MATCH_ADDED, () => { ComboBoxMatchs(); ResetAll(); comboboxMatchs.SelectedIndex = -1; });
            App.Register(this, AppMessages.MSG_PLAYER_ADDED, () => { if(matchSelected != null)CheckedListBoxPlayersAvalaible(); if (matchSelected != null) SetLabels(0); });
            InitializeComponent();
        }
    }
}
