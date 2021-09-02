using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int Score;

    public static PlayerController instance;

    [SerializeField] public float jumpDuration;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public List<GameObject> crates = new List<GameObject>();

    private bool isMidair = false;
    private bool isAlive = true;
    private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAlive)
        {
            if(SliderMove.instance.CanJump)
            {
                if(!isMidair)
                {
                    Jump();

                    if (Score % SliderMove.instance.PointsToSpeedUp == 0)
                    {
                        SliderMove.instance.SpeedUp();
                    }
                }                
            }
            else
            {
                Fall();
            }
        }
    }

    private void Jump()
    {
        foreach (var crate in crates)
        {
            Vector3 cratePos = crate.GetComponent<RectTransform>().anchoredPosition;
            crate.GetComponent<Crate>().index--;
            int crateIndex = crate.GetComponent<Crate>().index;

            if (crateIndex == -1)
            {
                crate.GetComponent<RectTransform>().DOLocalMove(new Vector3(cratePos.x - 320, cratePos.y - 230, 0), jumpDuration);
                StartCoroutine(ResetCrate(crate));
            }
            else
            {
                crate.GetComponent<RectTransform>().DOLocalMove(new Vector3(cratePos.x - 320, cratePos.y - 230, 0), jumpDuration);
            }
        }

        Score++;

        scoreText.text = Score.ToString();

        isMidair = true;
    }
    private bool ShowedContinue = false;
    [SerializeField] private GameObject ContinueScreen;
    private void Fall()
    {
        isAlive = false;

        PlayerPrefs.SetInt("LastScore", Score);

        if(PlayerPrefs.GetInt("Highscore") < Score)
        {
            PlayerPrefs.SetInt("Highscore", Score);
        }

        PlayerPrefs.Save();

        if(!ShowedContinue){
            // Show continue
            ContinueScreen.SetActive(true);

            ShowedContinue = true;
        }else{
            // Gameover
        }
    }

    public void Reviwe()
    {
        isAlive = true;
        ShowedContinue = false;
        ContinueScreen.SetActive(false);
    }

    IEnumerator ResetCrate(GameObject crate)
    {
        yield return new WaitForSeconds(jumpDuration);
        crate.GetComponent<RectTransform>().anchoredPosition = new Vector3(1280, 220, 0);
        crate.GetComponent<Crate>().index = 7;
        isMidair = false;
    }
}
