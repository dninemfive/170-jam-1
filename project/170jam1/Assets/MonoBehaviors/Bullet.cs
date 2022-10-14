using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward.normalized * Time.deltaTime * 7);
    }
    private void OnTriggerEnter(Collider other)
    {
        // `CompareTag` is apparently faster, and more importantly, suppresses UNT0002
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            GameManager.Instance.CurrentMap.DestroyTilesInRadius(transform.position, 2f);
            Destroy(this);
        }
    }
}
