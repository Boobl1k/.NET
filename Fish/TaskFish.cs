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
            Move();
            await Task.Delay(5);
        }
    }

    public TaskFish(int speed) : base(speed) => 
        Type = 1;
}
