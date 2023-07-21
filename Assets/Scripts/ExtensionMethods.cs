using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static float Map(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float Limit(this float value, float from, float to)
    {
        var min = Math.Min(from, to);
        var max = Math.Max(from, to);
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    private static readonly Random rng = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}