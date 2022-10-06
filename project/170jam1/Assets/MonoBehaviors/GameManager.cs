using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject Camera;
    [HasComponent(typeof(Player))]
    public GameObject Player;
    [HaveComponent(typeof(Map))]
    private List<Map> _maps { get; set; } = new();
    public IEnumerable<Map> Maps => _maps.AsEnumerable();
    private int _mapIndex { get; set; }
    public Map CurrentMap => _maps[_mapIndex];
    public void GoToMap(int mapIndex)
    {
        if (mapIndex is < 0 or >= NUM_MAPS) Debug.LogWarning($"Map index {mapIndex} is out of the range [0..{NUM_MAPS})!");
        _mapIndex = mapIndex;
        Camera.transform.position = CurrentMap.CameraPosition;
    }
    public void GoToNextMap() => GoToMap((++_mapIndex) % NUM_MAPS);
    public void GoToPreviousMap() => GoToMap((++_mapIndex) % NUM_MAPS);
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
        GoToMap(0);
    }
    public float ElapsedTime { get; private set; } = 0;
    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime > 5) Maps.First().Visible = false;
    }
    public void GenerateMaps()
    {
        for(int i = 0; i < NUM_MAPS; i++)
        {
            Debug.Log($"Generating new map at (0, {-i}, 0)");
            GameObject go = Instantiate(Prefabs.Map, new Vector3(0, -i, 0), Quaternion.identity);
            _maps.Add(go.GetComponent<Map>());
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
