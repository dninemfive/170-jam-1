using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward;
        if(false /* how do collisions work??? */)
        {
            GameManager.Instance.CurrentMap.DestroyTilesInRadius(transform.position, 2f);
        }
    }
}
