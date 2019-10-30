using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //    var kkk = other.transform.GetComponent<PlayerController>().spawnPoint;
            //  other.gameObject.transform.position = kkk.position;
            other.transform.position = other.transform.GetComponent<PlayerController>().spawnPoint;
        }
    }
}
