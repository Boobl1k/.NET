namespace Fish;

public class TaskFish : FishBase
{
    public override void Start()
    {
        Task.Run(() =>
        {
            while (true)
            {
                X = (X = Speed) % 500;
                ThreadId = Thread.GetCurrentProcessorId();
                Task.Delay(20);
            }
        });
    }

    public TaskFish(int speed) : base(speed)
    {
    }
}