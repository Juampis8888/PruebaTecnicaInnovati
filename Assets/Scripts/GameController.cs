using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    public PlayerController playerController;

    public UIController uiController;

    public Spawner spawnEnemies;

    private bool isCall = true;

    private bool isGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
        }else
        {
           if(Getint("Tutorial") == 1)
           {
                uiController.HidenTutorial();
           }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerController.GetRun() & isCall)
        {   
            isGame = true;
            spawnEnemies.CallSpawn();
            isCall = false;
        }

        if(!playerController.GetRun() & !isCall)
        {  
            spawnEnemies.CancelSpawn();
            isCall = true;
        }

        if(playerController.GetDie() & isGame)
        {
            isGame = false;  
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    public bool GetIsGame()
    {
        return isGame;
    }

}
