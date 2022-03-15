namespace Fish;

public class ThreadFish : FishBase
{
    public override FishBase Start()
    {
        new Thread(() =>
        {
            while (!Stop)
            {
                MakeStep();
                Thread.Sleep(5);
            }
        }).Start();
        return this;
    }

    public ThreadFish(int speed) : base(speed) => 
        Type = 2;
}