using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// The number of tiles in the y (vertical) direction.
    /// </summary>
    public const int NUM_TILES_Y = 9;
    [HasComponent(typeof(Player))]
    public GameObject Player;
    [HaveComponent(typeof(Map))]
    public List<GameObject> Maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Enumerates every possible coordinate on the board.
    /// </summary>
    public static IEnumerable<(int x, int y)> EnumerateAllCoordinates
    {
        get
        {
            for (int i = 0; i < NUM_TILES_X; i++)
                for (int j = 0; j < NUM_TILES_Y; j++)
                    yield return (i, j);
        }
    }
}
