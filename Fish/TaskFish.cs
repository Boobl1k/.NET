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
            MakeStep();
            await Task.Delay(5);
        }
    }

    public TaskFish(int speed) : base(speed) => 
        Type = 1;
}
