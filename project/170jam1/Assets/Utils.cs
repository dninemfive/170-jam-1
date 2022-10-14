using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Returns a vector corresponding to the input offset by the given amounts in each dimension, which all default to 0.
    /// </summary>
    /// <param name="vec">The vector from which to start.</param>
    /// <param name="x">How much to offset the input in the x dimension, default 0.</param>
    /// <param name="y">How much to offset the input in the y dimension, default 0.</param>
    /// <param name="z">How much to offset the input in the z dimension, default 0.</param>
    /// <returns>See summary.</returns>
    /// <remarks>This is what's known as an <em>extension method</em>, and in addition to the normal way, can be called as if it were a method on <c>Vector3</c>.</remarks>
    public static Vector3 Translate(this Vector3 vec, float x = 0f, float y = 0f, float z = 0f) => new(vec.x + x, vec.y + y, vec.z + z);
    public static TileState ToTileState(this bool b) => b switch
    {
        true => TileState.Visible,
        false => TileState.Hidden
    };
    public static bool? ToBool(this TileState ts) => ts switch
    {
        TileState.Visible => true,
        TileState.Hidden => false,
        _ => throw new Exception("TileState.ToBool() is only valid for values Visible and Hidden.")
    };
    public static float DistanceFrom(this Vector3 a, Vector3 b) => Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
    public static float DistanceFrom(this Tile tile, Vector3 b) => DistanceFrom(tile.transform.position, b);
    public static readonly List<List<Color>> DebugColors = new()
    {
        new() { new(238 / 255f, 186 / 255f, 244 / 255f), new(200 / 255f, 1, 1) },
        new() { new(217 / 255f, 217 / 255f, 217 / 255f), new(164 / 255f, 180 / 255f, 205f) },
        new() { new(1 / 255f, 0, 29 / 255f), new(192 / 255f, 192 / 255f, 190 / 255f) },
        new() { new(180 / 255f, 135 / 255f, 100 / 255f), new(130 / 255f, 97 / 255f, 77 / 255f) }
    };
}
