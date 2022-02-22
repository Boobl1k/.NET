namespace Fish;

public class ThreadFish : FishBase
{
    public override void Start()
    {
        new Thread(() =>
        {
            X = (X = Speed) % 500;
            ThreadId = Thread.GetCurrentProcessorId();
            Thread.Sleep(20);
        }).Start();
    }

    public ThreadFish(int speed) : base(speed)
    {
    }
}