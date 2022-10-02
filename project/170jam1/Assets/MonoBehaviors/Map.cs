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
    public GameObject TilePrefab;
    public Board<GameObject> Tiles { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Tiles = new(delegate(int x, int y)
        {
            GameObject prefab = Instantiate(TilePrefab);
            if(x % 2 == y % 2) prefab.GetComponent<MeshRenderer>().material.color = Color.black;
            // TODO: decide whether to rename all references to use z or to write down that y = z somewhere
            prefab.transform.position = new(x, transform.position.z, y);
            return prefab;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
