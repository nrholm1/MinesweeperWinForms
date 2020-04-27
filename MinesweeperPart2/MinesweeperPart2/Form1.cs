using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperPart2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            timer = new CounterTimer();
            timer.Start();
            timer.Tick += Timer_Tick;


            FlaggedMines = 0;
            FalsePositives = 0;
            InitializeResetButton(this);
            InitializeButtonMatrix();
            GenerateMinefield();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CounterTimer CT = sender as CounterTimer;
            CT.IncrementTimer();
            
            Console.WriteLine(CT.GetUITime());
            this.Text = "Mineswooper by Nøls (" + CT.GetUITime() + ")";
        }

        private GroupBox groupBox1;
        private Point windowSize = new Point(680, 585);

        private XYButton[,] buttonMatrix; // 2D array of Button-deriv class augmented with X- and Y-coordinates (PosX- & PosY-properties int) and some game logic parameters
        private Point matrixDimensions = new Point(12, 10);

        private int NumberOfMines = 20;
        private Random RNG = new Random();

        private int FlaggedMines;
        private int FalsePositives;

        private CounterTimer timer;

        public void InitializeButtonMatrix()
        {
            // Initialize main elements
            this.groupBox1 = new GroupBox();
            this.buttonMatrix = new XYButton[matrixDimensions.X, matrixDimensions.Y];


            // Groupbox dimensions / properties
            this.groupBox1.Location = new Point(30, 30);
            this.groupBox1.Size = new Size(windowSize.X - 50, windowSize.Y - 50);
            this.groupBox1.Text = "Minefield - " + NumberOfMines.ToString() + " Mines left";
            this.groupBox1.Font = new Font(groupBox1.Font, FontStyle.Bold);


            // Populate buttonMatrix
            for(int i = 0; i < matrixDimensions.X; i++)
            {
                for(int j = 0; j < matrixDimensions.Y; j++)
                {
                    // Create button
                    buttonMatrix[i, j] = new XYButton(i,j);
                    buttonMatrix[i, j].Size = new Size(50,50);
                    buttonMatrix[i, j].Location = new Point(50*i+15,50*j+18);
                    //buttonMatrix[i, j].Text = (i + 1).ToString() + ", " + (j + 1).ToString();
                    buttonMatrix[i, j].Font = new Font(groupBox1.Font, FontStyle.Regular);

                    // trigger buttonClicked event
                    buttonMatrix[i, j].MouseDown += new MouseEventHandler(buttonClicked);



                    // add to group box
                    this.groupBox1.Controls.Add(this.buttonMatrix[i, j]);
                }
            }

            // Window size
            this.ClientSize = new System.Drawing.Size(windowSize.X, windowSize.Y);
            // Put Group box in the client window
            this.Controls.Add(this.groupBox1);
        }

        private void GenerateMinefield()
        {

            int mineX;
            int mineY;

            // counter int
            int q = 0;

            while (q < NumberOfMines)
            {
                mineX = RNG.Next(0,matrixDimensions.X);
                mineY = RNG.Next(0, matrixDimensions.Y);

                if (!this.buttonMatrix[mineX, mineY].IsMine)
                { 
                    q++;
                    this.buttonMatrix[mineX, mineY].IsMine = true;
                    //this.buttonMatrix[mineX, mineY].BackColor = Color.Red;
                }
            }
        }

        private void GameOver()
        {
            Console.WriteLine("Mine clicked. Game Over!");
            var GameOverWindow = new GameResultWindow();
            GameOverWindow.GameResultText = "Game Over!";
            InitializeResetButton(GameOverWindow, true);
            GameOverWindow.ShowDialog(this);
        }

        private void GameWin()
        {
            timer.Stop();
            Console.WriteLine("All mines flagged. You Win!");
            var YouWinWindow = new GameResultWindow();
            YouWinWindow.GameResultText = "You Win!\r\n" + timer.GetUITime();
            InitializeResetButton(YouWinWindow, true);
            YouWinWindow.ShowDialog(this);
        }


        // Handle mouse clicks on a button
        private void buttonClicked(object sender, MouseEventArgs e)
        {
            XYButton b = sender as XYButton;

            switch (e.Button)
            {
                /*----- Left Click ----- */
                case MouseButtons.Left:
                    if (!b.IsFlagged)
                        b.IsClicked = true;
                    else break;

                    if (b.IsMine)
                    {
                        GameOver();
                    }

                    if (!b.IsMine)
                    {
                        int adjacentMines = FindAdjacent(b);
                        b.Text = adjacentMines > 0 ? adjacentMines.ToString() : "";
                        if (FindAdjacent(b) == 0)
                            ClickZeroButtons(b);
                        b.BackColor = Color.LightGoldenrodYellow;
                    }
                    Console.WriteLine("(" + b.Text + ") buttonClicked() | i = " + b.PosX.ToString() + " j = " + b.PosY.ToString());
                    break;

                /*----- Right Click ----- */
                case MouseButtons.Right:
                    if (!b.IsClicked)
                        b.IsFlagged = b.IsFlagged ? false : true;
                    else break;

                    if (b.IsFlagged && b.IsMine)
                    {
                        FlaggedMines++;
                        b.BackColor = Color.LightGreen;
                    }

                    if (b.IsFlagged && !b.IsMine)
                    {
                        FalsePositives++;
                        b.BackColor = Color.LightGreen;
                        //b.BackColor = Color.DarkGoldenrod; // used for debugging
                    }

                    if (!b.IsFlagged && b.IsMine)
                    {
                        FlaggedMines--;
                        b.BackColor = Color.Transparent;
                    }

                    if (!b.IsFlagged && !b.IsMine)
                    {
                        FalsePositives--;
                        Console.WriteLine("FalsePositives--");
                        b.BackColor = Color.Transparent;
                    }

                    if (FlaggedMines == NumberOfMines && FalsePositives == 0)
                        GameWin();

                    Console.WriteLine("(" + b.Text + ") fieldFlagged() | i = " + b.PosX.ToString() + " j = " + b.PosY.ToString());
                    break;

                default:
                    throw new Exception("Mouse button not recognized");
            }
            this.groupBox1.Text = "Minefield - " + (NumberOfMines - (FlaggedMines + FalsePositives)).ToString() + " Mines left";
        }
    
        private void InitializeResetButton(object sender, bool popup=false)
        {
            Form f = sender as Form;
            Button ResetButton = new Button();

            ResetButton.Text = "Reset form";
            if (!popup)
            {
                ResetButton.Location = new Point(windowSize.Y - 15, 10);
                ResetButton.Click += new EventHandler(ResetForm);
            }
            else
            {
                ResetButton.Location = new Point((f.Size.Width - ResetButton.Size.Width) / 2 - 11, (f.Size.Height - ResetButton.Size.Height) / 2 + 25);
                ResetButton.Click += new EventHandler(ResetFormPopup);
            }
            f.Controls.Add(ResetButton);


        }


        private int FindAdjacent(XYButton b)
        {
            int counter = 0;
            int x, y;
            for(int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    x = b.PosX + i;
                    y = b.PosY + j;
                    if (x >= 0 && y >= 0 && x < matrixDimensions.X && y < matrixDimensions.Y)
                        if (buttonMatrix[x, y].IsMine)
                            counter++;
                }
            }

            return counter;
        }

        private void ClickZeroButtons(XYButton b)
        {
            int x, y;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    x = b.PosX + i;
                    y = b.PosY + j;
                    if (x >= 0 && y >= 0 && x < matrixDimensions.X && y < matrixDimensions.Y && !(buttonMatrix[x,y].IsClicked)) 
                    {
                        int adjacentMines = FindAdjacent(buttonMatrix[x, y]);
                        bool ZeroAdjacent = adjacentMines == 0 ? true : false;

                        buttonMatrix[x, y].IsClicked = true;
                        buttonMatrix[x, y].BackColor = Color.LightGoldenrodYellow;
                        buttonMatrix[x, y].Text = ZeroAdjacent ? "" : adjacentMines.ToString();
                        if (ZeroAdjacent)
                        {
                            ClickZeroButtons(buttonMatrix[x, y]);
                            Console.WriteLine("Should be called recursively!");

                        }

                    }
                }
            }
        }


        private void ResetForm(object sender, EventArgs e)
        {
            timer.Stop();
            this.Controls.Clear(); // Consider removing groupbox instead of clearing everything?
            Initialize();            
        }
        private void ResetFormPopup(object sender, EventArgs e)
        {
            timer.Stop();
            this.Controls.Clear(); // Consider removing groupbox instead of clearing everything?
            Initialize();
        }

    }
}
