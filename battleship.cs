using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;

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
            game.startGame();
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
        
        public BattleShipGame()
        {
            player1 = new Player();
            player2 = new Player();
        }
        
        public void startGame()
        {
            player1.setName("player1");
            player2.setName("player2");
            
            player1.setShipLocation();
            player2.setShipLocation();
            
            player1.outputBoards();
            player2.outputBoards();
            
            playGame();
        }
        
        public void playGame()  
        {
            while (!player1.hasLost() && !player2.hasLost())
            {
                playRound();
            }
            
            player1.outputBoards();
            player2.outputBoards();
            
            if (player1.hasLost())
            {
                Console.WriteLine(player2.name + " has won the game!");
            }
            else if (player2.hasLost())
            {
                Console.WriteLine(player1.name + " has won the game!");
            }
        }
        
        public void playRound()  
        {
            //Player1 Fires First
            Console.WriteLine(player1.name + ": Provide a location to hit {0}'s ship", player2.name);
            var p1Shot = Console.ReadLine();
            
            player2.processShot(p1Shot);
            
            //Player2 Fires Second
            Console.WriteLine(player2.name + ": Provide a location to hit {0}'s ship", player1.name);
            var p2Shot = Console.ReadLine();
            
            player1.processShot(p2Shot);
        }
    }//end class BattleShipGame
    
    class BattleShipBoard  
    {
        public List<Unit> gameBoard{get;set;}
        
        public BattleShipBoard()
        {
            gameBoard = new List<Unit>();
            
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    gameBoard.Add(new Unit(i, j));
                }
            }
        }
        
        public void placeShip( Coordinates start, Coordinates end )
        { 
            //Set the Units corresponding to the coord range to occupied.
            //Find the corresponding units and set them to isOccupied
                              
            var occupiedUnits = new List<Unit>();
                              
            occupiedUnits = gameBoard.Where(x => x.coordinates.getRow() >= start.getRow() 
                                            && x.coordinates.getColumn() >= start.getColumn() 
                                            && x.coordinates.getRow() <= end.getRow() 
                                            && x.coordinates.getColumn() <= end.getColumn()).ToList();
            
            int i = 0;
            foreach (Unit unit in occupiedUnits)
            {
                unit.setIsOccupied();
                unit.coordinates.getColumn(), unit.getIsOccupied());
            } 
        }
    }//end class BattleShipBoard
    
    class Player
    {
        //member variables
        internal string name;
        BattleShipBoard gameBoard;
        string shipLocation;
        Ship ship;
        Coordinates startRange;
        Coordinates endRange;
        
        public Player()
        {
            gameBoard = new BattleShipBoard();
            ship = new Ship();
        }
        
        public void setName(string playerName)
        {
            name = playerName;
        }
        
        public bool hasLost()
        {
            return ship.hasSunk();
        }
        
        public void setShipLocation()
        {
            string[] coordinates = new string[2];

            Console.WriteLine("Please enter the ship location {0}. Format: A3 A5", name);

            shipLocation = Convert.ToString(Console.ReadLine());

            coordinates = parseLocation(shipLocation);

            while(!checkCoord( coordinates ))
            {
                Console.WriteLine("Sorry Player {0} please re-enter. Something was not right with your coordinates. Format: A3 A6", name);

                shipLocation = Convert.ToString(Console.ReadLine());

                coordinates = parseLocation(shipLocation);
            }
            
            gameBoard.placeShip( startRange, endRange );
        }
        
        //TEST THIS
        public bool checkCoord( string[] coords ) //eg 'A3, B7'
        {
            bool goodCoords = true;
            //'p' is a placeholder to move to base 1
            char[] coordArray = { 'p', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

            string c1 = coords[0];//A3
            string c2 = coords[1];//B7

            //Check if the user entered a correct format
            if( (c1 == null) || (c2 == null) )
            {            
                return false;
            } 

            //Get the Alpha character from each coord
            char col1 = c1[0];
            char col2 = c2[0];

            //Get the Numeric character from each coord
            char rnum1 = c1[1];
            char rnum2 = c2[1];
            //We need these to be int's.
            int y1 = (int)Char.GetNumericValue(rnum1);
            int y2 = (int)Char.GetNumericValue(rnum2);
            
            int x1 = Array.IndexOf(coordArray, col1);
            int x2 = Array.IndexOf(coordArray, col2);

            if((x1 == x2 && y1 == y2) || (x1 != x2 && y1 != y2) )
            {
                Console.WriteLine( "First Fail: coord NOT ok" );
                goodCoords = false;
            }

            if(x1 == x2){
                if(y1 > y2 && (y1 - y2) == 2)
                {
                    Console.WriteLine( "coord ok" );
                } else if(y1 < y2 && (y2 - y1) == 2)
                {
                    Console.WriteLine( "coord ok" );
                } else {
                    Console.WriteLine( "Second Fail: coord NOT ok" );
                    goodCoords = false;
                }
            } else
            {
                if(x1 > x2 && (x1 - x2) == 2)
                {
                    Console.WriteLine( "coord ok" );
                } else if(x1 < x2 && (x2 - x1) == 2)
                {
                    Console.WriteLine( "coord ok" );
                } else {
                    Console.WriteLine( "Third Fail: coord NOT ok" );
                    goodCoords = false;
                }
            }
            
            if(goodCoords) 
            {
                startRange = new Coordinates(x1, y1);
                endRange = new Coordinates(x2, y2);
            }
            
            return goodCoords;
        }
        
        //TEST THIS
        public string[] parseLocation( string location )
        {
            //This reg exp ensures the ship location is on the board.
            string re1="((?:[a-h][a-h]*[1-8]+[a-h1-8]*))";	// Coordinate
            string re2="(\\s+)";	// white space in between

            Regex r = new Regex(re1+re2+re1,RegexOptions.IgnoreCase|RegexOptions.Singleline);
            Match m = r.Match(location);
            
            string[] coordinates = new string[2];

            if (m.Success)
            {
                String coord1 = m.Groups[1].ToString();
                //String ws = m.Groups[2].ToString(); //NOT USED
                String coord2 = m.Groups[3].ToString();
                
                coordinates[0] = coord1;
                coordinates[1] = coord2;
            } else 
            {
                Console.WriteLine("Enter coordinates between [A-H], [1-8])");
            }
            
            return coordinates;
        }
        
        //TODO: Refactor - There must be a better way to get the units out of the gameBoard
        public void outputBoards()  
        {
            var currentUnitList = new List<Unit>();
            Unit tempUnit;
            
            Console.WriteLine("{0} board", name);
            Console.WriteLine(Environment.NewLine);
            
            //First print the Column titles...
            string[] clmns = { "   ", " A ", " B ", " C ", " D ", " E ", " F ", " G ", " H "};
            
            for(int i = 0; i <= 8; i++)
            {
                Console.Write(clmns[i]);
            }
            
            Console.WriteLine(Environment.NewLine);
            
            for(int row = 1; row <= 8; row++)
            {
                Console.Write(" {0} ", row);
                for(int ownColumn = 1; ownColumn <= 8; ownColumn++)
                {
                    currentUnitList =  gameBoard.gameBoard.Where(x => x.coordinates.getRow() == row && x.coordinates.getColumn() == ownColumn).ToList();
                    tempUnit = currentUnitList[0];
                    Console.Write(" {0} ", tempUnit.getStatus());
                }
                
                Console.WriteLine(Environment.NewLine);
            }
            
            Console.WriteLine(Environment.NewLine);
        }
        
        public void processShot(string shot)  
        {
            Unit hitUnit;
            char c1;
            char c2;
            int xValue;
            int yValue;
            var currentUnitList = new List<Unit>();
            //'p' is a placeholder to move to base 1
            char[] coordArray = { 'p', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            bool outOfBounds = true;
            
            do
            {
                //Check if in bounds
                c1 = shot[0];
                c2 = shot[1];
    
                xValue = Array.IndexOf(coordArray, c1);
                //convert to int
                yValue = (int)Char.GetNumericValue(c2);
            
                if( ( (xValue > 8) || (yValue > 8) ) || ( ( xValue < 1) || (yValue < 1) ) )
                {
                    Console.WriteLine( "Shot outside of bounds.  Please re-enter:" );
                    shot = Console.ReadLine();
                } else
                {
                    outOfBounds = false;
                }
            } 
            while(outOfBounds);
            
            //find Unit that was fired at
            currentUnitList =  gameBoard.gameBoard.Where(x => x.coordinates.getColumn() == xValue && x.coordinates.getRow() == yValue).ToList();
            hitUnit = currentUnitList[0];
            hitUnit.setShot();

            if(hitUnit.getIsOccupied())
            {
                //Call out a hit
                Console.WriteLine(name + " says: \"Hit!\"");
                
                ship.isHit();
            } else
            {
                //Call out a miss
                Console.WriteLine(name + " says: \"Miss!\""); 
            }
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
    }//end class ship
    
    class Unit  
    {
        internal Coordinates coordinates {get; set;}
        bool isOccupied = false;
        string status = "X";
        
        public Unit(int column, int row)
        {
            coordinates = new Coordinates(column, row);
        }
        
        public void setIsOccupied()
        {
            isOccupied = true;
            status = "S";
        } 
        
        public bool getIsOccupied()
        {
            return isOccupied;
        } 
        
        public void setShot()
        {
            if(isOccupied)
            {
                status = "H"; 
            }else
            {
                status = "M";
            }
        }
        
        public string getStatus()
        {
            return status;
        }
    }//end class Unit
    
    class Coordinates  
    {
        int row{ get; set; }
        int column{ get; set; }
        
        public Coordinates(int x, int y)
        {            
            column = x;
            row = y;
        }
        
        public int getRow()
        {
            return row;
        }
        
        public int getColumn()
        {
            return column;
        }
    }//end class Coordinates
}