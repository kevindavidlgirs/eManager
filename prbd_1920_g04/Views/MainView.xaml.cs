using PRBD_Framework;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace prbd_1920_g04
{
    public partial class MainView : WindowBase {

        public ICommand Logout { get; set; }

        public MainView() {
            InitializeComponent();
            
            DataContext = this;

            App.Register<Member>(this, AppMessages.MSG_DISPLAY_MEMBER, m => {
                TabOfMember(m, false);
            });

            Logout = new RelayCommand(LogoutAction);
        }

        private void TabOfMember(Member m, bool isNew) {
            foreach (TabItem t in tabControl.Items) {
                if (t.Header.ToString().Equals(m.Pseudo)) {
                    Dispatcher.InvokeAsync(() => t.Focus());
                    return;
                }
            }
            var tab = new TabItem() {
                Header = isNew ? "<new member>" : m.Pseudo,
                Content = new MemberDetailView(m)
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

        private void Login() {
            var loginView = new LoginView();
            Visibility = Visibility.Hidden;
            var res = loginView.ShowDialog();
            if (res == true) {
                Visibility = Visibility.Visible;
            }
            else {
                Close();
            }
        }

        private void LogoutAction() {
            App.CurrentUser = null;
            for (int i = tabControl.Items.Count - 1; i > 0; i--) 
                tabControl.Items.RemoveAt(i);
            Login();
        }
    }
}
