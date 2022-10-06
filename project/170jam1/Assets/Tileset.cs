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
    private Board<Material> _tileTextures;
    public Material this[int x, int z]
    {
        get => _tileTextures[x, z];
    }
}
