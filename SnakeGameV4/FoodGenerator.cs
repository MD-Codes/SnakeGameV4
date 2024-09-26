namespace SnakeGameV4
{
    class FoodGenerator
    {
        public bool BeEaten { get; set; }
        public int FoodWidth = 16;
        public int FoodHeight = 16;
        public int X { get; set; }
        public int Y { get; set; }

        public FoodGenerator()
        {
            X = 0;
            Y = 0;
            BeEaten = false;

        }
        public void DrawFood(Graphics canvas)
        {
            Brush foodColour = Brushes.Red;
            canvas.FillEllipse(foodColour, new Rectangle(X * 16, Y * 16, 16, 16));
        }

    }
}

