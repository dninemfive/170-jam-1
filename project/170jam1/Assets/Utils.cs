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
        new() { new(238 / 255, 186 / 255, 244 / 255), new(200 / 255, 1, 1) },
        new() { new(217 / 255, 217 / 255, 217 / 255), new(164 / 255, 180 / 255, 205) },
        new() { new(1 / 255, 0, 29 / 255), new(192 / 255, 192 / 255, 190 / 255) },
        new() { new(180 / 255, 135 / 255, 100 / 255), new(130 / 255, 97 / 255, 77 / 255) }
    };
}
