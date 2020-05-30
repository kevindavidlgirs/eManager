using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using PRBD_Framework;
using prbd_1920_g04.Model;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class MatchDetailListView : UserControlBase {
        public static readonly DependencyProperty MatchListProperty = DependencyProperty.Register("Match", typeof(Match), typeof(MatchDetailListView));
        public Match Match {
            get => (Match)GetValue(MatchListProperty);
            set => SetValue(MatchListProperty, value);
        }

        private ObservableCollection<Match> listMatchs;
        public ObservableCollection<Match> ListMatchs {
            get {

                //listMatchs = new ObservableCollection<Match>(App.Model.Matchs.Where(m => m.Home.Equals(Match.Home) && m.Adversary.Equals(Match.Adversary)).OrderBy(m => m.DateMatch));
                listMatchs = new ObservableCollection<Model.Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
                return listMatchs;
            }
            set {
                listMatchs = value;
                RaisePropertyChanged(nameof(listMatchs));
                RaisePropertyChanged(nameof(listMatchsView));
            }
        }

        private CollectionView listMatchsView;
        public CollectionView ListMatchsView {
            get {
                listMatchsView = (CollectionView)CollectionViewSource.GetDefaultView(ListMatchs);
                return listMatchsView;
            }
        }


        public MatchDetailListView() {
            InitializeComponent();
            DataContext = this;
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            ListMatchs = new ObservableCollection<Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
           
        }
    }
}
