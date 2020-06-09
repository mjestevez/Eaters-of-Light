using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private List<PlayerController> players;
    private int activePlayer = 0;
    public string actionAxis = "Fire1";
    private float action;
    public bool actionPressed = false;
    public CameraController cameraSinglePlayer;
    public CameraController cameraMultiplayer1;
    public CameraController cameraMultiplayer2;
    public bool singlePlayer=true;
    private bool gameOver = false;
    public GameObject panelGameOver;
    private float counter = 0f;
    private bool disabled = false;
    [Header("Music")]
    public AudioClip clip;
    public float volume;

    private void Awake()
    {
        if(panelGameOver)panelGameOver.SetActive(false);
        if (instance == null) instance = this;
        else if (instance != this)
        {
            singlePlayer = instance.singlePlayer;
            Destroy(instance.gameObject);
            instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        players = new List<PlayerController>();
        players.Add(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>());
        players.Add(GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<PlayerController>());
        if (!singlePlayer && cameraSinglePlayer)
        {
            cameraSinglePlayer.gameObject.SetActive(false);
            cameraMultiplayer1.gameObject.SetActive(true);
            cameraMultiplayer2.gameObject.SetActive(true);
            players[0].GetComponent<PlayerController>().Multiplayer();
            players[1].GetComponent<PlayerController>().enabled = true;
        }
        AudioManager.instance.ChangeMusic(clip, volume);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) GetComponent<MySceneManager>().Reset();
        if (Input.GetKeyDown(KeyCode.Escape)) GetComponent<MySceneManager>().MainMenu();
        if (!disabled)
        {
            if (gameOver)
            {
                counter += Time.deltaTime;
                if (counter >= 10f)
                {
                    counter = 0;
                    gameOver = false;
                    panelGameOver.SetActive(true);
                    GetComponent<AudioSource>().Play();
                    Invoke("ChangeScene", 3f);
                }
            }
            else
            {
                if (singlePlayer)
                {
                    action = Input.GetAxisRaw(actionAxis);
                    if (action != 0)
                    {
                        if (!actionPressed)
                        {
                            players[activePlayer].ChangeCharacter();
                            activePlayer += 1;
                            activePlayer %= 2;
                            players[activePlayer].enabled = true;
                            cameraSinglePlayer.target = players[activePlayer].gameObject;
                            cameraSinglePlayer.MoveCamera();
                        }
                        actionPressed = true;
                    }
                    else actionPressed = false;

                }
            }
        }
        
        

        
    }

    public void BlueTreasure()
    {
        if (singlePlayer)
        {
            cameraSinglePlayer.GetComponent<ColorShader>().deleteBlue();
        }
        else
        {
            cameraMultiplayer1.GetComponent<ColorShader>().deleteBlue();
            cameraMultiplayer2.GetComponent<ColorShader>().deleteBlue();
        }
        AudioManager.instance.EndMusic();
        gameOver = true;
    }

    public void RedTreasure()
    {
        if (singlePlayer)
        {
            cameraSinglePlayer.GetComponent<ColorShader>().deleteRed();
        }
        else
        {
            cameraMultiplayer1.GetComponent<ColorShader>().deleteRed();
            cameraMultiplayer2.GetComponent<ColorShader>().deleteRed();
        }
        AudioManager.instance.EndMusic();
        gameOver = true;
    }

    public void GreenTreasure()
    {
        if (singlePlayer)
        {
            cameraSinglePlayer.GetComponent<ColorShader>().deleteGreen();
        }
        else
        {
            cameraMultiplayer1.GetComponent<ColorShader>().deleteGreen();
            cameraMultiplayer2.GetComponent<ColorShader>().deleteGreen();
        }
        AudioManager.instance.EndMusic();
        gameOver = true;
    }

    public void ChangeScene()
    {
        GetComponent<MySceneManager>().NextScene();
    }

    public void DisableControl()
    {
        disabled = true;
    }

    public void ActivateControl()
    {
        disabled = false;

    }
}
