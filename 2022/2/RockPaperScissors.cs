
public enum RockPaperScissors {
    Rock,
    Paper, 
    Scissors
}

public class Game { 

    RockPaperScissors Player1 {get;}
    RockPaperScissors Player2 {get;}
    
    public Game(char player1, char player2){
        Player1 = Parse(player1);
        Player2 = Parse(player2);
    }

    public static  RockPaperScissors Parse(char input) {
        switch (input)
        {
            case 'A':
            case 'X':
                return RockPaperScissors.Rock;
            case 'B':
            case 'Y':
                return RockPaperScissors.Paper;
            case 'C':
            case 'Z':
                return RockPaperScissors.Scissors;
            default:
                throw new Exception();
        }
    }

    public int Score(){
        var score = 0;
        
        if(Player1 == Player2){
            score += 3;
        }
        else if ((Player1 == RockPaperScissors.Rock && Player2 == RockPaperScissors.Paper) ||
            (Player1 == RockPaperScissors.Paper && Player2 == RockPaperScissors.Scissors) || 
            (Player1 == RockPaperScissors.Scissors && Player2 == RockPaperScissors.Rock)) {
            score += 6;
        }

        switch(Player2){
            case RockPaperScissors.Rock:
                score++;
                break;
            case RockPaperScissors.Paper:
                score +=2;
                break;
            case RockPaperScissors.Scissors:
                score+=3;
                break;
        }

        return score;
    }
}
