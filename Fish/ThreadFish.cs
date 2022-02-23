namespace Fish;

public class ThreadFish : FishBase
{
    public override FishBase Start()
    {
        new Thread(() =>
        {
            while (!Stop)
            {
                X = (X + Speed) % 500;
                ThreadId = Environment.CurrentManagedThreadId;
                Thread.Sleep(20);
            }
        }).Start();
        return this;
    }

    public ThreadFish(int speed) : base(speed)
    {
    }
}