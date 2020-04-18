using PRBD_Framework;
using System;
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

namespace prbd_1920_g04
{
    public partial class LoginView : WindowBase {
        private string pseudo;
        public string Pseudo {
            get => pseudo;
            set => SetProperty<string>(ref pseudo, value, () => Validate());
        }

        private string password;
        public string Password {
            get => password;
            set => SetProperty<string>(ref password, value, () => Validate());
        }

        public override bool Validate() {
            ClearErrors();
            var member = App.Model.Members.Find(Pseudo);
            if (!ValidateLogin(member) || !ValidatePwd(member))
                RaiseErrors();
            return !HasErrors;
        }

        private bool ValidateLogin(Member member) {
            if (string.IsNullOrEmpty(Pseudo)) {
                AddError("Pseudo", Properties.Resources.Error_Required);
            }
            else {
                if (Pseudo.Length < 3) {
                    AddError("Pseudo", Properties.Resources.Error_LengthGreaterEqual3);
                }
                else {
                    if (member == null) {
                        AddError("Pseudo", Properties.Resources.Error_DoesNotExist);
                    }
                }
            }
            return !HasErrors;
        }

        private bool ValidatePwd(Member member) {
            if (string.IsNullOrEmpty(Password)) {
                AddError("Password", Properties.Resources.Error_Required);
            }
            else if (!member.Password.Equals(Password)) {
                AddError("Password", Properties.Resources.Error_WrongPassword);
            }
            return !HasErrors;
        }
        private void LoginAction() {
            if (Validate()) { // si aucune erreurs
                var member = App.Model.Members.Find(Pseudo); // on recherche le membre 
                App.CurrentUser = member; // le membre connecté devient le membre courant
                ShowMainView(); // ouverture de la fenêtre principale
                Close(); // fermeture de la fenêtre de login
            }
        }

        public ICommand Login { get; set; }
        public ICommand Cancel { get; set; }

        private static void ShowMainView() {
            var mainView = new MainView();
            mainView.Show();
            Application.Current.MainWindow = mainView;
        }

        public LoginView() {
            InitializeComponent();

            DataContext = this;
            
            Login = new RelayCommand(LoginAction,
                () => { return pseudo != null && password != null && !HasErrors; });
            
            Cancel = new RelayCommand(() => Close());
        }
    }
}
