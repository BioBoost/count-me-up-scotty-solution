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
            this.player = player;
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
                int possibleScore = (int)Math.Floor(12000 / time.TotalMilliseconds);       // TODO Make dependant on difficulty
                Console.WriteLine("Took you " + time.TotalMilliseconds + " milliseconds");
                challenges[nextChallenge - 1].Solve(attempt, new Score(possibleScore, time));
            }
        }

        public double GetFinalScore()
        {
            return GetScoreList().Sum(challenge => challenge.GetScore());
        }

        public List<Score> GetScoreList()
        {
            List<Score> scores = new List<Score>();
            foreach (var challenge in challenges)
            {
                scores.Add(challenge.GetScore());
            }
            return scores;
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
            output += $"Your score = {GetFinalScore()}";
            return output;
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

        // Attributes
        private Player player = null;
        private SumChallenge[] challenges = null;
        int nextChallenge = 0;
        private DateTime startTime;
    }
}