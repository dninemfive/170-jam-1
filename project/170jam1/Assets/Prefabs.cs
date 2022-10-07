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
    /// The GameObject prefab corresponding to each component tile of a Map.
    /// </summary>
    public static GameObject Tile => Instance._tile;
    /// <summary>
    /// Private non-static field backing <see cref="Tile"/> so it can be set in the editor.
    /// </summary>
    [SerializeField]
    private GameObject _tile;
}
