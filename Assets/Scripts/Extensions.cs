using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class Extensions {
    public static Vector3[] ToVector3(this Vector2[] vectors) {
        return System.Array.ConvertAll<Vector2, Vector3>(vectors, v => v);
    }

    public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize) {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }
}