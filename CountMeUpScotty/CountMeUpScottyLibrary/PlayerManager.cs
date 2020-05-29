using System;
using System.Collections.Generic;
using System.Text;

namespace CountMeUpScottyLibrary
{
    public class PlayerManager
    {
        private static List<Player> players = new List<Player>();

        public static Player GetPlayer(string nickname)
        {
            // First check if already exists
            foreach (Player player in players)
            {
                if (player.Name == nickname)
                {
                    return player;
                }
            }

            // If non-existing return new
            return Create(nickname);
        }

        private static Player Create(string nickname)
        {
            Player player = new Player(nickname);
            players.Add(player);
            return player;
        }
    }
}
