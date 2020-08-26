using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_1920_g04
{
    public partial class MemberDetailView : UserControlBase {

        public Member Member { get; set; }

        public MemberDetailView(Member member) {
            InitializeComponent();
            DataContext = this;
            Member = member;
        }
    }
}
