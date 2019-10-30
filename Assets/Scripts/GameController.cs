using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] playerOnMap;
        
    public int PointsToWin;

    [SerializeField] private int[] playerScore;

    private GameObject winObject, canvasObject;

    [SerializeField] private GameObject textWinner;
    
    public int LastLevel;

    void Awake()
    {
        findObjects();  
        playerScore = new int [playerOnMap.Length];
        canvasObject = GameObject.FindGameObjectWithTag("UI");
        canvasObject.SetActive(false);
    }    
    
    void Start()
    {
        findObjects();  
        winObject = GameObject.Find("Win");
        DontDestroyOnLoad(winObject);
        playerScore = new int [playerOnMap.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && canvasObject.activeSelf == false){
            canvasObject.SetActive(true);
            checkWinner();
        }

        for (int x = 0; x < playerOnMap.Length; x++){
             if (playerOnMap[x] == null){
                 findObjects();
             }
         }
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
                }
            }

            /* |||||||| ESQUEMA DE FASES SEQUENCIAIS|||||||||\
            if (SceneManager.GetActiveScene().buildIndex < 4 && SceneManager.GetActiveScene().buildIndex != 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
            }

            if (SceneManager.GetActiveScene().buildIndex == 4 && SceneManager.GetActiveScene().buildIndex != 5)
            {
                SceneManager.LoadScene(0);
            }                    
            /* |||||||| ESQUEMA DE FASES SEQUENCIAIS|||||||||\ */
            
            RamdomizeLevel();
            
            checkWinner();
            
            findObjects();
            
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
    }

    private void checkWinner()
    {
        for (int x = 0; x < playerOnMap.Length; x++)
        {       
            var y = SceneManager.GetActiveScene().buildIndex;
            
            if (playerScore[x] >= PointsToWin && y != 5)
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

    private void RamdomizeLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        var leveltoLoad = level;
        LastLevel = leveltoLoad;

        while (leveltoLoad == level && leveltoLoad == LastLevel)
        {
            leveltoLoad = Random.Range(0, 5);
            Debug.Log(leveltoLoad);
        }
        
        SceneManager.LoadScene(leveltoLoad);
    }
}
