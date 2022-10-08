using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Abstracts the concept of tiles, taking either individual tile textures or an entire image and providing tiles either way.
/// </summary>
public class Tileset
{
    private Board<Texture2D> _tileTextures;
    /// <summary>
    /// Get the tile texture at a specific position.
    /// </summary>
    /// <param name="x">The x-coordinate of the texture to retrieve.</param>
    /// <param name="z">The z-coordinate of the texture to retrieve.</param>
    /// <returns>The <see href="https://docs.unity3d.com/ScriptReference/Texture2D.html">Texture2D</see> for the tile with coordinates (x, z).</returns>
    public Texture2D this[int x, int z]
    {
        get => _tileTextures[x, z];
    }
    /// <summary>
    /// Loads a texture, scales it appropriately, cuts it up into <see cref="GameManager.NUM_TILES_X">NUM_TILES_X</see>×<see cref="GameManager.NUM_TILES_Z">NUM_TILES_Z</see> portions, 
    /// and stores them for later retrieval.
    /// </summary>
    /// <param name="texPath">A path to an image file representing a map's tile textures.</param>
    /// <remarks>See also:<br/>
    /// - Unity docs: <see href="https://docs.unity3d.com/ScriptReference/ImageConversion.LoadImage.html">ImageConversion.LoadImage()</see></remarks>
    public void Load(string texPath)
    {
        Texture2D tex = new(1,1);
        if (!tex.LoadImage(new TextAsset(texPath).bytes)) throw new ArgumentException($"Could not load image at {texPath}.");
        Scale(tex);
        _tileTextures = new(delegate (int x, int z)
        {
            throw new NotImplementedException();
        });
    }
    /// <summary>
    /// Scales the Material to match the dimensions <see cref="GameManager.NUM_TILES_X"/>×<see cref="GameManager.NUM_TILES_Z"/>.
    /// </summary>
    /// <remarks>
    /// See also:<br/>
    /// - <see href="https://answers.unity.com/questions/150942/texture-scale.html"/>
    /// </remarks>
    /// <param name="unscaled">The Material to scale.</param>
    /// <param name="scalingMode">Whether to use bicubic scaling, which "blurs" the image to increase the size, or linear scaling, which maintains pixel accuracy.</param>
    public static void Scale(Texture2D unscaled, ScalingMode scalingMode = ScalingMode.Bicubic)
    {

    }
}
/// <summary>
/// A scaling mode to be used with <see cref="Tileset.Scale(Material)"/>
/// </summary>
public enum ScalingMode { Bicubic, Linear }