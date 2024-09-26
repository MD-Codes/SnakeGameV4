using System.Drawing.Imaging;

namespace SnakeGameV4
{
    public partial class Form2 : Form
    {
        private SnakeObject Snake = new SnakeObject();
        private FoodGenerator food = new FoodGenerator();
        MyStack<int> MyLastScores = new MyStack<int>(10);
        Random rand = new Random();
        public static int maxWidth;
        public static int maxHeight;
        public bool isGameOver;
        public static int Score { get; set; }
        public int HighScore;

        public Form2()
        {
            isGameOver = true;
            Score = 0;
            InitializeComponent();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left && Snake.directions != "right")
            {
                Snake.goLeft = true;

            }
            if (e.KeyCode == Keys.Right && Snake.directions != "left")
            {
                Snake.goRight = true;
            }
            if (e.KeyCode == Keys.Up && Snake.directions != "down")
            {
                Snake.goUp = true;
            }
            if (e.KeyCode == Keys.Down && Snake.directions != "up")
            {
                Snake.goDown = true;
            }
        }


        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Snake.goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Snake.goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                Snake.goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                Snake.goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            isGameOver = false;
            RestartGame();
        }

        private void ScoreLists(object sender, EventArgs e)
        {


            string scoreList = "Scores List:\n\n";
            int Myloops = MyLastScores.Size();
            do
            {
                scoreList += Myloops + ": " + MyLastScores.Get(Myloops -1).ToString() + "\n";
                Myloops--;
            } while (Myloops > 0);
            MessageBox.Show(scoreList);
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            Snake.SnakeDirection();
            Snake.NextMove();
            Snake.SnakeColision(food);

            txtScore.Text = "Score: " + Score;

            if (food.BeEaten == true)
            {
                food = new FoodGenerator { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
                Score++;
            }
            if (Snake.gameOver == true)
            {
                GameOver();

            }
            //Game speed level
            gameTimer.Interval = 100;
            picCanvas.Invalidate();

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            if (isGameOver == false)
            {
                Graphics canvas = e.Graphics;
                Snake.DrawSnake(canvas);
                food.DrawFood(canvas);
            }
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Snake.SnakeWidth - 1;
            maxHeight = picCanvas.Height / Snake.SnakeHeight - 1;

            Snake.SnakeBody.Clear();

            startButton.Enabled = false;
            snapButton.Enabled = false;
            Score = 0;
            txtScore.Text = "Score: " + Score;


            SnakeObject head = new SnakeObject { PositionX = 10, PositionY = 5 };
            Snake.SnakeBody.Add(head); // adding the head part of the snake to the list

            for (int i = 1; i < 6; i++)
            {
                SnakeObject body = new SnakeObject();
                Snake.SnakeBody.Add(body);
            }
            food = new FoodGenerator { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();

        }

        public void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            snapButton.Enabled = true;
            Snake.gameOver = false;
            MyLastScores.Add(Score);


            if (Score > HighScore)
            {
                HighScore = Score;

                txtHighScore.Text = "High Score: " + Environment.NewLine + HighScore;

                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
            Score = 0;
        }


    }
}
