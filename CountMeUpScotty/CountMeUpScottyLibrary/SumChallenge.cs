using System;

namespace CountMeUpScottyLibrary
{

  public class SumChallenge {

    // Default constructor
    public SumChallenge() {
      Random generator = new Random();
      left = generator.Next(0, 21);
      right = generator.Next(0, 21);
    }

    public void Solve(int result) {
      attempt = result;
      isSolved = true;
    }

    private int Solution() {
      return left + right;
    }

    public bool IsCorrectlySolved() {
      return Solution() == attempt;
    }

    public int Score() {
      if (IsCorrectlySolved()) {
        return 1;
      } else {
        return 0;
      }
    }

    public override string ToString() {
      string output = $"{left} + {right} = ";

      if (!isSolved) {
        output += "?";
      } else {
        if (IsCorrectlySolved()) {
          output += $"{attempt} [V]";
        } else {
          output += $"{attempt} [X {Solution()}]";
        }
      }

      return output;
    }

    // Attributes
    private int left = 0;
    private int right = 0;
    private int attempt = 0;
    private bool isSolved = false;
  }
}