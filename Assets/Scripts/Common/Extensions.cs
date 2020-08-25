using System.Collections.Generic;

/// <summary>
/// Static extension methods.
/// </summary>
public static class Extensions
{
    private static readonly System.Random rand = new System.Random();

    /// <summary>
    /// Shuffles a list according to the Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}