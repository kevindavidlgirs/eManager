using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using prbd_1920_g04.Model;
using System.Windows.Data;
using System.Globalization;

namespace prbd_1920_g04.Views
{
    public partial class MatchsView : UserControlBase
    {

        private ObservableCollection<Match> matchs;
        public ObservableCollection<Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }
        public ICommand DisplayMatchDetails { get; set; }

        private Match selectedMatch;
        public Match SelectedMatch {
            get { return selectedMatch; }
            set {
                selectedMatch = value;
                RaisePropertyChanged(nameof(SelectedMatch));
                selectPlayerForMatch();
            }
        }

        private void selectPlayerForMatch() {
            if (SelectedMatch != null) {
                if (SelectedMatch.CanSelectPlayer) {
                    Console.WriteLine(SelectedMatch.DateMatch);
                    App.NotifyColleagues(AppMessages.MSG_CAN_SELECT_PLAYERS_FOR_MATCH, SelectedMatch);
                }
            }
        }

        private void SetCanSelectPlayer() {
            foreach (var match in App.Model.Matchs) {
                if (match.Category.Players.Count >= 5) {
                    match.CanSelectPlayer = true;
                }
            }
            Refresh();
        }


        private void Refresh()
        {
            Matchs = new ObservableCollection<Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
        }

        public MatchsView()
        {
            DataContext = this;
            SetCanSelectPlayer();
            //Refresh();
            SelectedMatch = null;

            DisplayMatchDetails = new RelayCommand<Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });

            App.Register(this, AppMessages.MSG_UPDATE_SELECT_PLAYERS_FOR_MATCH, () => { SetCanSelectPlayer(); });
            App.Register(this, AppMessages.MSG_MATCH_ADDED, () => { Refresh(); });

            InitializeComponent();
        }
    }

    public class ToUpperValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var str = value as string;
            return string.IsNullOrEmpty(str) ? string.Empty : str.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
