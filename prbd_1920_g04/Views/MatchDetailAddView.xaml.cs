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
        public String Team {
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

        public ICommand Delete { get; set; }

        private void SaveAction() {
            Match = Secretary.AddMatch(DateMatch, Place, Home, Adversary, Team);
            App.NotifyColleagues(AppMessages.MSG_MATCH_CHANGED, Match);
            App.NotifyColleagues(AppMessages.MSG_MATCH_SAVED, Home + "vs"+ Adversary);
            IsNew = false;
            var Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs);
            Console.WriteLine("Count of Matchs : " + Matchs.Count());
        }

        private void DeleteAction() {
            App.Model.Matchs.Remove(Match);
            App.Model.SaveChanges();
            App.NotifyColleagues(AppMessages.MSG_MATCH_DELETED);
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



        //Il faut ajouter des joueurs au team pour que la liste soit remplie. (condition ajouté, IsComplete() dans secretary)
        private void Refresh() {
            var tmp = new ObservableCollection<Model.Team>(App.Model.Teams.OrderBy(m => m.Name));
            Teams = new ObservableCollection<Model.Team>();
            foreach (var t in tmp)
            {
                if (t.IsComplete())
                {
                    Teams.Add(t);
                }
            }
            foreach (var t in Teams) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("Count of teams : " + Teams.Count());
        }
        //Il faut ajouter des joueurs au team pour que la liste soit remplie. (condition ajouté, IsComplete() dans secretary)




        public MatchDetailAddView() {
            DataContext = this;
            IsNew = true;
            Secretary = (Model.Secretary) App.CurrentUser;
            Refresh();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            Delete = new RelayCommand(DeleteAction);
            InitializeComponent();
            App.Register<Model.Match>(this, AppMessages.MSG_TEAM_CHANGED, match => { Refresh(); });
        }
    }
}
