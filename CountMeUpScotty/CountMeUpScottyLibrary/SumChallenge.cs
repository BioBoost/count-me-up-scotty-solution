using System;

namespace CountMeUpScottyLibrary
{
    public class SumChallenge
    {
        public SumChallenge(int minimumOperand = 0, int maxOperand = 10)
        {
            left = generator.Next(minimumOperand, maxOperand+1);
            right = generator.Next(minimumOperand, maxOperand+1);
        }

        public int LeftOperand()
        {
            return left;
        }

        public int RightOperand()
        {
            return right;
        }

        public void Solve(int result)
        {
            attempt = result;
            isSolved = true;
        }

        public bool IsCorrectlySolved()
        {
            return Solution() == attempt;
        }

        public override string ToString()
        {
            string output = $"{left} + {right} = ";

            if (!isSolved)
            {
                output += "?";
            }
            else
            {
                if (IsCorrectlySolved())
                {
                    output += $"{attempt} [V]";
                }
                else
                {
                    output += $"{attempt} [X {Solution()}]";
                }
            }

            return output;
        }

        // Private Methods

        private int Solution()
        {
            return left + right;
        }

        // Attributes
        private int left = 0;
        private int right = 0;
        private int attempt = 0;
        private bool isSolved = false;

        private static Random generator = new Random();
    }
}