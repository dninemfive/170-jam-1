using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class tracking an instance of a 'verse and its (potentially missing) tiles.
/// </summary>
public class Map : MonoBehaviour
{
    Artstyle Artstyle;
    [HaveComponent(typeof(Enemy))]
    List<GameObject> Enemies;
    public Board<GameObject> Tiles { get; private set; }
    private bool _visible = true;
    public bool Visible
    {
        get => _visible;
        set 
        {
            _visible = value;
            if (Tiles is null) Debug.Log("ti4jae;t");
            foreach (GameObject go in Tiles.AllItems)
            {
                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                if (mr is null) Debug.Log("null!");
                else mr.enabled = _visible;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("huh???");
        Debug.Log(transform.position.z);
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
    /// The desired position of the camera above the map.
    /// </summary>
    public Vector3 CameraPosition
    {
        get
        {
            // center the camera in the x plane
            float x = transform.position.x + GameManager.NUM_TILES_X / 2f;
            float y = transform.position.y + GameManager.CAMERA_DISTANCE;
            // center the camera in the y plane
            float z = transform.position.z + GameManager.NUM_TILES_Z / 2f - 0.5f;
            return new(x, y, z);
        }
    }
}
