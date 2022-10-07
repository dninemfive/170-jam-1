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
    /// The artstyle used by this specific Map.
    /// </summary>
    Artstyle Artstyle;
    /// <summary>
    /// A list of enemies on this map.
    /// </summary>
    [HaveComponent(typeof(Enemy))]
    List<GameObject> Enemies;
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
            if (Tiles is null) Debug.LogError($"Map at {transform.position} has an uninitialized Tiles object!");
            foreach (GameObject go in Tiles.AllItems)
            {
                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                if (mr is null) Debug.LogError($"MeshRenderer for tile at {go.transform.position} is null!");
                else mr.enabled = _visible;
            }
        }
    }
    /// <summary>
    /// Creates this map and initializes its Tiles; doing the latter causes the component GameObjects to be created.
    /// </summary>
    /// <remarks>Called before the first frame update.</remarks>
    void Start()
    {
        Debug.Log($"Map initializing at {transform.position}");
        Tiles = new(delegate(int x, int z)
        {
            GameObject prefab = Instantiate(Prefabs.Tile);
            if (x % 2 == z % 2) prefab.GetComponent<MeshRenderer>().material.color = Color.black;
            prefab.transform.position = new(x, transform.position.y, z);
            return prefab;
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// The desired position of the camera above this map, namely centered on the XZ plane and <see cref="GameManager.CAMERA_DISTANCE"/> units above the map.
    /// </summary>
    public Vector3 CameraPosition
    {
        get
        {
            float x = transform.position.x + GameManager.NUM_TILES_X / 2f + 0.5f;
            float y = transform.position.y + GameManager.CAMERA_DISTANCE;
            float z = transform.position.z + GameManager.NUM_TILES_Z / 2f - 0.5f;
            return new(x, y, z);
        }
    }
}
