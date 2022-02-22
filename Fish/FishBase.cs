namespace Fish;

public abstract class FishBase
{
    public int X { get; set; }
    public int Y { get; set; }
    protected int Speed { get; set; }
    public int ThreadId { get; protected set; }
    public bool Stop { get; set; } = false;

    protected FishBase(int speed)
    {
        Y = Random.Shared.Next(400) + 50;
        Speed = speed;
    }

    public abstract void Start();
}