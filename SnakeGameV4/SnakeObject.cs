namespace SnakeGameV4
{
    internal class SnakeObject
    {

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SnakeWidth { get; set; }
        public int SnakeHeight { get; set; }
        public string directions;
        public bool gameOver;
        public bool goLeft, goRight, goDown, goUp;
        public CustomLinkedList<SnakeObject> SnakeBody;
        public SnakeObject()
        {
            PositionX = 0;
            PositionY = 0;
            SnakeWidth = 16;
            SnakeHeight = 16;
            directions = "left";
            gameOver = false;
            SnakeBody = new CustomLinkedList<SnakeObject>();

        }
        public int BodyParts()
        {
            return SnakeBody.Count - 1;
        }
        public void EatFood()
        {

            SnakeObject body = new SnakeObject
            {
                PositionX = SnakeBody.Count,
                PositionY = SnakeBody.Count
            };

            SnakeBody.Add(body);

        }

        public void SnakeDirection()
        {

            if (goLeft)
            {
                directions = "left";
            }
            if (goRight)
            {
                directions = "right";
            }
            if (goDown)
            {
                directions = "down";
            }
            if (goUp)
            {
                directions = "up";
            }
        }
        public void NextMove()
        {
            int prevX = SnakeBody.PeekFirst().PositionX;
            int prevY = SnakeBody.PeekFirst().PositionY;
            int i = 0;

            switch (directions)
            {
                case "left":
                    SnakeBody.PeekFirst().PositionX--;
                    break;
                case "right":
                    SnakeBody.PeekFirst().PositionX++;
                    break;
                case "down":
                    SnakeBody.PeekFirst().PositionY++;
                    break;
                case "up":
                    SnakeBody.PeekFirst().PositionY--;
                    break;
            }

            foreach (var item in SnakeBody)
            {
                if (i > 0)
                {
                    int tempX = item.PositionX;
                    int tempY = item.PositionY;
                    item.PositionX = prevX;
                    item.PositionY = prevY;
                    prevX = tempX;
                    prevY = tempY;
                }
                i++;
            }
        }
        public void SnakeColision(FoodGenerator food)
        {

            //boar colision
            if (SnakeBody.PeekFirst().PositionX < 0)
            {
                SnakeBody.PeekFirst().PositionX = Form2.maxWidth;
            }
            if (SnakeBody.PeekFirst().PositionX > Form2.maxWidth)
            {
                SnakeBody.PeekFirst().PositionX = 0;
            }
            if (SnakeBody.PeekFirst().PositionY < 0)
            {
                SnakeBody.PeekFirst().PositionY = Form2.maxHeight;
            }
            if (SnakeBody.PeekFirst().PositionY > Form2.maxHeight)
            {
                SnakeBody.PeekFirst().PositionY = 0;
            }

            // Food colision 
            if (SnakeBody.PeekFirst().PositionX == food.X && SnakeBody.PeekFirst().PositionY == food.Y)
            {
                food.BeEaten = true;
                EatFood();
            }
            //body colision
            foreach (var item in SnakeBody.Skip(1))
            {
                if (SnakeBody.PeekFirst().PositionX == item.PositionX && SnakeBody.PeekFirst().PositionY == item.PositionY)
                {
                    gameOver = true;
                }
            }
        }
        public void DrawSnake(Graphics canvas)
        {
            int i = 0;
            Brush snakeColour;
            foreach (var bodyPart in SnakeBody)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                (
                bodyPart.PositionX * SnakeWidth,
                bodyPart.PositionY * SnakeHeight,
                SnakeWidth, SnakeHeight
                ));
                i++;
            }

        }

    }
}
