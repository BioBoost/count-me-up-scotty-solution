using System;
using System.Collections.Generic;
using System.Text;

namespace CountMeUpScottyLibrary
{
    public class Score
    {
        public Score()
        {
        }

        public Score(int score, TimeSpan time)
        {
            this.score = score;
            this.time = time;
        }

        public int GetScore()
        {
            return score;
        }

        public TimeSpan GetTime()
        {
            return time;
        }

        public void ClearScore()
        {
            score = 0;
        }

        public override string ToString()
        {
            return $"{score} {time}";
        }

        // Attributes
        private int score = 0;
        private TimeSpan time;
    }
}
