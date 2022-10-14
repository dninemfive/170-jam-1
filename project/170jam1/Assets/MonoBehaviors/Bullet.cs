using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward.normalized * Time.deltaTime * 7);
        if(true /* how do collisions work??? */)
        {
            GameManager.Instance.CurrentMap.DestroyTilesInRadius(transform.position, 2f);
            Destroy(this);
        }
    }
}
