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
    [SerializeField] int knockbackForce = 2;
    Rigidbody rb;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(player)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)0.009);
            transform.Rotate(90, 0, 0);
        }
        //if(player)
        //{
        //    Vector3 knockback = new Vector3(5, 0, 5);
        //    rb.AddForce(Vector3.Scale((transform.position - player.transform.position), knockback);
        //}
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
            
            //rb.AddForce(knockback);
            //transform.position = knockback;
            Debug.Log("check");
        }
    }
}
