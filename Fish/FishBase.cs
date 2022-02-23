namespace Fish;

public abstract class FishBase
{
    private static readonly object locker = new();
    private static int _prevId = 1000;

    public int Type { get; protected set; }
    public int Id { get; }

    private volatile int _x;
    private volatile int _y;

    public int X
    {
        get => _x;
        set => _x = value;
    }

    public int Y
    {
        get => _y;
        set => _y = value;
    }

    protected int Speed { get; }
    private volatile int _threadId;

    public int ThreadId
    {
        get => _threadId;
        protected set => _threadId = value;
    }

    public bool Stop { get; set; } = false;

    protected FishBase(int speed)
    {
        lock (locker)
        {
            Id = ++_prevId;
        }

        Y = Random.Shared.Next(400) + 50;
        Speed = speed;
    }

    public abstract FishBase Start();

    private bool _goingLeft = false;
    protected void Move()
    {
        if (!_goingLeft)
        {
            X += Speed;
            if (X >= 500)
            {
                _goingLeft = true;
                X = 999 - X;
            }
        }
        else
        {
            X -= Speed;
            if (X < 0)
            {
                _goingLeft = false;
                X = -X;
            }
        }

        ThreadId = Environment.CurrentManagedThreadId;
    }
}