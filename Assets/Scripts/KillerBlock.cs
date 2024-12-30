using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBlock : MonoBehaviour
{
    private bool _respawn;

    private void Start()
    {
        _respawn = GlobalConfig.killBlockRespawn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(_respawn){
                other.transform.position = other.transform.GetComponent<PlayerController>().spawnPoint;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }   
    }
}
