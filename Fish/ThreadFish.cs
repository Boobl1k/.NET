namespace Fish;

public class ThreadFish : FishBase
{
    public override FishBase Start()
    {
        new Thread(() =>
        {
            while (!Stop)
            {
                Move();
                Thread.Sleep(5);
            }
        }).Start();
        return this;
    }

    public ThreadFish(int speed) : base(speed) => 
        Type = 2;
}