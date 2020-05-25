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
    public partial class AddPlayerIntoATeamView : UserControlBase
    {
        //Le coach ici ?
        public Model.Secretary Secretary { get; set; }

        private ObservableCollection<Model.Player> players;
        public ObservableCollection<Model.Player> Players { get => players; set => SetProperty(ref players, value); }
        
        private ObservableCollection<Model.Team> teams;
        public ObservableCollection<Model.Team> Teams { get => teams; set => SetProperty(ref teams, value); }

        private string player;
        public string Player
        {
            get { return player; }
            set
            {
                player = value;
                RaisePropertyChanged(value);
            }
        }

        private string team;
        public String Team
        {
            get { return team; }
            set
            {
                team = value;
                RaisePropertyChanged(nameof(Team));
            }
        }
        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; }

        private void SaveAction()
        {
            
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
        public AddPlayerIntoATeamView()
        {
            DataContext = this;
            IsNew = true;
            Secretary = (Model.Secretary)App.CurrentUser;
            Teams = new ObservableCollection<Model.Team>(App.Model.Teams.OrderBy(m => m.Name));
            Players = new ObservableCollection<Model.Player>(App.Model.Players.OrderBy(p => p.LastName)); 
            //Save = new RelayCommand(SaveAction, CanSaveOrCancelAction);
            //Cancel = new RelayCommand(CancelAction);
            InitializeComponent();
        }
    }
}
