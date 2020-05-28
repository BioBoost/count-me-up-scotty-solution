using System;

namespace CountMeUpScottyLibrary
{
    public class PlayerScore
    {
        public PlayerScore(Player player)
        {
            this.player = player;
        }

        public PlayerScore(Player player, int score, TimeSpan time)
            : this(player)
        {
            this.score = score;
            this.time = time;
        }

        public void Add(int value, TimeSpan time)
        {
            score += value;
            this.time += time;
        }

        private int score = 0;
        public int Score
        {
            get { return score; }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
        }

        private Player player = null;
        public Player Player
        {
            get { return player; }
        }

        public void ClearScore()
        {
            score = 0;
        }

        public override string ToString()
        {
            return $"{score} {time}";
        }
    }
}
