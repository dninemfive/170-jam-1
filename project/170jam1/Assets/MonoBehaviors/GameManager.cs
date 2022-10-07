using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Disables the compiler warning that Update() isn't called, since it isn't aware of Unity doing so.
#pragma warning disable IDE0052
// Disables the compiler warning that _camelCase variable names are not standard.
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
    /// <summary>
    /// The camera which is a fixed distance from the current map.
    /// </summary>
    [SerializeField]
    private GameObject Camera;
    /// <summary>
    /// The GameObject representing the player.
    /// </summary>
    [SerializeField]
    private GameObject Player;
    /// <summary>
    /// A list of Map components corresponding to the generated maps. Private because we don't want to mess with it after generation.
    /// </summary>
    private List<Map> _maps { get; set; } = new();
    /// <summary>
    /// The index of the current map. Private because this should only be changed via <see cref="GoToMap(int)"/>.
    /// </summary>
    private int _mapIndex { get; set; }
    /// <summary>
    /// The map the player is currently on.
    /// </summary>
    public Map CurrentMap => _maps[_mapIndex];
    /// <summary>
    /// Goes to a specified map. Moves the camera to follow it and hides the maps above it.
    /// </summary>
    /// <param name="mapIndex">The index of the map to go to.</param>
    public void GoToMap(int mapIndex)
    {
        if (mapIndex is < 0 or >= NUM_MAPS) Debug.LogWarning($"Map index {mapIndex} is out of the range [0..{NUM_MAPS})!");
        _mapIndex = mapIndex;
        Camera.transform.position = CurrentMap.CameraPosition;
        for (int i = 0; i < _maps.Count; i++) _maps[i].Visible = _mapIndex <= i;
    }
    /// <summary>
    /// Goes to the next map. If it goes past the end, wraps back around to the first map.
    /// </summary>
    public void GoToNextMap() => GoToMap((++_mapIndex) % NUM_MAPS);
    /// <summary>
    /// Goes to the previous map. If it goes past the end, wraps around to the last map.
    /// </summary>
    public void GoToPreviousMap() => GoToMap((--_mapIndex) % NUM_MAPS);
    /// <summary>
    /// An object containing the prefabs to spawn in via scripts.
    /// </summary>
    public Prefabs Prefabs { get; private set; }
    /// <summary>
    /// The root GameObject, which holds the only valid instance of GameManager.
    /// </summary>
    public static GameObject Root { get; private set; } = null;
    /// <summary>
    /// The valid instance of GameManager, static so that there is precisely one.
    /// </summary>
    private static GameManager _instance { get; set; } = null;
    /// <summary>
    /// A public reference to <see cref="_instance"/>. Makes sure that it is the root GameManager.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (_instance is null) _instance = Root.GetComponent<GameManager>();
            return _instance;
        }        
    }
    /// <summary>
    /// Essentially, starts the game. Sets up the <see cref="Root"/> <c>GameObject</c>, generates the maps into the world, and positions the camera properly.
    /// </summary>
    /// <remarks>Called before the first frame update.</remarks>
    void Start()
    {
        if (Root is not null) throw new Exception("Attempted to initialize a new GameManager but one already existed.");
        Root = gameObject;
        GenerateMaps();
        GoToMap(0);
    }
    /// <summary>
    /// Currently, debug code. Tracks the elapsed time so it can be used in <see cref="Update"/>.
    /// </summary>
    public float ElapsedTime { get; private set; } = 0;
    /// <summary>
    /// Currently, debug code. Goes to the next map every second.
    /// </summary>
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if(ElapsedTime > 1)
        {
            ElapsedTime = 0;
            GoToNextMap();
        }
    }
    /// <summary>
    /// Generates <see cref="NUM_MAPS"/> maps into the world.
    /// </summary>
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
    /// Enumerates every possible coordinate on an instance of <see cref="Board{T}"/>.
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
