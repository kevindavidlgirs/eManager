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

        public MatchDetailView(Model.Match match) {
            DataContext = this;
            Match = match;
            InitializeComponent();
        }
    }
}
