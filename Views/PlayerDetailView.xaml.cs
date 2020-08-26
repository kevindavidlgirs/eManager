using prbd_1920_g04.Model; 
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace prbd_1920_g04.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerDetailView.xaml
    /// </summary>
    public partial class PlayerDetailView : UserControlBase
    {
        public List<Statistics> Data { get; set; }
        public Player Player { get; set; }
        
        public string FirstName
        {
            get { return Player.FirstName; }
        }
        
        public string LastName
        {
            get { return Player.LastName; }
        }

        public int Age
        {
            get { return Player.Age; }
        }

        public int Hght
        {
            get { return Player.Height; }
        }

        public double Weight
        {
            get { return Player.Weight; }
        }

        public string Adresse
        {
            get { return Player.Adresse; }
        }

        public string Email
        {
            get { return Player.Email; }
        }

        public int JerseyNumber
        {
            get { return Player.JerseyNumber; }
        }
        public string PicturePath
        {
            get { return Player.PicturePath; }
        }

        private void CreateStats()
        {
            foreach (var stats in Player.StatsList)
            {
                Data.Add(new Statistics { Date = stats.Match.DateMatch.ToString("dd/MM/yyyy"), SumGoodActions = stats.GoalsScored + stats.GoalsConceced + stats.Assists, SumBadActions = stats.Injuries + stats.Fouls });
            }
        }
        public PlayerDetailView(Player p)
        {
            DataContext = this;
            Player = p;
            Data = new List<Statistics>();
            CreateStats();
            InitializeComponent();
        }
    }
}
