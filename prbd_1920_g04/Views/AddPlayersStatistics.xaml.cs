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

        private ObservableCollection<Player> listOfPlayers;
        public ObservableCollection<Player> ListOfPlayers { get => listOfPlayers; set => SetProperty(ref listOfPlayers, value); }
        public ICommand UpdateStats { get; set; }

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

        private ICollection<Player> QualifiedPlayers(Match match) {
            return match.Players;
        }

        private void UpdateAction() {
            App.Model.SaveChanges();
        }

        private bool CanSaveOrCancelAction() {
            //TODO : Faire en sorte que le bouton s'active et se désactive au bon moment.
            return true;
        }

        public AddPlayersStatistics(Match match) {
            DataContext = this;
            Match = match;
            InitializeComponent();

            ListOfPlayers = new ObservableCollection<Player>(QualifiedPlayers(match));

            foreach(var m in ListOfPlayers) {
                Console.WriteLine(m.Stats.Injuries);
            }
            UpdateStats = new RelayCommand<Player>((p) => { UpdateAction(); }, (p) => CanSaveOrCancelAction());
        }
    }
}
