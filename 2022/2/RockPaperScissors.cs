
public enum RockPaperScissors
{
    Rock,
    Paper,
    Scissors
}
public enum Result
{
    Win,
    Lose,
    Draw
}

public class Game
{

    public RockPaperScissors Player1 { get; }
    public RockPaperScissors Player2 { get; }

    public Game(char player1, char player2)
    {
        Player1 = Parse(player1);
        var result = ParseResult(player2);
        if (result == Result.Draw)
        {
            Player2 = Player1;
        }
        else if (result == Result.Win)
        {
            Player2 = Player1+1;
            if ((int)Player2 == 3)
            {
                Player2 = RockPaperScissors.Rock;
            }
        }
        else
        {
            Player2 = Player1-1;
            if ((int)Player2 == -1)
            {
                Player2 = RockPaperScissors.Scissors;
            }
        }
    }

    public static RockPaperScissors Parse(char input)
    {
        switch (input)
        {
            case 'A':
                return RockPaperScissors.Rock;
            case 'B':
                return RockPaperScissors.Paper;
            case 'C':
                return RockPaperScissors.Scissors;
            default:
                throw new Exception();
        }
    }
    public static Result ParseResult(char input)
    {
        switch (input)
        {
            case 'X':
                return Result.Lose;
            case 'Y':
                return Result.Draw;
            case 'Z':
                return Result.Win;
            default:
                throw new Exception();
        }
    }

    public int Score()
    {
        var score = 0;

        if (Player1 == Player2)
        {
            score += 3;
        }
        else if ((Player1 == RockPaperScissors.Rock && Player2 == RockPaperScissors.Paper) ||
            (Player1 == RockPaperScissors.Paper && Player2 == RockPaperScissors.Scissors) ||
            (Player1 == RockPaperScissors.Scissors && Player2 == RockPaperScissors.Rock))
        {
            score += 6;
        }

        switch (Player2)
        {
            case RockPaperScissors.Rock:
                score++;
                break;
            case RockPaperScissors.Paper:
                score += 2;
                break;
            case RockPaperScissors.Scissors:
                score += 3;
                break;
        }

        return score;
    }
}
