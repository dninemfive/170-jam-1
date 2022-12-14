using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior which controls enemies and causes them to move and attack the player.
/// </summary>


public class Enemy : MonoBehaviour
{
    [SerializeField] int knockbackForce = 2;
    private GameObject player => GameManager.Instance.Player;
    Rigidbody rb;
    Collider col;
    // Update is called once per frame
    void Update()
    {
        
        if(player)
        {
            //look at the player
            //Transform lookPoint = player.transform;
            //lookPoint.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //transform.LookAt(lookPoint);
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.Rotate(90, 0, 0);

            //move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)0.009);
            
        }
        if(!(0.2 > (transform.position.y - player.transform.position.y) && (transform.position.y - player.transform.position.y) > -0.2))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // `CompareTag` is apparently faster, and more importantly, suppresses UNT0002
        if(other.CompareTag("Player"))
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
