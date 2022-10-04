using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which holds the prefab GameObjects in one helpful place.
/// </summary>
public class Prefabs : MonoBehaviour
{
    public static Prefabs Instance => GameManager.Instance.Prefabs;
    public static GameObject Map => Instance._map;
    [SerializeField]
    private GameObject _map;
    public static GameObject Tile => Instance._tile;
    [SerializeField]
    private GameObject _tile;
}
