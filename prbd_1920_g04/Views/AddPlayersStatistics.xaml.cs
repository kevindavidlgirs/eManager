using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRBD_Framework;
using prbd_1920_g04.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour AddPlayersStatistics.xaml
    /// </summary>
    public partial class AddPlayersStatistics : UserControlBase {
        public Match Match { get; set; }

        private int GoalsAvailable { get; set; }

        private ObservableCollection<Player> listOfPlayers;
        public ObservableCollection<Player> ListOfPlayers { get => listOfPlayers; set => SetProperty(ref listOfPlayers, value); }
        public ICommand UpdateStats { get; set; }
        public ICommand GoToMatch { get; set; }

        private int goalsScored {get; set;}
        public int GoalsScored {
            get {
                return goalsScored;
            }

            set {
                goalsScored = value;
                RaisePropertyChanged(nameof(GoalsScored));
            }
        }

        public int GoalsConceced {
            set {
                RaisePropertyChanged(nameof(GoalsConceced));
            }
        }

        public int Assists {
            set {
                RaisePropertyChanged(nameof(Assists));
            }
        }

        public int Injuries {
            set {
                RaisePropertyChanged(nameof(Injuries));
            }
        }

        public int Fouls {
            set {
                RaisePropertyChanged(nameof(Fouls));
            }
        }

        private ICollection<Player> QualifiedPlayers(Match match)
        {
            return match.Teams;
        }

        private bool StatsExiste(Player p)
        {
            foreach (var stats in p.StatsList)
            {
                if (stats.Match.Equals(p.Stats.Match))
                {
                    //A voir si laisser là
                    stats.copyAttr(p.Stats);
                    //A voir si laisser là
                    return true;
                }
            }
            return false;
        }

        private void UpdateAction(Player p) {
            if (GoalsAvailable >= p.Stats.GoalsScored) {
                GoalsAvailable -= p.Stats.GoalsScored;
                p.Stats.Match = Match;
                p.Stats.Player = p;
                if (!StatsExiste(p))
                {
                    Statistics s = new Statistics(p.Stats);
                    p.StatsList.Add(s);
                }
                SetLabel();
            }
        }
        private bool CanSaveOrCancelAction(Player p)
        {
            if(p != null)
            {
                return p.Stats.GoalsScored <= GoalsAvailable;
            }
            return false;
        }
        private bool CanTransferOrCancelAction()
        {
            return GoalsAvailable == 0;
        }
        private void TransfertAction() {
            foreach(Player p in Match.Teams)
            {
                p.Stats = new Statistics();
            }

            Match.CanSelectPlayer = false; // Supprimer l'icone dans MatchsView
            App.NotifyColleagues(AppMessages.MSG_MATCH_ADDED); // Pour rafraichir la liste des joueurs !
            App.NotifyColleagues(AppMessages.MSG_REMOVE_STATS_PLAYERS_TAB, Match); // Nous supprimons automatiquement la vue addPlayerStatisticsView
            App.NotifyColleagues(AppMessages.MSG_REMOVE_SELECT_PLAYERS_TAB, Match); // Nous supprimons automatiquement la vue addPlayerIntoTeam
            App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, Match);
        }

        private void SetLabel()
        {
            goalsAvailableLabel.Content = "You can assign " + GoalsAvailable + " goals.";
        }

        public AddPlayersStatistics(Match match) {
            DataContext = this;
            Match = match;
            GoalsAvailable = match.GoalsHome;
            ListOfPlayers = new ObservableCollection<Player>(QualifiedPlayers(match));
            UpdateStats = new RelayCommand<Player>((p) => {UpdateAction(p); }, (p) => CanSaveOrCancelAction(p));
            GoToMatch = new RelayCommand(TransfertAction, CanTransferOrCancelAction);
            InitializeComponent();
            SetLabel();

        }
    }
}
