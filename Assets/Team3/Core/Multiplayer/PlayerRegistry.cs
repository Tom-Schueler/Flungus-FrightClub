using System.Collections.Generic;
using Team3.Characters;

public static class PlayerRegistry
{
    public static readonly Dictionary<ulong, PlayerStats> playerStatsByClient = new();

    public static void Register(ulong clientId, PlayerStats stats)
    {
        playerStatsByClient[clientId] = stats;
    }

    public static PlayerStats GetStats(ulong clientId)
    {
        playerStatsByClient.TryGetValue(clientId, out var stats);
        return stats;
    }

    public static void Unregister(ulong clientId)
    {
        playerStatsByClient.Remove(clientId);
    }

    public static void ClearAll()
    {
        playerStatsByClient.Clear();
    }
}