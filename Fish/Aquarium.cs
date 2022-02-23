using System.Text;
using System.Text.Json;

namespace Fish;

public static class Aquarium
{
    private const int maxSpeed = 10;
    private const int maxCount = 10;
    private static List<FishBase> Fishes { get; } = new();

    public static bool TryAddFish(bool task)
    {
        if (Fishes.Count >= maxCount) return false;
        Fishes.Add(((FishBase) (task
            ? new TaskFish(Random.Shared.Next(maxSpeed) + 1)
            : new ThreadFish(Random.Shared.Next(maxSpeed) + 1))).Start());
        return true;
    }

    public static bool TryKillFish(int id) =>
        Fishes.Remove(Fishes.FirstOrDefault(fish => fish.Id == id)!);

    public static string GetAllJson() =>
        Fishes.Aggregate(new StringBuilder(), (builder, fish) => builder.Append(JsonSerializer.Serialize(fish)))
            .ToString();
}