using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C# doesn't like naming private variables with the _camelCase notation
#pragma warning disable IDE1006
/// <summary>
/// Root component which holds and manages all other components.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The number of tiles in the x (horizontal) direction.
    /// </summary>
    public const int NUM_TILES_X = 16;
    /// <summary>
    /// The number of tiles in the z (vertical w/r/t the top-down view) direction.
    /// </summary>
    public const int NUM_TILES_Z = 9;
    /// <summary>
    /// Distance of the camera above the map. Eventually, should be calculated from a desired proportion of the screen space.
    /// </summary>
    public const float CAMERA_DISTANCE = 10f;
    /// <summary>
    /// The number of maps, which should correspond to the number of artstyles loaded but does not necessarily need to do so.
    /// </summary>
    public const int NUM_MAPS = 5;
    [HasComponent(typeof(Player))]
    public GameObject Player;
    [HaveComponent(typeof(Map))]
    public List<GameObject> Maps;
    public Prefabs Prefabs;
    public static GameObject Root { get; private set; } = null;
    private static GameManager _instance { get; set; } = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null) _instance = Root.GetComponent<GameManager>();
            return _instance;
        }        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Root is not null) throw new Exception("Attempted to initialize a new GameManager but one already existed.");
        Root = gameObject;
        GenerateMaps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateMaps()
    {
        for(int i = 0; i < NUM_MAPS; i++)
        {
            Debug.Log($"Generating new map at (0, {-i}, 0)");
            Maps.Add(Instantiate(Prefabs.Map, new Vector3(0, -i, 0), Quaternion.identity));
        }
    }
    /// <summary>
    /// Enumerates every possible coordinate on the board.
    /// </summary>
    public static IEnumerable<(int x, int z)> EnumerateAllCoordinates
    {
        get
        {
            for (int i = 0; i < NUM_TILES_X; i++)
                for (int j = 0; j < NUM_TILES_Z; j++)
                    yield return (i, j);
        }
    }
}
