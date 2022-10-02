using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root component which holds and manages all other components.
/// </summary>
public class GameManager : MonoBehaviour
{
    public const int NUM_TILES_X = 16, NUM_TILES_Y = 9;
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
}
