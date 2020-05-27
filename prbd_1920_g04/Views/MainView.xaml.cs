using PRBD_Framework;
using prbd_1920_g04.Model;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace prbd_1920_g04.Views {
    public partial class MainView : WindowBase {
        public MainView() {
            InitializeComponent();
            DataContext = this;
            
            //Cette partie doit à terme aller dans le loginView.
            var member = App.Model.Members.SingleOrDefault (s => s.Fonction == Fonction.Secretary); // on recherche un secrétaire pour les tests
            App.CurrentUser = member; // le membre connecté devient le membre courant

            App.Register<Model.Match>(this, AppMessages.MSG_SHOW_MATCH, (match) => {
                foreach (TabItem t in tabControl.Items) {
                    if (t.Header.ToString().Equals(match.DateMatch)) {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var tab = new TabItem() {
                    Header = match.Home + " vs " + match.Adversary,
                    Content = new MatchDetailView(match),
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
            });

            App.Register(this, AppMessages.MSG_NEW_MATCH, () => {
                // Nous récupérons un secrétaire pour les tests, à terme une condition devra vérifier que le user connecté est un sectétaire
                var sec = App.Model.Secretaries.Find(113); 
                var match = App.Model.Matchs.Create();
                foreach (TabItem t in tabControl.Items) {
                    if (t.Header.ToString().Equals(match.DateMatch)) {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var tab = new TabItem() {
                    Header = "<new match>",
                    Content = new MatchDetailAddView()
                };

                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
            });

            App.Register(this, AppMessages.MSG_NEW_PLAYER, () =>
            {
                var tab = new TabItem()
                {
                    //Mise à jour de l'onglet lors de la sauvegarde (à implémenter)
                    Header = "<new player>",
                    Content = new PlayerDetailAddView()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
            });

            App.Register(this, AppMessages.MSG_CANCEL_ADD_PLAYER, () => {
                //tabControl.Items.Remove(...);
            });

            App.Register(this, AppMessages.MSG_ADD_PLAYER_TO_A_TEAM, () =>
            {
                var tab = new TabItem()
                {
                    //Mise à jour de l'onglet lors de la sauvegarde (à implémenter)
                    Header = "<Add player into a team>",
                    Content = new AddPlayerIntoAMatchView()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
            });

            App.Register(this, AppMessages.MSG_ADD_RESULT_TO_MATCH, () => {
                var tab = new TabItem() {
                    Header = "Result",
                    Content = new MatchAddResult()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
            });

            App.Register<string>(this, AppMessages.MSG_MATCH_SAVED, (s) => {
                (tabControl.SelectedItem as TabItem).Header = s;
            });
        }
        
           /* private void tabOfMatch(Model.Match m, bool isNew) {
                foreach (TabItem t in tabControl.Items) {
                    if (t.Header.ToString().Equals(m.DateMatch)) {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }

                var tab = new TabItem() {
                    Header = isNew ? "<new match>" : m.Adversary+"vs"+m.Home,
                    Content = new MatchDetailView(m)
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
            }*/
        }
}
