using System;
namespace BattleShipApplication
{
   class BattleShip
   {
      static void Main(string[] args)
      {
          BattleShipGame game = new BattleShipGame();
         /* Battleship Runner */
         Console.WriteLine("Ready to go, Players?  Lets place our ships! Press any key to continue.");
         Console.ReadKey();
         
         game.playRound();
      }
      
      class BattleShipGame
       {
          //member variables
          //TODO: Create Player class
          internal Player player1;
          internal Player player2;
          
          public void playRound()
          {
              player1 = new Player();
              player2 = new Player();
              
              player1.setName("player1");
              player2.setName("player2");
              
              Console.WriteLine("Game Starting...");
              player1.setShipLocation();
              player2.setShipLocation();
          }
       }//end class BattleShipGame
       
       
      
      class Player
       {
          //member variables
          internal string name;
          //TODO: Create class Board
          //Board gameBoard;
          bool hasLost = false;
          string shipLocation;
          
          public void setName(string playerName)
          {
              name = playerName;
          }
          
          public void setHasLost()
          {
              hasLost = true;
          }
          
          public void setShipLocation()
          {
              Console.WriteLine("Please enter the ship location {0}. Format: A3 A5", name);
              
              shipLocation = Convert.ToString(Console.ReadLine());
              Console.WriteLine("player one entered these coordinates: {0}", shipLocation);
          }
       }//end class Player
   
      
      
      //Used simply to test the testing.  
      //TODO: Figure out how to add in nunit.
      public int Add(int num1, int num2)
      {
          int result = num1 + num2;
          return result;
      }
   }
}