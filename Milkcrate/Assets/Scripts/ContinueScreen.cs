using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueScreen : MonoBehaviour {

    public float TimeToStopContinue = 5f;
    public Image slider;
    public GameObject AdButton;
    private void Start() {
        StartCoroutine(MoveSliderTimer(1,0,TimeToStopContinue));
    }

    public void WatchAdButton(){
        // Show ad
        AdManager.instance.PlayAd();

        StopCoroutine("MoveSliderTimer");



        gameObject.SetActive(false);
    }
    private IEnumerator MoveSliderTimer( float v_start, float v_end, float Speed)
    {
        
        float elapsed = 0.0f;
        while (elapsed < Speed)
        {
            slider.fillAmount = Mathf.Lerp( v_start, v_end, elapsed / Speed );
            elapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(0);
    }

    public void OnEnable()
    {
        if(AdManager.instance.usedAd)
        {
            SceneManager.LoadScene(0);
        }
    }
}