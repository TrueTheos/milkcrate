using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour {

    public float TimeToStopContinue = 5f;
    public Image slider;
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

        // Show GameOver
    }
}