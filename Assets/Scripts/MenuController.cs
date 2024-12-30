using Rewired;
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

    private GameObject textoTimer;

    //

    private bool _gameStarted, _registerNewKey = true;

    private int _playersReady;

    private Player _rewiredInitialPlayer;

    public GameObject CubeRacerPrefab;

    private List<PlayerController> _playersList;


    void Start()
    {
        textoTimer = GameObject.Find("TextTimer");

        timeLeft = timerNumber;

        textoTimer.GetComponent<Text>().text = timeLeft.ToString();

        _playersList = new List<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStarted)
        {
            ActivatePlayers();

            if (timeOn == true)
            {
                timeLeft -= Time.deltaTime;
                var kkk = timeLeft;
                textoTimer.GetComponent<Text>().text = timeLeft.ToString("N0");
                if (timeLeft < 0)
                {
                    SceneManager.LoadScene(1);
                }

                if (_playersReady >= 4 && macete == false)
                {
                    timeLeft = 5;
                    macete = true;
                }
            }

            //playersReady();
        }
    }

    public void ActivatePlayers()
    {
        if(_playersReady < 4)
        {
            //ReInput.controllers.

            _rewiredInitialPlayer = ReInput.players.GetPlayer(_playersReady);

            if (_rewiredInitialPlayer.controllers.Keyboard.PollForFirstKeyDown().success)
            {
                var pressedKey = _rewiredInitialPlayer.controllers.Keyboard.PollForFirstKeyDown().keyboardKey;

                if(_playersList.Count > 0) 
                {
                    for (int x = 0; x < _playersList.Count; x++)
                    {
                        if (_playersList[x].ReturnJumpKey() == pressedKey)
                        {
                            _registerNewKey = false;
                            break;
                        }
                        else
                        {
                            _registerNewKey = true;
                        }
                    }
                }

                if (_registerNewKey)
                {
                    var newRacer = Instantiate(CubeRacerPrefab);

                    _playersList.Add(newRacer.GetComponent<PlayerController>());

                    newRacer.GetComponent<PlayerController>().SetPlayerJumpKey(_playersReady, pressedKey);

                    _playersReady++;
                }
            }
        }
    }

    /*
    public void playersReady()
    {
        _playersReady = 0;
        
        for (int x = 0; x < playerOnMap.Length; x++)
        {
            if (playerOnMap[x].gameObject.activeSelf)
            {
                _playersReady = _playersReady + 1;
            }
        }
        if (_playersReady >= 2)
        {
            timeOn = true;
        }
        else
        {
            timeOn = false;
            timeLeft = timerNumber;
        }
    }*/
}

/*
         playerOnMap = GameObject.FindGameObjectsWithTag("Player");
 * */

