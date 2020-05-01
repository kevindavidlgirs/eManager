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

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour MatchDetailAdd.xaml
    /// </summary>
    public partial class MatchDetailAdd : UserControl {
        public Model.Match Match { get; set; }
        public MatchDetailAdd(Model.Match match) {
            DataContext = this;
            Match = match;
            InitializeComponent();
        }
    }
}
