using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model
{
    public class Coach : Member
    {
        protected Coach() { }

        public bool selectPlayer(Match match, Player player) {
            if (!match.TeamPlaying.Players.Contains(player)) {
                //player.TeamName = match.TeamPlaying.Name;
                var p = App.Model.Players.Find(player.Id);
                p.TeamName = match.TeamPlaying.Name;
                match.TeamPlaying.Players.Add(player);

                App.Model.SaveChanges();
                return true;
            }
            return false;
        }

        public bool removePlayer(Match match, Player player) {
            if (match.TeamPlaying.Players.Contains(player)) {
                match.TeamPlaying.Players.Remove(player);
                App.Model.SaveChanges();
            }
            return false;
        }
    }
}
