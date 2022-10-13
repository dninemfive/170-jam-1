using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior which controls enemies and causes them to move and attack the player.
/// </summary>


public class Enemy : MonoBehaviour
{
    ///reference to the player object
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(player)
        {
            transform.LookAt(player.transform, new Vector3(0, 1, 0));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)0.009);
            transform.Rotate(90, 0, 0);
        }
    }
}
