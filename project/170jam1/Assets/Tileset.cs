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
    private List<List<Material>> _tileTextures;
    public Material this[int x, int y]
    {
        get
        {
            if (x is < 0 or >= GameManager.NUM_TILES_X) throw new ArgumentOutOfRangeException(nameof(x));
            if (y is < 0 or >= GameManager.NUM_TILES_Y) throw new ArgumentOutOfRangeException(nameof(y));
            return _tileTextures[x][y];
        }
    }
}
