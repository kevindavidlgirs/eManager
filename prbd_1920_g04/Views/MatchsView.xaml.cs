using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_1920_g04.Views
{
    public partial class MatchsView : UserControlBase {

        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

        public ICommand DisplayMatchDetails { get; set; }
        public ICommand NewMatch { get; set; }

        public MatchsView() {

            DataContext = this;
            
            DisplayMatchDetails = new RelayCommand<Model.Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });

            NewMatch = new RelayCommand(() => { App.NotifyColleagues(AppMessages.MSG_NEW_MATCH); });

            Refresh();
            InitializeComponent();
        }

        private void Refresh() {
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs.OrderBy(m => m.Date));
        }
    }
}
