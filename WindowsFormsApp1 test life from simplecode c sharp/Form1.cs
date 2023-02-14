using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing;

namespace WindowsFormsApp1_test_life_from_simplecode_c_sharp
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private bool[,] field;
        private int rows;
        private int cols;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
            {
                return;
            }

            nudResolution.Enabled= false;
            nudDensity.Enabled= false;
            resolution = (int)nudResolution.Value;
            rows = pictureBox1.Height / resolution;
            cols = pictureBox1.Width / resolution;
            field= new bool[cols, rows];

            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x,y] = random.Next((int)nudDensity.Value) == 0;
                }      //29;38          
            }


            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
            //  graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution, resolution);
        }

        private void NextGeneration()
        {
            graphics.Clear(Color.Black);

            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = field[x,y];

                    if (!hasLife && neighboursCount == 3)
                    {
                        newField[x,y] = true;
                    }
                    else if (hasLife && (neighboursCount <2||neighboursCount>3))
                    {
                        newField[x,y] = false;
                    }
                    else
                    {
                        newField[x, y] = field[x,y];
                    }

                    if (hasLife)
                    {
                        graphics.FillRectangle(Brushes.Crimson , x * resolution,y* resolution,resolution,resolution);
                    }
                }
            }
            //Random random = new Random();
            //for (int x = 0; x < cols; x++)
            //{
            //    for (int y = 0; y < rows; y++)
            //    {
            //        field[x, y] = random.Next((int)nudDensity.Value) == 0;
            //    }     ///////////////////////////       
            //}


            field= newField;
            pictureBox1.Refresh();
        }

        private int CountNeighbours(int x, int y) 
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfChecking = col == x && row == y;
                    var hasLife = field[col,row];

                    if (hasLife && !isSelfChecking)
                    {
                        count++;
                    }
                }
            }
            return count;
        }


        private void StopGame()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //////////////////StartGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
