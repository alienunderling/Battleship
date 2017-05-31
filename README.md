# Battleship
Simple .net implementation of the classic board game.

Requirements:

1. Two players
2. Two board (one for each player)
3. A board is a 8x8 grid for a total of 64 units
4. Each player has one ship that is 3 grid units in length. 
5. A ship can be considered to be composed of three parts one for each grid unit
6. Each player will place the ship in the board either vertically or horizontally 
7. Players will take turns firing at their opponents ship
8. A hit occurs when a ship part resides in a grid unit that the other player fires selects to fire upon
9. A ship is sunk when all three parts have been hit
10. Grid units are specified by column and row
11. Columns are labeled A to H
12. Rows are labeled 1 to 8
13. The player grid should be displayed 

Example

    A B C D E F G H
1   - - - - - - - -
2   - X - - - - - -
3   - - - - - - - -
4   - - - - S H H -
5   - - - - - - - -
6   - - - - - - - -
7   - - - - - - - -
8   - - - - - - - -

- Ship is placed horizontally at E4
- Player fires at B2 (miss)
- Player fires at G4 (hit)
- Player fires at F4 and E4 sinking the ship and winning the game

Expectations

Allow two people (or one person pretending to be two) to play a game of battleship by positioning a ship and taking turns firing at their opponent. The minimum UI expectation is a console application that will print the player grids after each round. Since this is a coding problem it is acceptable if the player grids are visible to each other. The hits and misses must be displayed on the grid. You cannot fire outside of the grid. When a ship has been hit three times it is considered sunk and the game ends.  The code should be representative of your approach and should include the characteristics of good coding practices.
