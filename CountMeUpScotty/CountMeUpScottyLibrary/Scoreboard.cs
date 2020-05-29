using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Linq;

namespace CountMeUpScottyLibrary
{
    public class Scoreboard
    {
        public void Add(PlayerScore score)
        {
            scores.Add(score);
            Sort();
            Limit();
        }

        public void Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                try
                {
                    string[] parts = line.Split('|');
                    int score = Convert.ToInt32(parts[0]);
                    TimeSpan time = TimeSpan.Parse(parts[1]);
                    Player player = PlayerManager.GetPlayer(parts[2]);       // Get actual player
                    Add(new PlayerScore(player, score, time));
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Failed to parse score " + line);
                }
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                foreach (PlayerScore score in scores)
                {
                    file.WriteLine($"{score.Score}|{score.Time}|{score.Player.Name}");
                }
            }
        }

        public void Clear()
        {
            scores.Clear();
        }

        public List<PlayerScore> GetScores()
        {
            return scores;
        }

        private void Sort()
        {
            scores = scores.OrderByDescending(score => score.Score).ToList();
        }

        private void Limit()
        {
            scores = scores.Take(Math.Min(scores.Count, MAX_SCORES)).ToList();
        }

        // Attributes
        private List<PlayerScore> scores = new List<PlayerScore>();
        private static int MAX_SCORES = 5;
    }
}
