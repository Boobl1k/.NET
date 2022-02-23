using System.Text;
using System.Text.Json;

namespace Fish;

public static class Aquarium
{
    private const int maxCount = 30;
    private static List<FishBase> Fishes { get; } = new();

    public static bool TryAddFish(bool task, int speed)
    {
        if (Fishes.Count >= maxCount) return false;
        Fishes.Add(((FishBase) (task
            ? new TaskFish(speed)
            : new ThreadFish(speed))).Start());
        return true;
    }

    public static bool TryKillFish(int id) =>
        Fishes.Remove(Fishes.FirstOrDefault(fish => fish.Id == id)!);

    public static string GetAllJson() =>
        JsonSerializer.Serialize(Fishes);
}