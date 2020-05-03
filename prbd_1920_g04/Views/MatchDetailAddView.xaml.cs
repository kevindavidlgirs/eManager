using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRBD_Framework;

namespace prbd_1920_g04.Views {
    /// <summary>
    /// Logique d'interaction pour MatchDetailAdd.xaml
    /// </summary>
    public partial class MatchDetailAddView : UserControlBase {
        public Model.Match Match { get; set; }
        public Model.Secretary Secretary {get; set;}
        private string home;
        private string adversary;
        private DateTime dateMatch = DateTime.Now;
        private string place;
        private string team;

        private ObservableCollection<Model.Team> teams;
        public ObservableCollection<Model.Team> Teams { get => teams; set => SetProperty(ref teams, value); }


        public string Home {
            get {
                return home;
            }

            set {
                home = value;
                RaisePropertyChanged(nameof(Home));
            }
        }

        public string Adversary {
            get {
                return adversary;
            }

            set {
                adversary = value;
                RaisePropertyChanged(nameof(Adversary));
            }
        }

        public DateTime DateMatch {
            get {
                return dateMatch;
            }

            set {
                dateMatch = value;
                RaisePropertyChanged(nameof(DateMatch));
            }
        }

        public string Place {
            get {
                return place;
            }

            set {
                place = value;
                RaisePropertyChanged(nameof(Place));
            }
        }

        public String Team {
            get {
                return team;
            }

            set {
                team = value;
                RaisePropertyChanged(nameof(Team));
            }
        }

        private bool isNew; // Pour le bouton save
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
            IsNew = false;
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
            //Match = match;
            Refresh();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            Delete = new RelayCommand(DeleteAction);
            InitializeComponent();
        }
    }
}
