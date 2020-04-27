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
using System.Windows.Shapes;

namespace prbd_1920_g04.Views {
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();
        }
        private void tabOfMember(Model.Match m, bool isNew) {
            foreach (TabItem t in tabControl.Items) {
                if (t.Header.ToString().Equals(m.Date)) {
                    Dispatcher.InvokeAsync(() => t.Focus());
                    return;
                }
            }
            var tab = new TabItem() {
                Header = isNew ? "<new member>" : m.Pseudo,
                Content = new MemberDetailView(m, isNew)
            };
            tabControl.Items.Add(tab);
            tab.MouseDown += (o, e) => {
                if (e.ChangedButton == MouseButton.Middle &&
                    e.ButtonState == MouseButtonState.Pressed) {
                    tabControl.Items.Remove(o);
                    (tab.Content as UserControlBase).Dispose();
                }
            };
            tab.PreviewKeyDown += (o, e) => {
                if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                    tabControl.Items.Remove(o);
                    (tab.Content as UserControlBase).Dispose();
                }
            };
            // exécute la méthode Focus() de l'onglet pour lui donner le focus (càd l'activer)
            Dispatcher.InvokeAsync(() => tab.Focus());
        }
    }
}
