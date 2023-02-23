using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{   
    public PlayerController playerController;

    public GameObject tutorialGameObject;

    public GameObject titleGameObject;

    public TextMeshProUGUI scoreMaxText;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI titleText;

    private int score;

    private float timeElapsed;

    void Start()
    {   
        if(!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();
        }else
        {
           scoreMaxText.text =  GetInt("Score").ToString();
        }
        
    }

    void Update()
    {   
        if(!playerController.GetDie() & playerController.GetRun())
        {   
            HidenTitle();
            timeElapsed += Time.deltaTime;
            score += Mathf.FloorToInt(timeElapsed);
            scoreText.text = score.ToString();
        }
    }

    public void IsDead()
    {
        SetInt("Score",score);
        ShowTitle("Perdiste... di reiniciar para comenzar de nuevo");
    }

    public void SetInt(string KeyName, int Value)
    {   
        if(Value > GetInt(KeyName) )
        {
            PlayerPrefs.SetInt(KeyName, Value);
        }    
    }

    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    public void HidenTutorial()
    {
        tutorialGameObject.SetActive(false);
    }

    public void ShowTutorial()
    {
        tutorialGameObject.SetActive(true);
    }

    public void HidenTitle()
    {
        titleGameObject.SetActive(false);
    }

    public void ShowTitle(string value)
    {   
        titleText.text = value;
        titleGameObject.SetActive(true);
    }

}
