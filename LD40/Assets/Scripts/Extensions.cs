using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T PickRandom<T>(this IEnumerable<T> enumerable)
    {
        var index = UnityEngine.Random.Range(0, enumerable.Count());

        return enumerable.ElementAt(index);
    }
}