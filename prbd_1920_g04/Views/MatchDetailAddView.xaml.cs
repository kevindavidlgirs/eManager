using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using PRBD_Framework;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour MatchDetailAdd.xaml
    /// </summary>
    public partial class MatchDetailAddView : UserControlBase {
        public Model.Match Match { get; set; }
        public Model.Secretary Secretary {get; set;}

        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

        private ObservableCollection<Model.Team> teams;
        public ObservableCollection<Model.Team> Teams { get => teams; set => SetProperty(ref teams, value); }

        //Binding des TextBox START
        private string home;
        public string Home {
            get {
                return home;
            }

            set {
                home = value;
                RaisePropertyChanged(nameof(Home));
            }
        }

        private string adversary;
        public string Adversary {
            get {
                return adversary;
            }

            set {
                adversary = value;
                RaisePropertyChanged(nameof(Adversary));
            }
        }

        //(n'est pas un textBox)
        private DateTime dateMatch = DateTime.Now;
        public DateTime DateMatch {
            get {
                return dateMatch;
            }

            set {
                dateMatch = value;
                RaisePropertyChanged(nameof(DateMatch));
            }
        }

        private string place;
        public string Place {
            get {
                return place;
            }

            set {
                place = value;
                RaisePropertyChanged(nameof(Place));
            }
        }

        private string team;
        public string Team {
            get {
                return team;
            }

            set {
                team = value;
                RaisePropertyChanged(nameof(Team));
            }
        }
        //Binding des TextBox END

        private bool isNew; 
        public bool IsNew {
            get { return isNew; }
            set {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        public ICommand Save { get; set; }

        private void SaveAction() {
            var query = from match in Matchs
                        where match.Home.Equals(this.Home) && match.Adversary.Equals(this.adversary)
                        select match;
            if(query.Count() == 0)
            {
                Match = Secretary.AddMatch(DateMatch, Place, Home, Adversary, Team);
                App.NotifyColleagues(AppMessages.MSG_MATCH_ADDED);
                App.NotifyColleagues(AppMessages.MSG_MATCH_SAVED, Home + " vs " + Adversary);
                IsNew = false;
                Console.WriteLine("Count of Matchs : " + Matchs.Count());
            }
            
        }

        private bool CanSaveOrCancelAction() {
            if (IsNew) {
                return !(DateMatch == null && HasErrors);
            }
            var change = (from c in App.Model.ChangeTracker.Entries<Model.Match>()
                          where c.Entity == Match
                          select c).FirstOrDefault();
            return change != null && change.State != EntityState.Unchanged;
        }

        private void Refresh() {
            Teams = new ObservableCollection<Model.Team>(App.Model.Teams.OrderBy(m => m.Name));
            foreach (var t in Teams) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("Count of teams : " + Teams.Count());
        }

        public MatchDetailAddView() {
            DataContext = this;
            IsNew = true;
            Secretary = (Model.Secretary) App.CurrentUser;
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs);
            Refresh();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            App.Register<Model.Match>(this, AppMessages.MSG_TEAM_CHANGED, match => { Refresh(); });
            InitializeComponent();
        }
    }
}

