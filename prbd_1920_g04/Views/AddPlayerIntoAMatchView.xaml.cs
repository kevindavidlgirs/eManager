using PRBD_Framework;
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

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPlayerIntoATeamView.xaml
    /// </summary>
    public partial class AddPlayerIntoAMatchView : UserControlBase
    {
        public Model.Secretary Secretary { get; set; }

        private ObservableCollection<Model.Player> players;
        public ObservableCollection<Model.Player> Players { get => players; set => SetProperty(ref players, value); }
        
        private ObservableCollection<Model.Match> matchs;
        public ObservableCollection<Model.Match> Matchs { get => matchs; set => SetProperty(ref matchs, value); }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                //RaisePropertyChanged(value);
            }
        }

        private string mtchs;
        public string Mtchs
        {
            get { return mtchs; }
            set
            {
                mtchs = value;
                RaisePropertyChanged(nameof(Mtchs));
            }
        }
        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; }

        private void SaveAction()
        {
            if(Secretary.AddPlayerInMatchs(id, mtchs))
            {
                IsNew = false;
                ComboBoxMatchs();
                ComboBoxPlayers();
                App.NotifyColleagues(AppMessages.MSG_TEAM_CHANGED);
            }
        }

        private void CancelAction()
        {
        }

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        private bool CanSaveOrCancelAction()
        {
            if (IsNew)
            {
                return !(Players.Count() == 0 && Matchs.Count() == 0 && HasErrors);
            }
            //var change = (from c in App.Model.ChangeTracker.Entries<Model.Match>()
            //              where c.Entity == Match
            //              select c).FirstOrDefault();                             //Je dois comprendre cette partie (kevin)
            //return change != null && change.State != EntityState.Unchanged;
            return false; 
        }

        private void ComboBoxMatchs()
        {
            Matchs = new ObservableCollection<Model.Match>(App.Model.Matchs);
            Console.WriteLine(Matchs);
        }

        private void ComboBoxPlayers()
        {
            var Plys = new ObservableCollection<Model.Player>(App.Model.Players.OrderBy(p => p.LastName));
            Players = new ObservableCollection<Model.Player>();
            foreach (var p in Plys)
            {
                if(p.TeamName == null)
                {
                    Players.Add(p);
                }
            }
        } 
        public AddPlayerIntoAMatchView()
        {
            DataContext = this;
            IsNew = true;
            Secretary = (Model.Secretary)App.CurrentUser;
            ComboBoxMatchs();
            ComboBoxPlayers();
            Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            //Cancel = new RelayCommand(CancelAction);
            InitializeComponent();
            App.Register<Model.Match>(this, AppMessages.MSG_MATCH_CHANGED, player => { ComboBoxPlayers(); ComboBoxMatchs();});
        }
    }
}
