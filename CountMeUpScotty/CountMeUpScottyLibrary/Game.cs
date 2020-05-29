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
        public Game(Player player, Difficulty difficulty = Difficulty.NORMAL, int numberOfChallenges = 10)
        {
            totalPlayerScore = new PlayerScore(player);
            this.difficulty = difficulty;
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

        public bool SolveCurrentChallenge(int attempt)
        {
            bool correct = false;
            if (nextChallenge > 0)
            {
                TimeSpan time = DateTime.Now - startTime;
                Console.WriteLine("Took you " + time.TotalMilliseconds + " milliseconds");

                challenges[nextChallenge - 1].Solve(attempt);
                correct = challenges[nextChallenge - 1].IsCorrectlySolved();

                totalPlayerScore.Add(DetermineScore(time, correct), time);
            }
            return correct;
        }

        private int DetermineScore(TimeSpan time, bool isCorrect) 
        {
            if (!isCorrect)
            {
                return 0;
            }
            else
            {
                double multiplier = 1;
                if (difficulty == Difficulty.EASY)
                {
                    multiplier = 0.5;
                } else if (difficulty == Difficulty.HARD)
                {
                    multiplier = 2;
                }
                return (int)Math.Floor(multiplier * 12000 / time.TotalMilliseconds);
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
            if (IsFinished())
            {
                LoadScoreboard();
                scoreboard.Add(totalPlayerScore);
                SaveScoreboard();
            }
        }

        public List<SumChallenge> GetChallenges()
        {
            return new List<SumChallenge>(challenges);
        }

        // Private Methods

        private void CreateChallenges(int numberOfChallenges)
        {
            int minimumOperand = 0;
            int maximumOperand = 0;
            switch (difficulty)
            {   
                case Difficulty.EASY:
                    minimumOperand = 0;
                    maximumOperand = 20;
                    break;
                case Difficulty.NORMAL:
                    minimumOperand = -20;
                    maximumOperand = 20;
                    break;
                case Difficulty.HARD:
                    minimumOperand = -50;
                    maximumOperand = 50;
                    break;
                default:
                    break;
            }

            challenges = new SumChallenge[numberOfChallenges];
            for (int i = 0; i < numberOfChallenges; i++)
            {
                challenges[i] = new SumChallenge(minimumOperand, maximumOperand);
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
        private Difficulty difficulty = Difficulty.NORMAL;
    }
}