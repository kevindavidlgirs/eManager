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

namespace prbd_1920_g04.Views {

    public partial class MatchDetailView : UserControlBase {
        public Model.Match Match { get; set; }

        public string Home {
            get {
                return Match.Home;
            }

            set {
                RaisePropertyChanged(nameof(Match.Home));
            }
        }

        public string Adversary {
            get {
                return Match.Adversary;
            }
             set {
                RaisePropertyChanged(nameof(Match.Adversary));
            }
        }

        public DateTime Date {
            get {
                return Match.DateMatch;
            }

            set {
                RaisePropertyChanged(nameof(Match.DateMatch));
            }
        }

        public int GoalsHome {
            get {
                return Match.GoalsHome;
            }

            set {
                RaisePropertyChanged(nameof(Match.GoalsHome));
            }
        }

        public int GoalsAdversary {
            get {
                return Match.GoalsAdversary;
            }

            set {
                RaisePropertyChanged(nameof(Match.GoalsAdversary));
            }
        }

        public int winsHome;
        public int WinsHome {
            get {
                return winsHome;
            }

            set {
                winsHome = value;
                RaisePropertyChanged(nameof(WinsHome));
            }
        }

        public int lossesHome;
        public int LossesHome {
            get {
                return lossesHome;
            }

            set {
                lossesHome = value;
                RaisePropertyChanged(nameof(LossesHome));
            }
        }

        public int winsAdversary;
        public int WinsAdversary {
            get {
                return winsAdversary;
            }

            set {
                winsAdversary = value;
                RaisePropertyChanged(nameof(WinsAdversary));
            }
        }


        private void getNumberOfVictory(string squad) {
            var nbVictory = (from match in App.Model.Matchs
                             where match.Home.Equals(squad) && match.GoalsHome > match.GoalsAdversary
                             select match).Count();
            WinsHome = nbVictory;
        }

        private void getNumberOfDefeat(string squad) {
            var nbDefeat = (from match in App.Model.Matchs
                             where match.Home.Equals(squad) && match.GoalsHome < match.GoalsAdversary
                             select match).Count();
            LossesHome = nbDefeat;
        }

        public MatchDetailView(Model.Match match) {
            DataContext = this;
            Match = match;
            getNumberOfVictory(Match.Home);
            getNumberOfDefeat(Match.Home);

            //getNumberOfVictory(Match.Adversary);
            InitializeComponent();
        }
    }
}
