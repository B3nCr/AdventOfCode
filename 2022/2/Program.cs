var input = File.ReadAllLines("input.txt");

var score = 0;
for(var i = 0 ; i<input.Length ; i++){
    var player1 = input[i][0];
    var player2 = input[i][2];

    // Console.WriteLine(Game.Parse(player1));
    // Console.WriteLine(Game.Parse(player2));

    var game = new Game(input[i][0], input[i][2]);
    
    Console.WriteLine(game.Player1);
    Console.WriteLine(game.Player2);
  
    score += game.Score();
}

Console.WriteLine(score);