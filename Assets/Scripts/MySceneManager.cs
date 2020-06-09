using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            Cursor.visible = false;
        else Cursor.visible = true;
    }
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangePlayers(bool singlePlayer)
    {
        GetComponent<GameManager>().singlePlayer = singlePlayer;
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index++;
        if (index >= 4) index = 0;
        SceneManager.LoadScene(index);
    }

    public void Reset()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        if(SceneManager.GetActiveScene().buildIndex!=0)
            SceneManager.LoadScene(0);
    }
}
