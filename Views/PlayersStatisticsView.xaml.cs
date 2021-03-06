﻿using System;
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
using System.ComponentModel;
using System.Windows.Forms;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour PlayersStatisticsView.xaml
    /// </summary>
    public partial class PlayersStatisticsView : UserControlBase {
        public static readonly DependencyProperty PlayersStatsProperty = DependencyProperty.Register("Match", typeof(Match), typeof(PlayersStatisticsView));
        public Match Match {
            get => (Match)GetValue(PlayersStatsProperty);
            set => SetValue(PlayersStatsProperty, value);
        }

        private ObservableCollection<Player> listPlayers;
        public ObservableCollection<Player> ListPlayers {
            get {

                if (listPlayers == null) {
                    listPlayers = new ObservableCollection<Player>(Match.Teams);
                    foreach(var p in listPlayers)
                    {
                        p.MatchForCreatePlayersStatsView = Match;
                    }
                }

                return listPlayers;
            }

            set {
                listPlayers = value;
                //RaisePropertyChanged(nameof(listMatchs));
                RaisePropertyChanged(nameof(ListPlayers));
            }
        }

        private CollectionView listPlayersView;
        public CollectionView ListPlayersView {
            get {
                listPlayersView = (CollectionView)CollectionViewSource.GetDefaultView(ListPlayers);
                return listPlayersView;
            }
        }

        public void eventGestion()
        {
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent, new MouseButtonEventHandler(Row_DoubleClick)));
            dataGrid.RowStyle = rowStyle;
        }
        
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            App.NotifyColleagues(AppMessages.MSG_VIEW_PLAYER, (Player)row.Item);
        }

        public PlayersStatisticsView() {
            InitializeComponent();
            DataContext = this;
            eventGestion();
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            if (Match != null) {
                ListPlayers = new ObservableCollection<Player>(Match.Teams);
            }
            RaisePropertyChanged(nameof(ListPlayersView));
        }
    }
}
