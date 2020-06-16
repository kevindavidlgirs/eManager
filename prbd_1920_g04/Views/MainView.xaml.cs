using PRBD_Framework;
using prbd_1920_g04.Model;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace prbd_1920_g04.Views {
    public partial class MainView : WindowBase {

        public ICommand Browse { get; set; }
        public ICommand NewMatch { get; set; }
        public ICommand NewPlayer { get; set; }
        public ICommand AddPlayerToAMatch { get; set; }
        public ICommand AddResultToMatch { get; set; }

        private string textHeaderTab;
        public string TextHeaderTab
        {
            get { return textHeaderTab; }
            set { textHeaderTab = value; }
        }

        private bool activeAddResultButton;
        public bool ActiveAddResultButton
        {
            get { return activeAddResultButton; }
            set
            {
                activeAddResultButton = value;
                RaisePropertyChanged(nameof(activeAddResultButton));
            }
        }
        private bool CanSaveOrCancelAction()
        {
            return activeAddResultButton;
        }
        
        public MainView() {

            InitializeComponent();
            DataContext = this;
            activeAddResultButton = false;

            var member = App.Model.Members.SingleOrDefault(s => s.Fonction == Fonction.Secretary); 
            App.CurrentUser = member; 

            App.Register<Model.Match>(this, AppMessages.MSG_ADD_STATS_TO_PLAYER, (match) => {
                foreach (TabItem t in tabControl.Items) {
                    if (t.Header.ToString().Equals("Stats: " + match.Home + " vs " + match.Adversary)) {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }

                var tab = new TabItem()
                {
                    Header = "Stats: " + match.Home + " vs " + match.Adversary,
                    Content = new AddPlayersStatistics(match),
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());

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
            });

            App.Register<Model.Match>(this, AppMessages.MSG_SHOW_MATCH, (match) => {
                foreach (TabItem t in tabControl.Items)
                {
                    if (t.Header.ToString().Equals("Show: " + match.Home + " vs "+ match.Adversary))
                    {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var tab = new TabItem()
                {
                    Header = "Show: " + match.Home + " vs " + match.Adversary,
                    Content = new MatchDetailView(match),
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());

                tab.MouseDown += (o, e) => {
                    if (e.ChangedButton == MouseButton.Middle &&
                        e.ButtonState == MouseButtonState.Pressed)
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
                tab.PreviewKeyDown += (o, e) => {
                    if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
            });

            App.Register<bool>(this, AppMessages.MSG_ADD_PLAYER_TO_A_TEAM, activeButton => {
                activeAddResultButton = activeButton;
            });

            App.Register<bool>(this, AppMessages.MSG_MATCH_IS_OVER, activeButton => {
                activeAddResultButton = activeButton;
                if (!activeAddResultButton)
                {
                    foreach (TabItem t0 in tabControl.Items)
                    {
                        if (t0.Header.ToString().Equals("Results"))
                        {
                            tabControl.Items.Remove(t0);
                            foreach (TabItem t1 in tabControl.Items)
                            {
                                Dispatcher.InvokeAsync(() => t1.Focus());
                                return;
                            }
                        }
                    }
                }
            });

            App.Register<bool>(this, AppMessages.MSG_REMOVE_PLAYER_TO_A_TEAM, activeButton =>
            {
                activeAddResultButton = activeButton;
                if (!activeAddResultButton)
                {
                    foreach (TabItem t in tabControl.Items)
                    {
                        if (t.Header.ToString().Equals("Results"))
                        {
                            tabControl.Items.Remove(t);
                            return;
                        }
                    }
                }
            });

            App.Register<string>(this, AppMessages.MSG_MATCH_SAVED, (s) => {
                (tabControl.SelectedItem as TabItem).Header = "Creation: " + s;
                textHeaderTab = "Creation: " + s;
            });

            NewMatch = new RelayCommand(() =>
            {
                {
                    var sec = App.Model.Secretaries.Find(113);
                    var match = App.Model.Matchs.Create();
                    foreach (TabItem t in tabControl.Items)
                    {
                        if (t.Header.ToString().Equals("<create match>") || t.Header.ToString().Equals(textHeaderTab))
                        {
                            Dispatcher.InvokeAsync(() => t.Focus());
                            return;
                        }
                    }
                    var tab = new TabItem()
                    {
                        Header = "<create match>",
                        Content = new MatchDetailAddView()
                    };

                    tabControl.Items.Add(tab);
                    Dispatcher.InvokeAsync(() => tab.Focus());

                    tab.MouseDown += (o, e) =>
                    {
                        if (e.ChangedButton == MouseButton.Middle &&
                            e.ButtonState == MouseButtonState.Pressed)
                        {
                            tabControl.Items.Remove(o);
                            (tab.Content as UserControlBase).Dispose();
                        }
                    };
                    tab.PreviewKeyDown += (o, e) =>
                    {
                        if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                        {
                            tabControl.Items.Remove(o);
                            (tab.Content as UserControlBase).Dispose();
                        }
                    };
                }
            });

            //TODO: Peaufiner cette partie
            Browse = new RelayCommand(() => 
            {
                foreach (TabItem t in tabControl.Items)
                {
                    Dispatcher.InvokeAsync(() => t.Focus());
                    return;
                }
            });
            //TODO: Peaufiner cette partie



            NewPlayer = new RelayCommand(() => 
            {
                foreach (TabItem t in tabControl.Items)
                {
                    if (t.Header.ToString().Equals("<create players>"))
                    {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var player = App.Model.Players.Create();
                var tab = new TabItem()
                {
                    Header = "<create players>",
                    Content = new PlayerDetailAddView()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());
                tab.MouseDown += (o, e) => {
                    if (e.ChangedButton == MouseButton.Middle &&
                        e.ButtonState == MouseButtonState.Pressed)
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
                tab.PreviewKeyDown += (o, e) => {
                    if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
            });

            AddPlayerToAMatch = new RelayCommand(() => 
            {
                foreach (TabItem t in tabControl.Items)
                {
                    if (t.Header.ToString().Equals("<Add player into a teams>"))
                    {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var tab = new TabItem()
                {
                    Header = "<Add player into a teams>",
                    Content = new AddPlayerIntoAMatchView()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());

                tab.MouseDown += (o, e) => {
                    if (e.ChangedButton == MouseButton.Middle &&
                        e.ButtonState == MouseButtonState.Pressed)
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
                tab.PreviewKeyDown += (o, e) => {
                    if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
            });

            AddResultToMatch = new RelayCommand(() =>
            {
                foreach (TabItem t in tabControl.Items)
                {
                    if (t.Header.ToString().Equals("Results"))
                    {
                        Dispatcher.InvokeAsync(() => t.Focus());
                        return;
                    }
                }
                var tab = new TabItem()
                {
                    Header = "Results",
                    Content = new MatchAddResult()
                };
                tabControl.Items.Add(tab);
                Dispatcher.InvokeAsync(() => tab.Focus());

                tab.MouseDown += (o, e) =>
                {
                    if (e.ChangedButton == MouseButton.Middle &&
                        e.ButtonState == MouseButtonState.Pressed)
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
                tab.PreviewKeyDown += (o, e) =>
                {
                    if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        tabControl.Items.Remove(o);
                        (tab.Content as UserControlBase).Dispose();
                    }
                };
            }, CanSaveOrCancelAction);
        }
    }
}

