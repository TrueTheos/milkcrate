using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public TMP_Text HighscoreText,LastscoreText;
    private bool disclaimerEnabled = false;

    public GameObject obj1, obj2, obj3;

    void Start()
    {
        
        int Highscore = PlayerPrefs.GetInt("Highscore",0);
        int Lastscore = PlayerPrefs.GetInt("LastScore",0);

        HighscoreText.SetText($"Highscore\n{Highscore}");
        LastscoreText.SetText($"Lastscore\n{Lastscore}");
    }

    public void GoToScene(int index){
        SceneManager.LoadScene(index);
    }

    public void Disclaimer()
    {
        if (disclaimerEnabled)
        {
            obj1.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(false);
            disclaimerEnabled = false;
        }
        else
        {
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(true);
            disclaimerEnabled = true;
        }
    }
}
