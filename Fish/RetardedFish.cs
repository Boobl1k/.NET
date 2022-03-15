namespace Fish;

public class RetardedFish : ThreadFish
{
    public RetardedFish(int speed) : base(speed) =>
        Type = 3;

    private bool _goingUp = false;
    private bool _goingLeft = false;

    private static int xSpeed;
    private static int ySpeed;

    protected override void Move()
    {
        if (Random.Shared.Next(20) == 0)
        {
            xSpeed = Random.Shared.Next(20);
            ySpeed = Random.Shared.Next(20);
        }

        if (!_goingLeft)
        {
            X += xSpeed;
            if (X >= 500)
            {
                _goingLeft = true;
                X = 1000 - X;
            }
        }
        else
        {
            X -= xSpeed;
            if (X < 0)
            {
                _goingLeft = false;
                X = -X;
            }
        }

        if (!_goingUp)
        {
            Y += ySpeed;
            if (Y >= 500)
            {
                _goingUp = true;
                Y = 1000 - Y;
            }
        }
        else
        {
            Y -= ySpeed;
            if (Y < 0)
            {
                _goingUp = false;
                Y = -Y;
            }
        }
    }
}
