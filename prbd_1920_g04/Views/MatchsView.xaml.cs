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
        public ICommand NewPlayer { get; set; }
        public ICommand AddPlayerToAMatch { get; set; }
        public ICommand AddResultToMatch { get; set; }
        public MatchsView() {

            DataContext = this;

            DisplayMatchDetails = new RelayCommand<Model.Match>(m => {
                App.NotifyColleagues(AppMessages.MSG_SHOW_MATCH, m);
            });

            NewMatch = new RelayCommand(() => { App.NotifyColleagues(AppMessages.MSG_NEW_MATCH); });
            NewPlayer = new RelayCommand(() => { App.NotifyColleagues(AppMessages.MSG_NEW_PLAYER); });
            AddPlayerToAMatch = new RelayCommand(() => { App.NotifyColleagues(AppMessages.MSG_ADD_PLAYER_TO_A_TEAM); });
            AddResultToMatch = new RelayCommand(() => { App.NotifyColleagues(AppMessages.MSG_ADD_RESULT_TO_MATCH); });

            App.Register<Model.Match>(this, AppMessages.MSG_MATCH_CHANGED, match =>{ Refresh(); }); 
            Refresh();
            InitializeComponent();
        }

        private void Refresh() {
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs.OrderBy(m => m.DateMatch));
        }
    }
}
