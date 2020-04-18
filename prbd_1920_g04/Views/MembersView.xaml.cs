using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_1920_g04
{
    public partial class MembersView : UserControlBase {

        private ObservableCollection<Member> members;
        public ObservableCollection<Member> Members { get => members; set => SetProperty(ref members, value); }

        public ICommand DisplayMemberDetails { get; set; }

        public MembersView() {
            InitializeComponent();

            DataContext = this;

            DisplayMemberDetails = new RelayCommand<Member>(m => {
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_MEMBER, m);
            });

            Refresh();
        }

        private void Refresh() {
            Members = new ObservableCollection<Member>(App.Model.Members.OrderBy(m => m.Pseudo));
        }
    }
}
