using PRBD_Framework;
using prbd_1920_g04.Model;
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
using System.Collections.ObjectModel;

namespace prbd_1920_g04.Views {
    public partial class MatchAddResult : UserControlBase {
        private ObservableCollection<Match> playedMatchs;
        public ObservableCollection<Match> PlayedMatchs { get => playedMatchs; set => SetProperty(ref playedMatchs, value); }
        public ICommand UpdateMatch { get; set; }

        public int GoalsHome {
            set {
                RaisePropertyChanged(nameof(GoalsHome));
            }
        }

        public int GoalsAdversary {
            set {
                RaisePropertyChanged(nameof(GoalsAdversary));
            }
        }

        private void UpdateAction(Match m) {
            Console.WriteLine(m.Home + "vs" + m.Adversary);
            Console.WriteLine(m.GoalsHome + "vs" + m.GoalsAdversary);
            m.IsOver = true;
            App.Model.SaveChanges();
            App.NotifyColleagues(AppMessages.MSG_ADD_STATS_TO_PLAYER, m);
            Refresh();
        }
        private void Refresh() {
            var matchs = new ObservableCollection<Match>(App.Model.Matchs);
            PlayedMatchs = new ObservableCollection<Match>();
            foreach (var m in matchs)
            {
                if (m.IsComplete && !m.IsOver)
                {
                    PlayedMatchs.Add(m);
                }
            }
        }

        public MatchAddResult() {
            DataContext = this;
            UpdateMatch = new RelayCommand<Match>((m) => { UpdateAction(m); });
            Refresh();
            InitializeComponent();

        }
    }
}
