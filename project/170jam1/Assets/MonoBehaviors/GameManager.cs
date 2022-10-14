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
    [SerializeField]
    private GameObject DebugSphere;
    //timer for enemies spawn
    
    [SerializeField] float spawnTime = 6.0f;
    float timer;
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
    private GameObject _camera;
    public GameObject Camera => _camera;
    /// <summary>
    /// The GameObject representing the player.
    /// </summary>
    [SerializeField]
    private GameObject _player;
    public GameObject Player => _player;
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
        Debug.Log($"Camera: {Camera}\nCurrentMap: {CurrentMap}");
        Camera.transform.position = CurrentMap.CameraPosition;
        _player.transform.position = CurrentMap.PlayerPosition;
        DebugSphere.transform.position = CurrentMap.PlayerPosition;
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
    [SerializeField]
    private Prefabs _prefabs;
    /// <summary>
    /// An object containing the prefabs to spawn in via scripts.
    /// </summary>
    public Prefabs Prefabs => _prefabs;
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
        //ensures the first spawn is always 3 secs after the begining of the game
        timer = spawnTime - 3.0f;
        if (Root is not null) throw new Exception("Attempted to initialize a new GameManager but one already existed.");
        Root = gameObject;
        GenerateMaps();
        GoToMap(0);
    }

    private void Update()
    {
        //spawn enemy every 10 secs
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer = 0.0f;
            spawnEnemy();
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

    void spawnEnemy()
    {
        //select random position
        Vector3 position = new Vector3(UnityEngine.Random.Range(15, 0), _player.transform.position.y ,UnityEngine.Random.Range(8, 0));
        //if the position is far enough away from the player then spawn otherwise try again next frame
        if(Vector3.Distance(position, _player.transform.position) > 5)
        {
            Instantiate(Prefabs.Enemy, position, Quaternion.identity);
        } else {
            timer = spawnTime;
        }
    }
}
