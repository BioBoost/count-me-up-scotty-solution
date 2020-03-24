using System.Diagnostics;

namespace CountMeUpScottyLibrary
{

    public class Game
    {

        // Constructor
        public Game(Player player)
        {
            this.player = player;
            CreateChallenges();
        }

        public SumChallenge NextChallenge()
        {
            // SumChallenge challenge = challenges[currentChallenge];
            // currentChallenge++;
            // return challenge;

            if (!IsFinished())
            {
                return challenges[currentChallenge++];
            }
            else
            {
                return null;
            }
        }

        public bool IsFinished()
        {
            return currentChallenge >= challenges.Length;
        }

        public int Score()
        {
            int score = 0;
            foreach (var challenge in challenges)
            {
                score += challenge.Score();
            }
            return score;
        }

        public int NumberOfChallenges()
        {
            return challenges.Length;
        }

        public int GetCurrentChallengeNumber()
        {
            return currentChallenge;
        }

        public string Overview()
        {
            string output = "Your challenge overview:\n";
            foreach (var challenge in challenges)
            {
                output += challenge + "\n";
            }
            output += $"Your score = {Score()}";
            return output;
        }

        private void CreateChallenges()
        {
            for (int i = 0; i < challenges.Length; i++)
            {
                challenges[i] = new SumChallenge();
            }
        }

        // Attributes
        private Player player;
        private SumChallenge[] challenges = new SumChallenge[10];
        int currentChallenge = 0;
    }
}