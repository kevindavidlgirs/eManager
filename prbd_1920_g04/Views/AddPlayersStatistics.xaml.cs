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

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour AddPlayersStatistics.xaml
    /// </summary>
    public partial class AddPlayersStatistics : UserControlBase {
        public Match Match { get; set; }
        private ObservableCollection<Player> listOfPlayers;
        public ObservableCollection<Player> ListOfPlayers { get => listOfPlayers; set => SetProperty(ref listOfPlayers, value); }

        private ICollection<Player> QualifiedPlayers(Match match) {
            var query = from p in App.Model.Players
                        where match.PlayersId.Contains(p.Id)
                        select p;

                return query.ToList();
        }

        public AddPlayersStatistics(Match match) {
            Match = match;
            InitializeComponent();
            listOfPlayers = new ObservableCollection<Player>(QualifiedPlayers(match));

            foreach(var m in listOfPlayers) {
                Console.WriteLine(m);
            }
        }
    }
}
