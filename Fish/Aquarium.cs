using System.Text.Json;

namespace Fish;

public static class Aquarium
{
    private const int MaxCount = 3000;
    private const int MaxSpeed = 500;
    private static List<FishBase> Fishes { get; } = new();

    public static bool TryAddFish(bool task, int speed)
    {
        var retarded = Random.Shared.Next(15) == 0;
        speed = Math.Min(MaxSpeed, speed);
        if (Fishes.Count >= MaxCount) return false;
        Fishes.Add(((FishBase) (
            retarded
                ? new RetardedFish(speed)
                : task
                    ? new TaskFish(speed)
                    : new ThreadFish(speed))).Start());
        return true;
    }

    public static bool TryKillFish(int id) =>
        Fishes.Remove(Fishes.FirstOrDefault(fish => fish.Id == id)!);

    public static string GetAllJson() =>
        JsonSerializer.Serialize(Fishes);
}
