using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public TMP_Text HighscoreText,LastscoreText;
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
}
