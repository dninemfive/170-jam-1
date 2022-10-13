using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which holds the prefab GameObjects in one helpful place.
/// </summary>
public class Prefabs : MonoBehaviour
{
    /// <summary>
    /// The singleton instance of <see cref="GameManager"/>, aliased here for readability.
    /// </summary>
    public static Prefabs Instance => GameManager.Instance.Prefabs;
    /// <summary>
    /// The GameObject prefab corresponding to each instance of <see cref="Map"/>.
    /// </summary>
    public static GameObject Map => Instance._map;
    /// <summary>
    /// Private non-static field backing <see cref="Map"/> so it can be set in the editor.
    /// </summary>
    [SerializeField]
    private GameObject _map;
    /// <summary>
    /// The GameObject prefab corresponding to each component tile of a Map.
    /// </summary>
    public static GameObject Tile => Instance._tile;
    /// <summary>
    /// Private non-static field backing <see cref="Tile"/> so it can be set in the editor.
    /// </summary>
    [SerializeField]
    private GameObject _tile;
    /// <summary>
    /// The GameObject prefab corresponding to enemies.
    /// </summary>
    public static GameObject Enemy => Instance._enemy;
    /// <summary>
    /// Private non-static field backing <see cref="Enemy"/> so it can be set in the editor.
    /// </summary>
    [SerializeField]
    private GameObject _enemy;
}
