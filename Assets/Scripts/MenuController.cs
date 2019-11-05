using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{    
    private bool timeOn = false, macete = false;
    
    [SerializeField] private float timeLeft = 60f;
    
    public float timerNumber;
    
    [SerializeField] private GameObject[] playerOnMap = new GameObject[4];
    
    public KeyCode[] key; 
    
    public int playersProntos = 0;

    private GameObject textoTimer;
    
    private void Awake()
    {  
        key = new KeyCode[4];
        
     /*   playerOnMap[0] = GameObject.Find("Player1");
        playerOnMap[1] = GameObject.Find("Player2");
        playerOnMap[2] = GameObject.Find("Player3");
        playerOnMap[3] = GameObject.Find("Player4");*/

        playerOnMap = GameObject.FindGameObjectsWithTag("Player");

        textoTimer = GameObject.Find("TextTimer");
        
        timeLeft = timerNumber;
    }

    void Start()
    {
        findPlayers();
        
        for (int x = 0; x < playerOnMap.Length; x++)
        {
            if (playerOnMap[x].tag == "Player")
            {
                playerOnMap[x].SetActive(false);
            }
        }
        
        key[0] = KeyCode.Z;
        key[1] = KeyCode.X;
        key[2] = KeyCode.C;
        key[3] = KeyCode.V;
        
        textoTimer.GetComponent<Text>().text = timeLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ActivatePlayers();
        
        if (timeOn == true)
        {
            timeLeft -= Time.deltaTime;
            var kkk = timeLeft;
            textoTimer.GetComponent<Text>().text = timeLeft.ToString("N0");
            if ( timeLeft < 0 )
            {
                SceneManager.LoadScene(1);
            }

            if (playersProntos >= 4 && macete == false)
            {
                timeLeft = 5;
                macete = true;
            }
        }
        
        playersReady();
    }

    public void ActivatePlayers()
    {
        for (int x = 0; x < playerOnMap.Length; x++)
        {
            if (Input.GetKeyDown(key[x]))
            {
                PlayersOnMap.playersOnMap[x] = true;
                playerOnMap[x].SetActive(true);
            }         
        }
    }

    public void playersReady()
    {
        playersProntos = 0;
        
        for (int x = 0; x < playerOnMap.Length; x++)
        {
            if (playerOnMap[x].gameObject.active == true)
            {
                playersProntos = playersProntos + 1;
            }
        }
        if (playersProntos >= 2)
        {
            timeOn = true;
        }
        else
        {
            timeOn = false;
            timeLeft = timerNumber;
        }
    }

    private void findPlayers()
    {
        playerOnMap[0] = GameObject.Find("Player1");
        playerOnMap[1] = GameObject.Find("Player2");
        playerOnMap[2] = GameObject.Find("Player3");
        playerOnMap[3] = GameObject.Find("Player4");
    }
}
