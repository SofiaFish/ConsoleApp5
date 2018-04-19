using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.ComponentModel;
//using System.Threading.Tasks;

namespace ConsoleApp5
{
    public partial class Form1 : Form
    {
        private List<Coordinate> Snake = new List<Coordinate>();
        private Coordinate food = new Coordinate();

        private void Move()
        {
            for (int i = Snake.Count - 1; i >= 0; i--) 
            {
                if(i==0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.up:
                            Snake[i].pos_y--;
                            break;
                        case Direction.down:
                            Snake[i].pos_y++;
                            break;
                        case Direction.left:
                            Snake[i].pos_x--;
                            break;
                        case Direction.right:
                            Snake[i].pos_x++;
                            break;
                        default:
                            break;
                    }

                    int max_pos_x = Background.Size.Width;
                    int max_pos_y = Background.Size.Height;

                    if (Snake[i].pos_x < 0 || Snake[i].pos_y < 0 || Snake[i].pos_x > max_pos_x || Snake[i].pos_y > max_pos_y)
                        Die();

                    for (int j = 0; j < Snake.Count; j++)
                    {
                        if (Snake[i].pos_x == Snake[j].pos_x || Snake[i].pos_y == Snake[j].pos_y)
                            Die();
                    }

                    if (Snake[i].pos_x == food.pos_x || Snake[i].pos_y == food.pos_y)
                        Eat();
                    else
                    {
                        Snake[i].pos_x = Snake[i].pos_x;
                        Snake[i].pos_y = Snake[i].pos_y;
                    }
                }
            }
        }

        private void Eat()
        {
            Coordinate body = new Coordinate { pos_x = Snake[Snake.Count - 1].pos_x, pos_y = Snake[Snake.Count - 1].pos_y };
            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void Die()
        {
            Settings.GameOwer = true;
        }

        private void StartGame()
        {
            label1.Visible = false;
           // new Settings();
            Snake.Clear();
            Coordinate head = new Coordinate { pos_x = 150, pos_y = 50 };
            Snake.Add(head);
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood()
        {
            int max_pos_x = Background.Size.Width;
            int max_pos_y = Background.Size.Height;

            Random rand = new Random();
            food = new Coordinate { pos_x = rand.Next(0,max_pos_x), pos_y = rand.Next(0, max_pos_y) }; 
        }

        private void Screen(object sender, EventArgs e)
        {
            if (Settings.GameOwer == true)
            {
                if (Input.Press(Keys.Enter))
                    StartGame();

            }
            else
            {
                if (Input.Press(Keys.Right))
                    Settings.direction = Direction.right;
                if (Input.Press(Keys.Left))
                    Settings.direction = Direction.left;
                if (Input.Press(Keys.Up))
                    Settings.direction = Direction.up;
                if (Input.Press(Keys.Down))
                    Settings.direction = Direction.down;

                Move();
            }
            Background.Invalidate();
        }
        public Form1()
        {
            InitializeComponent();

            new Settings();

            Timer.Interval = 1000/Settings.Speed;
            Timer.Tick += Screen;
            Timer.Start();
            StartGame();
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void keyup(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Graphics(object sender, PaintEventArgs e)
        {
            Graphics background = e.Graphics;

            if(Settings.GameOwer == false)
            {
                Brush SnakeColore;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        SnakeColore = Brushes.Coral;
                    else
                        SnakeColore = Brushes.DarkRed;

                    background.FillEllipse(SnakeColore, Snake[i].pos_x * Settings.Widht, Snake[i].pos_y * Settings.Height, Settings.Widht, Settings.Height);
                    background.FillEllipse(Brushes.DeepPink, new Rectangle(Snake[i].pos_x * Settings.Widht, Snake[i].pos_y * Settings.Height, Settings.Widht, Settings.Height));
                }
            }
            string GameOwer = "Game ower \n" + "Final score is: " + Settings.Score + "\n Press Enter to restart \n";
        label1.Text = GameOwer;
        label1.Visible = true;
        }


        

            private void label1_Click(object sender, EventArgs e)
            {

            }

        private void Background_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
