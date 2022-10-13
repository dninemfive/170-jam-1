using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class tracking an instance of a 'verse and its (potentially missing) tiles.
/// </summary>
public class Map : MonoBehaviour
{
    /// <summary>
    /// A <see cref="Board{GameObject}"/> holding the component tiles of this map.
    /// </summary>
    public Board<GameObject> Tiles { get; private set; }
    /// <summary>
    /// Internal variable used by <see cref="Visible"/>.
    /// </summary>
    private bool _visible = true;
    /// <summary>
    /// Whether or not the component tiles of this map are visible. Setting this modifies all said tiles to match the specified state.
    /// </summary>
    public bool Visible
    {
        get => _visible;
        set 
        {
            _visible = value;
            foreach (GameObject go in Tiles.AllItems)
            {
                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                mr.enabled = _visible;
            }
        }
    }
    /// <summary>
    /// Creates this map and initializes its Tiles; doing the latter causes the component GameObjects to be created.
    /// </summary>
    /// <remarks>Called before the first frame update.</remarks>
    void Awake()
    {
        Debug.Log($"{this}: initializing");
        Tiles = new(delegate(int x, int z)
        {
            GameObject prefab = Instantiate(Prefabs.Tile);
            if (x % 2 == z % 2) prefab.GetComponent<MeshRenderer>().material.color = Color.black;
            prefab.transform.position = new(x, transform.position.y, z);
            return prefab;
        });
    }
    /// <summary>
    /// The representation of this Map when printed.
    /// </summary>
    /// <returns>$"Map at {transform.position}"</returns>
    public override string ToString() => $"Map at {transform.position}";
    /// <summary>
    /// The desired position of the camera above this map, namely centered on the XZ plane and <see cref="GameManager.CAMERA_DISTANCE"/> units above the map.
    /// </summary>
    public Vector3 CameraPosition => new(PlayerPosition.x, PlayerPosition.y + GameManager.CAMERA_DISTANCE, PlayerPosition.z);
    /// <summary>
    /// The starting position of the player on this map, namely centered on the XZ plane at the same height.
    /// </summary>
    public Vector3 PlayerPosition
    {
        get
        {
            float x = transform.position.x + GameManager.NUM_TILES_X / 2f + 0.5f;
            // epsilon to make sure it's above the map in case of rounding errors
            float y = transform.position.y + float.Epsilon;
            float z = transform.position.z + GameManager.NUM_TILES_Z / 2f - 0.5f;
            return new(x, y, z);
        }
    }
}
