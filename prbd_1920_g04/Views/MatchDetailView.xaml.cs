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

        public int lossesAdversary;
        public int LossesAdversary {
            get {
                return lossesAdversary;
            }

            set {
                lossesAdversary = value;
                RaisePropertyChanged(nameof(LossesAdversary));
            }
        }

               public int drawsHome;
               public int DrawsHome {
                   get {
                       return drawsHome;
                   }

                   set {
                       drawsHome = value;
                       RaisePropertyChanged(nameof(DrawsHome));
                   }
               }

               public int drawsAdversary;
               public int DrawsAdversary {
                   get {
                       return drawsAdversary;
                   }

                   set {
                       drawsAdversary = value;
                       RaisePropertyChanged(nameof(DrawsAdversary));
                   }
               }
        
       private void getNumberOfHomeVictories() {
           var nbVictory = (from match in App.Model.Matchs
                            where match.Home.Equals(Home) && match.GoalsHome > match.GoalsAdversary
                            && match.IsOver == true
                            select match).Count();
           WinsHome = nbVictory;
       }

       private void getNumberOfAdversaryVictories() {
           var nbVictory = (from match in App.Model.Matchs
                            where match.Adversary.Equals(Adversary) && match.GoalsAdversary > match.GoalsHome
                            && match.IsOver == true
                            select match).Count();
           WinsAdversary = nbVictory;
       }


       private void getNumberOfHomeDefeats() {
           var nbDefeat = (from match in App.Model.Matchs
                            where match.Home.Equals(Home) && match.GoalsHome < match.GoalsAdversary
                            && match.IsOver == true
                           select match).Count();
           LossesHome = nbDefeat;
       }


       private void getNumberOfAdversaryDefeats() {
           var nbDefeat = (from match in App.Model.Matchs
                           where match.Adversary.Equals(Adversary) && match.GoalsAdversary < match.GoalsHome
                           && match.IsOver == true
                           select match).Count();
           LossesAdversary = nbDefeat;
       }

           private void getNumberOfHomeDraws() {
               var nbDraws = (from match in App.Model.Matchs
                               where match.Home.Equals(Home) && match.GoalsAdversary == match.GoalsHome
                               && match.IsOver == true
                              select match).Count();
               DrawsHome = nbDraws;
           }

          private void getNumberOfAdversaryDraws() {
              var nbDraws = (from match in App.Model.Matchs
                              where match.Adversary.Equals(Adversary) && match.GoalsAdversary == match.GoalsHome
                              && match.IsOver == true
                             select match).Count();
           DrawsAdversary = nbDraws;
          }

        public MatchDetailView(Model.Match match) {
            DataContext = this;
            Match = match;
            getNumberOfHomeVictories();
            getNumberOfAdversaryVictories();
            getNumberOfHomeDefeats();
            getNumberOfAdversaryDefeats();
            getNumberOfHomeDraws();
            getNumberOfAdversaryDraws();
            InitializeComponent();
        }
    }
}
