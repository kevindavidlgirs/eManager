using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_1920_g04.Views
{
    public partial class MatchsView : UserControlBase
    {

        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }
        public ICommand DisplayMatchDetails { get; set; }
        private void Refresh()
        {
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
        }

        public MatchsView()
        {
            DataContext = this;
            Refresh();
            DisplayMatchDetails = new RelayCommand<Model.Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });
            App.Register(this, AppMessages.MSG_MATCH_ADDED, () => { Refresh(); });
            InitializeComponent();
        }
    }
}
