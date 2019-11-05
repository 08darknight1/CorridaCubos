using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBlock : MonoBehaviour
{
  //  public GameObject gameController;
    public bool Respawn;
    
    void Start()
    {    
       // gameController = GameObject.Find("Win");  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(Respawn == true){
                other.transform.position = other.transform.GetComponent<PlayerController>().spawnPoint;
            }
            else{
                Destroy(other.gameObject);
            }
        }
        
    }

 
}
