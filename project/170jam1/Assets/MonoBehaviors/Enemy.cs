using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior which controls enemies and causes them to move and attack the player.
/// </summary>


public class Enemy : MonoBehaviour
{
    ///reference to the player object
    [SerializeField] int knockbackForce = 2;
    GameObject player;
    Rigidbody rb;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(player)
        {
            //look at the player
            Transform lookPoint = player.transform;
            lookPoint.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.LookAt(lookPoint);
            transform.Rotate(90, 0, 0);

            //move towards the player
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), (float)0.009);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if(playerRb != null)
            {
                Vector3 knockback = (player.transform.position - transform.position).normalized * knockbackForce;
                playerRb.AddForce(knockback, ForceMode.Impulse);
            }
        }
    }
}
