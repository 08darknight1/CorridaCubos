using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    [SerializeField] private GameObject [] playerOnMap;

    [SerializeField] private int[] playerScore;

    private GameObject winObject;

    [SerializeField] private GameObject textWinner;

    [SerializeField] private int playerOnTheMapSize;
    
    
    
    void Start()
    {
        findObjects();  
        playerScore = new int [playerOnMap.Length];
        playerOnTheMapSize = playerOnMap.Length;
        winObject = GameObject.Find("Win");
        DontDestroyOnLoad(winObject);
    }

    // Update is called once per frame
    void Update()
    {         
        checkWinner();
        findObjects();      
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")         // && playerOnMap.Length == 2)
        {
            for (int x = 0; x < playerOnMap.Length; x++)
            {
                if (col.gameObject.name == playerOnMap[x].name)
                {    
                    playerScore[x]++;
                    checkWinner();
                }
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);         
            
            checkWinner();
            
        }     
    }

    private void findObjects()
    {
        textWinner = GameObject.FindGameObjectWithTag("Respawn"); 
        
        playerOnMap = GameObject.FindGameObjectsWithTag("Player");

        switch (playerOnMap.Length)
        {
            case 2:
                playerOnMap[0] = GameObject.Find("Player1");
                playerOnMap[1] = GameObject.Find("Player2");
            break;
            case 3:
                playerOnMap[0] = GameObject.Find("Player1");
                playerOnMap[1] = GameObject.Find("Player2");
                playerOnMap[2] = GameObject.Find("Player3");
                break;
            case 4:
                playerOnMap[0] = GameObject.Find("Player1");
                playerOnMap[1] = GameObject.Find("Player2");
                playerOnMap[2] = GameObject.Find("Player3");
                playerOnMap[3] = GameObject.Find("Player4");
                break;
        }
        
        checkWinner();
    }

    private void checkWinner()
    {
        for (int x = 0; x < playerOnTheMapSize; x++)
        {       
            var y = SceneManager.GetActiveScene().buildIndex;
            
            if (playerScore[x] >= 3 && y != 5)
            {
                SceneManager.LoadScene(5);      
            }

            if (y == 5){
                textWinner = GameObject.FindGameObjectWithTag("Respawn");
                var bug = playerScore.Max();
                var bug2 = playerScore.ToList().IndexOf(bug) + 1;
                textWinner.GetComponent<Text>().text = "Jogador "+ bug2 +" Venceu";
            }
        }
    }
}
