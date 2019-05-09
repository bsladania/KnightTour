Knight's Tour Windows Form Application
======================================

-There are two methods that are used for knight's tour.
1. Non intelligent method
2. Heuristics Method

-Instructions
=============
-User can Select the number of times to run the method through drop down box.
-There are two drop down box for row and column. They will be zero by default.
-User must have to select atleast one method to run the application. 
-Output would be displayed in another tab.

-Explanation of code
====================

1. Non intelligent method
=========================
- At the begining of this method, zeros are placed at all the postions of 8 x 8 array of int type.
- To start it, the row and column are asked to the user for the initial position of knight. If the user does not select any value then it will start by 0,0 so basically it is a default position of knight.
- Now, the intial position(row,column) and count will go through the method. The count value would be placed to the current position and then there are 8 if else if statements to discover the possible moves of knight.
- If the knight satisfy any of the condition to take further move then it would call the method itself (recursively) with updated row, column and count.
- It will continue until it goes to all possible moves. Once it is done then else condition call print method to print all the moves into file.

2. Heuristics Method
====================
- This method utilizes two arrays for its functioning; gameboard and movepoints.
- gameboard is a two dimensional 8X8 array,representing a chess board, which is initialized to 0's.
- as the game proceeds and the knight moves on the board, they are recorded in gameboard upto a maximum of 64 moves (8X8)
- heuristics also uses probability to determine the knights next move!
- the array movepoints contains numbers pertaining to "the best possible" move be made.
- a higher number indicates a greater possibility of the knight getting trapped.
- to avoid being trapped, the knight must move along the edges of the gameboard and slowly make it's way in.
- consequently, the the heuristics method makes function calls to 'checkXXX' methods that assist in determining the next best move.
- for example checkNNW() checks the North-North-West direction, checkESE() checks South-South-East.
- a lower returned number implies a better choice to move (in that direction).

     