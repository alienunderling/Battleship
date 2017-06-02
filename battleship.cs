using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            Console.WriteLine("Game Starting...");
            game.playRound();
        }
    
        //Used simply to test the testing.  
        //TODO: Figure out how to add in nunit.
        public int Add(int num1, int num2)
        {
            int result = num1 + num2;
            return result;
        }
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
            
            player1.setShipLocation();
            player2.setShipLocation();
        }
    }//end class BattleShipGame
    
    class Coordinates  
    {
        int row{ get; set; }
        int column{ get; set; }
        
        public Coordinates(int x, int y)
        {
            row = x;
            column = y;
        }
    }
    
    class BattleShipBoard  
    {
        List<Unit> gameBoard{get;set;}
        
        public BattleShipBoard()
        {
            gameBoard = new List<Unit>();
            
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    gameBoard.Add(new Unit(i, j));
                }
            }
        }
    }
    
    class Player
    {
        //member variables
        internal string name;
        //BattleShipBoard gameBoard;
        string shipLocation;
        Ship ship;
        
        public void setName(string playerName)
        {
            name = playerName;
        }
        
        public bool getHasLost()
        {
            return ship.hasSunk();
        }
        
        public void setShipLocation()
        {
            Console.WriteLine("Please enter the ship location {0}. Format: A3 A5", name);
            
            shipLocation = Convert.ToString(Console.ReadLine());
            Console.WriteLine("player one entered these coordinates: {0}", shipLocation);
            
            //gameBoard.placeShips( parseLocation() );
        }
        
        public string getShipLocation()
        {
            return shipLocation;
        }
        
        public string[] parseLocation( string location )
        {
            string re1="((?:[a-h][a-h]*[1-8]+[a-h1-8]*))";	// Coordinate
            string re2="(\\s+)";	// white space in between

            Regex r = new Regex(re1+re2+re1,RegexOptions.IgnoreCase|RegexOptions.Singleline);
            Match m = r.Match(location);
            
            string[] coordinates = new string[2];

            if (m.Success)
            {
                String coord1 = m.Groups[1].ToString();
                String ws = m.Groups[2].ToString();
                String coord2 = m.Groups[3].ToString();
                
                coordinates[0] = coord1;
                coordinates[1] = coord2;
                
                Console.WriteLine("First Coordinate: "+coordinates[0].ToString()+ws.ToString()+"Second Coordinate: "+coordinates[1].ToString()+"\n");
            } else {
                Console.WriteLine("Enter coordinates between [A-H], [1-8])");
                
            }
            
            return coordinates;
        } 
    }//end class Player
    
    class Ship  
    {
        int width = 3;
        int hits;
        
        public bool hasSunk()
        {
            return hits >= width;
        }
        
        public void isHit()
        {
            ++hits;    
        }
    }
    
    class Unit  
    {
        Coordinates coordinates {get; set;}
        bool isOccupied = false;
        
        public Unit(int row, int column)
        {
            coordinates = new Coordinates(row, column);
            isOccupied = true;
        }
        
        public bool getIsOccupied()
        {
            return isOccupied;
        }
        
    }
}