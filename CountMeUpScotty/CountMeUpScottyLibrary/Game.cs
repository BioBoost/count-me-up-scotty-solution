using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CountMeUpScottyLibrary
{

    public class Game
    {
        // Constructor
        public Game(Player player, int numberOfChallenges = 10)
        {
            totalPlayerScore = new PlayerScore(player);
            CreateChallenges(numberOfChallenges);
        }

        public SumChallenge NextChallenge()
        {
            if (!IsFinished())
            {
                startTime = DateTime.Now;
                return challenges[nextChallenge++];
            }
            else
            {
                return null;
            }
        }

        public bool IsFinished()
        {
            return nextChallenge == NumberOfChallenges();
        }

        public int NumberOfChallenges()
        {
            return challenges.Length;
        }

        public void SolveCurrentChallenge(int attempt)
        {
            if (nextChallenge > 0)
            {
                TimeSpan time = DateTime.Now - startTime;
                Console.WriteLine("Took you " + time.TotalMilliseconds + " milliseconds");

                int score = 0;
                challenges[nextChallenge - 1].Solve(attempt);
                if (challenges[nextChallenge - 1].IsCorrectlySolved())
                {
                    score = (int)Math.Floor(12000 / time.TotalMilliseconds);       // TODO Make dependant on difficulty
                }
                totalPlayerScore.Add(score, time);
            }
        }

        public PlayerScore GetCurrentScore()
        {
            return totalPlayerScore;
        }

        public int GetCurrentChallengeNumber()
        {
            return nextChallenge;
        }

        public string Overview()
        {
            string output = "Your challenge overview:\n";
            foreach (var challenge in challenges)
            {
                output += challenge + "\n";
            }
            output += $"Your score = {GetCurrentScore()}";
            return output;
        }

        public void Finish()
        {
            LoadScoreboard();
            scoreboard.Add(totalPlayerScore);
            SaveScoreboard();
        }

        public List<SumChallenge> GetChallenges()
        {
            return new List<SumChallenge>(challenges);
        }

        // Private Methods

        // TODO Should depend on the difficulty
        private void CreateChallenges(int numberOfChallenges)
        {
            challenges = new SumChallenge[numberOfChallenges];
            for (int i = 0; i < numberOfChallenges; i++)
            {
                challenges[i] = new SumChallenge();
            }
        }

        private void LoadScoreboard()
        {
            scoreboard.Load("scores.dat");
        }

        private void SaveScoreboard()
        {
            scoreboard.Save("scores.dat");
        }

        // Attributes
        private SumChallenge[] challenges = null;
        int nextChallenge = 0;
        private DateTime startTime;
        private Scoreboard scoreboard = new Scoreboard();
        private PlayerScore totalPlayerScore = null;
    }
}