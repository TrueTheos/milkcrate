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
    [SerializeField] public AnimationClip jumpAnimation;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public List<GameObject> crates = new List<GameObject>();

    private bool isMidair = false;
    private Animation anim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(SliderMove.instance.CanJump && !isMidair)
            {
                Jump();

                if(Score % SliderMove.instance.PointsToSpeedUp == 0){
                    SliderMove.instance.SpeedUp();
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
        anim.clip = jumpAnimation;
        anim.Play();

        Score++;

        scoreText.text = Score.ToString();

        isMidair = true;

        foreach (var crate in crates)
        {
            Vector3 cratePos = crate.GetComponent<RectTransform>().anchoredPosition;
            crate.GetComponent<Crate>().index--;
            int crateIndex = crate.GetComponent<Crate>().index;

            if(crateIndex == -1)
            {
                crate.GetComponent<RectTransform>().DOLocalMove(new Vector3(cratePos.x - 400, cratePos.y - 400, 0), jumpDuration);
                StartCoroutine(ResetCrate(crate));
            }
            else
            {
                crate.GetComponent<RectTransform>().DOLocalMove(new Vector3(cratePos.x - 400, cratePos.y - 400, 0), jumpDuration);
            }
        }
    }
    private bool ShowedContinue = false;
    [SerializeField] private GameObject ContinueScreen;
    private void Fall()
    {
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
        ShowedContinue = false;
        ContinueScreen.SetActive(false);
    }

    IEnumerator ResetCrate(GameObject crate)
    {
        yield return new WaitForSeconds(jumpDuration);
        crate.GetComponent<RectTransform>().anchoredPosition = new Vector3(1200, 500, 0);
        crate.GetComponent<Crate>().index = 5;
        isMidair = false;
    }
}
