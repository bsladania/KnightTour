using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment1
{
    //delegates
    delegate void Output(int row, int col, int position);
    delegate void InsertZero();
    delegate void Print(string file);
    delegate void Reader(string file);
    public partial class Form1 : Form
    {
        //global variables
        int time = 1;
        int count = 1;
        int avg = 0;
        string file = "";
        int hurMove = 0;
        int[] touch = new int[10];
        const int SIZE = 8;
        int[,] chessBoard = new int[SIZE, SIZE];
        int[,] gameboard = new int[SIZE, SIZE];
        int[,] movepoints = new int[SIZE, SIZE];        
        int curX = 0;
        int curY = 0;       
//------------ functions to check the availability of move using heuristics method ----------
        //function to check North-North-West
        public int checkNNW(int x, int y)
        {
            if (((x - 1) >= 0 && (x - 1) < 8) && ((y - 2) >= 0 && (y - 2)<8))
            {
                if (gameboard[x - 1, y - 2] == 0)
                {
                    return movepoints[x - 1, y - 2];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check West-North-West
        public int checkWNW(int x, int y)
        {
            if (((y - 1) >= 0 && (y - 1) < 8) && ((x - 2) >= 0 && (y - 2)<8))
            {
                if (gameboard[x - 2, y - 1] == 0)
                {
                    return movepoints[x - 2, y - 1];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check West-South-West
        public int checkWSW(int x, int y)
        {
            if (((y + 1) >= 0 && (y + 1) <= 7) && ((x - 2) >= 0 && (x - 2)<8))
            {
                if (gameboard[x - 2, y + 1] == 0)
                {
                    return movepoints[x - 2, y + 1];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check South-South-West
        public int checkSSW(int x, int y)
        {
            if (((y + 2) >= 0 && (y + 2) <= 7) && ((x - 1) >= 0 && (x - 1)<8))
            {
                if (gameboard[x - 1, y + 2] == 0)
                {
                    return movepoints[x - 1, y + 2];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check South-South-East
        public int checkSSE(int x, int y)
        {
            if (((y + 2) >= 0 && (y + 2) <= 7) && ((x + 1)>=0 && (x + 1) <= 7))
            {
                if (gameboard[x + 1, y + 2] == 0)
                {
                    return movepoints[x + 1, y + 2];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check East-South-East
        public int checkESE(int x, int y)
        {
            if (((y + 1) >= 0 && (y + 1) <= 7) && ((x + 2)>=0 && (x + 2) <= 7))
            {
                if (gameboard[x + 2, y + 1] == 0)
                {
                    return movepoints[x + 2, y + 1];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check East-North-East
        public int checkENE(int x, int y)
        {
            if (((y - 1) >= 0 && (y - 1) < 8) && ((x + 2)>=0 && (x + 2) <= 7))
            {
                if (gameboard[x + 2, y - 1] == 0)
                {
                    return movepoints[x + 2, y - 1];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to check North-North-East
        public int checkNNE(int x, int y)
        {
            if (((y - 2) >= 0 && (y - 2) < 8) && ((x + 1)>=0 && (x + 1) <= 7))
            {
                if (gameboard[x + 1, y - 2] == 0)
                {
                    return movepoints[x + 1, y - 2];
                }
                else
                {
                    return 30;
                }
            }

            return 30;
        }
        //function to print chessboard
        public void printMoves(string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
                for (int r = 0; r < SIZE; r++)
                {
                    for (int c = 0; c < SIZE; c++)
                    {
                        writer.Write("\t" + gameboard[r, c]);
                    }
                    writer.WriteLine("\n");
                }
        }
        //------------ functions end ----------
        public Form1()
        {
            InitializeComponent();
        }
        //------button event handler method ---
        private void button1_Click(object sender, EventArgs e)
        {
            //clean richTextBox 
            outputTxtBox.Clear();
            txtMove.Clear();
            //getting moves
            time = Convert.ToInt32(cmbTime.SelectedItem);
            if (time == 0)
            {
                time = 1;
            }

            count = 1;
            //getting row and column by default they are 0 
            int rowNum = Convert.ToInt32(cmbRow.SelectedItem);
            int colNum = Convert.ToInt32(cmbCol.SelectedItem);
            //button selection
            if (rbtnon_intelligent.Checked)
            {
                file = "BikramjeetLadaniaNonIntelligentMethod.txt";
                System.IO.File.WriteAllText(file, string.Empty);
                //Insert zeros into chessboard(8,8) array
                InsertZero iZ = insertZero;
                iZ();
                //Calling knight Tour method through tour with appropriate arguments
                Output tour = knightTour;
                tour(rowNum, colNum, 1);
                MessageBox.Show("Knight tour is completed using non-intelligent method.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //If user select two times to run, then Average would be displayed using MessageBox
                if (time == 2)
                {
                    MessageBox.Show("Average number of squares touched is " + average() / 2.0, "Average", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //If user select more than two times to run, then Average as well as standard deviation would be displayed using MessageBox
                else if (time > 2)
                {
                    double variance = 0;
                    double av = average() / (double)time;
                    touch = positions();
                    MessageBox.Show("Average number of squares touched is " + av, "Average", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    for (int i = 0; i < time; i++)
                    {
                        variance = variance + Math.Pow((touch[i] - av), 2);
                    }
                    variance = variance / (time - 1);
                    MessageBox.Show("Standard deviation of squares touched is " + Math.Sqrt(variance), "Standard Deviation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }   
                
            }
            //button selection
            else if (rdbtnHeuristics.Checked)
            {
                file = "DanielFrancisHeuristicsMethod.txt";
                System.IO.File.WriteAllText(file, string.Empty);
                //getting row and column by default they are 0 
                curX = Convert.ToInt32(cmbRow.SelectedItem);
                curY = Convert.ToInt32(cmbCol.SelectedItem);
                //getting no. of times to run
                time = Convert.ToInt32(cmbTime.SelectedItem);
                //Calling runHeuristics with appropriate arguments
                runHeuristics(curX, curY, time,file);               
                MessageBox.Show("Knight tour is completed using Heuristic method.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //If user select two times to run, then Average would be displayed using MessageBox
                if (time == 2)
                {
                    MessageBox.Show("Average number of squares touched is " + average() / 2.0, "Average", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //If user select more than two times to run, then Average as well as standard deviation would be displayed using MessageBox
                else if (time > 2)
                {
                    double variance = 0;
                    double av = average() / (double)time;
                    touch = positions();
                    MessageBox.Show("Average number of squares touched is " + av, "Average", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    for (int i = 0; i < time; i++)
                    {
                        variance = variance + Math.Pow((touch[i] - av), 2);
                    }
                    variance = variance / (time - 1);
                    MessageBox.Show("Standard deviation of squares touched is " + Math.Sqrt(variance), "Standard Deviation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select one method!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        //It will insert zeros into chessBoard Array
        public void insertZero()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    chessBoard[row, col] = 0;
                }
            }
        }
        //It will print to Non-intelligent method's txt file
        public void print(string file )
        {
            using (StreamWriter writer = new StreamWriter(file))
                for (int r = 0; r < SIZE; r++)
                {
                    for (int c = 0; c < SIZE; c++)
                    {
                        writer.Write("\t" + chessBoard[r, c]);
                    }
                    writer.WriteLine("\n");
                }
        }
        //Non-Intelligent method uses recursive to get all available positions
        public void knightTour(int row, int col, int position)
        {
            Random rd = new Random();
            if ((row < SIZE && col < SIZE) && (position < (SIZE * SIZE)))
            {
                chessBoard[row, col] = position;
                //to check if the right and up position is avilable to move
                if ((row - 1 >= 0 && col + 2 >= 0) && (row - 1 < SIZE && col + 2 < SIZE) && (chessBoard[row - 1, col + 2] == 0))
                {                 
                        knightTour(row - 1, col + 2, position + 1);                    
                }
                //to check if the right and down position is avilable to move
                else if ((row + 1 >= 0 && col + 2 >= 0) && (row + 1 < SIZE && col + 2 < SIZE) && (chessBoard[row + 1, col + 2] == 0))
                {                    
                        knightTour(row + 1, col + 2, position + 1);                   
                }
                //to check if the left and up position is avilable to move
                else if ((row - 1 >= 0 && col - 2 >= 0) && (row - 1 < SIZE && col - 2 < SIZE) && (chessBoard[row - 1, col - 2] == 0))
                {                    
                        knightTour(row - 1, col - 2, position + 1);                    
                }
                //to check if the left and down position is avilable to move
                else if ((row + 1 >= 0 && col - 2 >= 0) && (row + 1 < SIZE && col - 2 < SIZE) && (chessBoard[row + 1, col - 2] == 0))
                {
                        knightTour(row + 1, col - 2, position + 1);                             
                }
                //to check if the down and right position is avilable to move
                else if ((row + 2 >= 0 && col + 1 >= 0) && (row + 2 < SIZE && col + 1 < SIZE) && (chessBoard[row + 2, col + 1] == 0))
                {                    
                        knightTour(row + 2, col + 1, position + 1);                        
                }
                //to check if the down and left position is avilable to move
                else if ((row + 2 >= 0 && col - 1 >= 0) && (row + 2 < SIZE && col - 1 < SIZE) && (chessBoard[row + 2, col - 1] == 0))
                {
                        knightTour(row + 2, col - 1, position + 1);                     
                }
                //to check if the up and left position is avilable to move
                else if ((row - 2 >= 0 && col - 1 >= 0) && (row - 2 < SIZE && col - 1 < SIZE) && (chessBoard[row - 2, col - 1] == 0))
                {
                    knightTour(row - 2, col - 1, position + 1);
                }
                //to check if the down and right position is avilable to move
                else if ((row - 2 >= 0 && col + 1 >= 0) && (row - 2 < SIZE && col + 1 < SIZE) && (chessBoard[row - 2, col + 1] == 0))
                {
                    knightTour(row - 2, col + 1, position + 1);
                }
                else
                {
                    //It would print into rich text box
                    outputTxtBox.AppendText("Trial:" + count + " The Knight was able to successfully touch " + position + " squares.\n");
                    //It would write into non-intelligent's text file
                    trialFile(file, count, position);
                    //It stores the number of postions
                    touch[count - 1] = position;
                    //It counts the time
                    count = count + 1;
                    //add all the moves made by knight
                    avg = avg + position;
                    position = 0;
                    //Call method to print all the moves in rich text box
                    printToBox();
                    //Insert zeros for next time
                    insertZero();
                    //Call run method to execute according to user requirements
                    run(rd.Next(8), rd.Next(8), time, count);
                                       
                }
            }
            
        }
        //Intelligent(heuristics) method  
        public void heuristics(int curX, int curY)
        {
            int i, j, a, b;
            bool canmove = true;
            int nnw, wnw, wsw, ssw, sse, ese, ene, nne;
            int move = 1;
            //Insert zeros into gameboard array
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    gameboard[i, j] = 0;
                }
            }
            //initialize array movepoints
            for (a = 0; a < 8; a++)
            {
                for (b = 0; b < 8; b++)
                {
                    if ((a >= 2) && (a <= 5) && (b >= 2) && (b <= 5))
                    {
                        movepoints[a, b] = 8;
                    }// movepoints = 8
                    else if ((a == 1) || (a == 6))
                    {
                        if ((b >= 2) && (b <= 5))
                        {
                            movepoints[a, b] = 6;
                        }
                    }//movepoints = 6 row 1 and 7
                    else if ((b == 1) || (b == 6))
                    {
                        if ((a >= 2) && (a <= 5))
                        {
                            movepoints[a, b] = 6;
                        }
                    }//movepoints = 6 column 1 and 7
                    else if ((a == 0) || (a == 7))
                    {
                        if ((b >= 2) && (b <= 5))
                        {
                            movepoints[a, b] = 4;
                        }
                    }//movepoints = 4 - part 1 row 0 and 8
                    else if ((b == 0) || (b == 7))
                    {
                        if ((a >= 2) && (a <= 5))
                        {
                            movepoints[a, b] = 4;
                        }
                    }//movepoints = 4 - part 2 (column 0 and column 8)                
                }
            }//end of movepoints array loop initialization

            movepoints[0, 0] = 2;
            movepoints[0, 7] = 2;
            movepoints[7, 0] = 2;
            movepoints[7, 7] = 2;

            movepoints[0, 1] = 3;
            movepoints[0, 6] = 3;
            movepoints[1, 0] = 3;
            movepoints[1, 7] = 3;
            movepoints[6, 0] = 3;
            movepoints[6, 7] = 3;
            movepoints[7, 1] = 3;
            movepoints[7, 6] = 3;

            movepoints[1, 1] = 4;
            movepoints[1, 6] = 4;
            movepoints[6, 1] = 4;
            movepoints[6, 6] = 4;

            //begin heuristics
            do
            {
                //look for the possible moves
                nnw = checkNNW(curX, curY);
                wnw = checkWNW(curX, curY);
                wsw = checkWSW(curX, curY);
                ssw = checkSSW(curX, curY);
                sse = checkSSE(curX, curY);
                ese = checkESE(curX, curY);
                ene = checkENE(curX, curY);
                nne = checkNNE(curX, curY);

                if ((nnw == 30) && (wnw == 30) && (wsw == 30) && (ssw == 30) && (sse == 30) && (ese == 30) && (ene == 30) && (nne == 30))
                {
                    canmove = false;
                }
                else if ((nnw <= wnw) && (nnw <= wsw) && (nnw <= ssw) && (nnw <= sse) && (nnw <= ese) && (nnw <= ene) && (nnw <= nne))
                {   //North-North-West
                    gameboard[curX, curY] = move;
                    move++;
                    curX -= 1;
                    curY -= 2;
                }
                else if ((wnw <= nnw) && (wnw <= wsw) && (wnw <= ssw) && (wnw <= sse) && (wnw <= ese) && (wnw <= ene) && (wnw <= nne) && (wnw < 30))
                {   //West-North-West
                    gameboard[curX, curY] = move;
                    move++;
                    curX -= 2;
                    curY -= 1;
                }
                else if ((wsw <= nnw) && (wsw <= wnw) && (wsw <= ssw) && (wsw <= sse) && (wsw <= ese) && (wsw <= ene) && (wsw <= nne))
                {   //West-South-West
                    gameboard[curX, curY] = move;
                    move++;
                    curX -= 2;
                    curY += 1;
                }
                else if ((ssw <= nnw) && (ssw <= wnw) && (ssw <= wsw) && (ssw <= sse) && (ssw <= ese) && (ssw <= ene) && (ssw <= nne))
                {   //South-South-West
                    gameboard[curX, curY] = move;
                    move++;
                    curX -= 1;
                    curY += 2;
                }
                else if ((sse <= nnw) && (sse <= wnw) && (sse <= wsw) && (sse <= ssw) && (sse <= ese) && (sse <= ene) && (sse <= nne))
                {   //South-South-East
                    gameboard[curX, curY] = move;
                    move++;
                    curX += 1;
                    curY += 2;
                }
                else if ((ese <= nnw) && (ese <= wnw) && (ese <= wsw) && (ese <= ssw) && (ese <= sse) && (ese <= ene) && (ese <= nne))
                {   //East-South-East
                    gameboard[curX, curY] = move;
                    move++;
                    curX += 2;
                    curY += 1;
                }
                else if ((ene <= nnw) && (ene <= wnw) && (ene <= wsw) && (ene <= ssw) && (ene <= sse) && (ene <= ese) && (ene <= nne))
                {   //East-North-East
                    gameboard[curX, curY] = move;
                    move++;
                    curX += 2;
                    curY -= 1;
                }
                else if ((nne <= nnw) && (nne <= wnw) && (nne <= wsw) && (nne <= ssw) && (nne <= sse) && (nne <= ese) && (nne <= ene))
                {   //North-North-East
                    gameboard[curX, curY] = move;
                    move++;
                    curX += 1;
                    curY -= 2;
                }
                else
                {
                    //Error message 
                    
                }
                hurMove = move-1;                
            } while (canmove);//keep looping as ong as knight can move
        }
        //It will print chessBoard Array's (Non-Intelligent) positions to rich text box 
        public void printToBox()
        {
            
                for (int r = 0; r < SIZE; r++)
                {
                    for (int c = 0; c < SIZE; c++)
                    {
                        txtMove.AppendText("\t" + chessBoard[r, c]);
                    }
                    txtMove.AppendText("\n\n");
                }
                txtMove.AppendText("\n\n");

        }
        //It will print gameboard Array's (Intelligent) positions to rich text box 
        public void printToBoxH()
        {

            for (int r = 0; r < SIZE; r++)
            {
                for (int c = 0; c < SIZE; c++)
                {
                    txtMove.AppendText("\t" + gameboard[r, c]);
                }
                txtMove.AppendText("\n\n");
            }
            txtMove.AppendText("\n\n");

        }
        //It would read the file and print to the outputTxtBox
        public void reader(string file)
        {
            using (StreamReader reader = new StreamReader(file))
                while (!reader.EndOfStream)
                {
                    outputTxtBox.AppendText(reader.ReadLine());
                    outputTxtBox.AppendText("\n");
                }
        }
        //It would write to the file with count and moves made by knight
        public void trialFile(string file,int count, int moves)
        {
            using (StreamWriter writer = new StreamWriter(file,true))
            {
                writer.WriteLine("Trial:" + count + " The Knight was able to successfully touch " + moves +" squares.\n");
            }
            
                
        }
        //It would return updated avg after function's calculations
        public int average()
        {
            return avg;
        }
        //It would return updated touch array after function's calculations
        public int[] positions()
        {
            return touch;
        }
        //it would look for number of time to run
        public void run(int row, int col, int time,int run)
        {             
            if (run <= time)
            {
                knightTour(row, col, 1);
            }
        }
        //it would look for number of time to run and will write to file
        public void runHeuristics(int row, int col, int run, string path)
        {
            Random rnd = new Random();
            int count = 1;
            heuristics(row, col);
            using (StreamWriter writer = new StreamWriter(path))
                writer.WriteLine("Trial:" + count + " The Knight was able to successfully touch " + hurMove + " squares.\n");
            outputTxtBox.AppendText("Trial:" + count + " The Knight was able to successfully touch " + hurMove + " squares.\n");
            printToBoxH();
            avg = avg + hurMove;
            touch[0] = hurMove;
            for (int i = 0; i < run-1; i++)
            {
                heuristics(rnd.Next(8), rnd.Next(8));
                count++;
                using (StreamWriter writer = new StreamWriter(path,true))
                    writer.WriteLine("Trial:" + count + " The Knight was able to successfully touch " + hurMove + " squares.\n");
                outputTxtBox.AppendText("Trial:" + count + " The Knight was able to successfully touch " + hurMove + " squares.\n");
                printToBoxH();
                avg = avg + hurMove;
                touch[i+1] = hurMove;
            }       
        }
    }
}
