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
            if (!match.Squad.Players.Contains(player)) {
                match.Squad.Players.Add(player);
            }
            return false;
        }

        public bool removePlayer(Match match, Player player) {
            if (match.Squad.Players.Contains(player)) {
                match.Squad.Players.Remove(player);
            }
            return false;
        }
    }
}
