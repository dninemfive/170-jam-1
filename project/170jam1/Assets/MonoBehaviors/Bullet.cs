using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // make bullet come from mouth instead of center of body
        transform.Translate(-transform.forward.normalized * 0.5f);
    }
    void Update()
    {
        transform.Translate(-transform.forward.normalized * Time.deltaTime * 7);
    }
    private void OnTriggerEnter(Collider other)
    {
        // `CompareTag` is apparently faster, and more importantly, suppresses UNT0002
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.CurrentMap.DestroyTilesInRadius(transform.position, 2f);
            // quick hack so the bullets aren't visible after this. not sure why destroying them doesn't make them invisible
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this);
        }
    }
}
