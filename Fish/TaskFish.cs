namespace Fish;

public class TaskFish : FishBase
{
    public override FishBase Start()
    {
        _Start();
        return this;
    }

    private async Task _Start()
    {
        while (!Stop)
        {
            X = (X + Speed) % 500;
            ThreadId = Environment.CurrentManagedThreadId;
            await Task.Delay(20);
        }
    }

    public TaskFish(int speed) : base(speed)
    {
    }
}