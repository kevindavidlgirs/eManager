using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using prbd_1920_g04.Model;

namespace prbd_1920_g04.Views
{
    public partial class MatchsView : UserControlBase
    {

        private ObservableCollection<Match> matchs;
        public ObservableCollection<Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }
        public ICommand DisplayMatchDetails { get; set; }

        private void Refresh()
        {
            Matchs = new ObservableCollection<Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
        }

        public MatchsView()
        {
            DataContext = this;
            Refresh();

            DisplayMatchDetails = new RelayCommand<Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });

            App.Register(this, AppMessages.MSG_MATCH_CHANGED, () => { Refresh(); });
            InitializeComponent();
        }
    }
}
