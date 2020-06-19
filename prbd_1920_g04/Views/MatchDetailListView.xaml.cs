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
        public ICommand DisplayOtherMatchDetails { get; set; }
        public static readonly DependencyProperty MatchListProperty = DependencyProperty.Register("Match", typeof(Match), typeof(MatchDetailListView));
        public Match Match {
            get => (Match)GetValue(MatchListProperty);
            set => SetValue(MatchListProperty, value);
        }

        private Match selectedMatch;
        public Match SelectedMatch {
            get {
                return selectedMatch;
            }

            set {
                selectedMatch = value;
                RaisePropertyChanged(nameof(SelectedMatch));
            }
        }

        private ObservableCollection<Match> listMatchs;
        public ObservableCollection<Match> ListMatchs {
            get {

                if (listMatchs == null) {
                    listMatchs = new ObservableCollection<Match>(ListOfSameMatches(Match));
                }
               
                return listMatchs;
            }
            set {
                listMatchs = value;
                //RaisePropertyChanged(nameof(listMatchs));
                RaisePropertyChanged(nameof(ListMatchsView));
            }
        }

        private CollectionView listMatchsView;
        public CollectionView ListMatchsView {
            get {
                listMatchsView = (CollectionView)CollectionViewSource.GetDefaultView(ListMatchs);
                return listMatchsView;
            }
        }

        private static ICollection<Match> ListOfSameMatches(Match match) {
            var query = (from m in App.Model.Matchs
                             where m.Home.Equals(match.Home) && 
                             m.Adversary.Equals(match.Adversary) &&
                             match.DateMatch != m.DateMatch
                             orderby m.DateMatch descending
                         select m);
            return query.ToList();
        }


        public MatchDetailListView() {
            InitializeComponent();
            DataContext = this;
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            if (Match != null) {
                ListMatchs = new ObservableCollection<Match>(ListOfSameMatches(Match));
            }

            RaisePropertyChanged(nameof(ListMatchsView));

            DisplayOtherMatchDetails = new RelayCommand<Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });
        }
    }
}
