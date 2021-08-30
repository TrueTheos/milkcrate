using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMove : MonoBehaviour
{
    public static SliderMove instance;
    [Header("GamePlay")]
    [SerializeField] private float Speed;
    public float SpeedUpAmount;
    public float SpeedUpCap;
    public int PointsToSpeedUp;

    public float GreenSize;
    public float ShrinkAmount;
    public float ShrinkCap;
    public int PointsToShrink;
    [Header("Compontents")]
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform Red,Green;
    [SerializeField] private RectTransform ArrowRect;
    [HideInInspector] public bool CanJump;
    float valueFrom,valueTo;
    private float RedWidth,GreenWidth;
    private float GreenPrst;

    public void SpeedUp(){
        // Decrease the Speed var
        Speed -= SpeedUpAmount;

        // Clamp the Speed so it doesn't go lower than the cap
        Speed = Mathf.Clamp(Speed,SpeedUpCap,5);
        
        // Decrease the Green box size var
        GreenSize -= ShrinkAmount;

        // Clamp the Green box size so it doesn't go lower than the cap
        GreenSize = Mathf.Clamp(GreenSize,ShrinkCap,GreenSize);

        // Update it on the UI
        Green.sizeDelta = new Vector2(GreenSize, Green.rect.height);

        // Update the persentage
        StartCoroutine(GetWidth());

    }



    private void Awake() {
        instance = this;
    }
    private void Start() {
        valueFrom = 0;
        valueTo = 100;
        StartCoroutine(MoveSlider(valueFrom, valueTo));
        StartCoroutine(GetWidth());
    }
    
    private void Update() {
        if(valueFrom >= 95)
            valueTo = 0;
        else if(valueFrom <= 5)
            valueTo = 100;
        
        if(Input.GetMouseButtonDown(0)){
            Clicked();
        }
    }
    public Text test;
    public Animator SliderAnim;
    private void Clicked(){
        SliderAnim.SetTrigger("Click");
        if(ArrowRect.anchorMax.x <= 0.5 + GreenPrst / 2 && ArrowRect.anchorMax.x >= 0.5 - GreenPrst / 2){
            Debug.Log("Correct");
            CanJump = true;
            //test.text = "Can Jump: True";
        }else{
            Debug.Log("Wrong");
            CanJump = false;
            //test.text = "Can Jump: False";
        }
    }

    private IEnumerator MoveSlider( float v_start, float v_end)
    {
        float elapsed = 0.0f;
        while (elapsed < Speed)
        {
            valueFrom = Mathf.Lerp( v_start, v_end, elapsed / Speed );
            elapsed += Time.deltaTime;
            slider.value = valueFrom;
            yield return null;
        }
        valueFrom = v_end;
        StartCoroutine(MoveSlider(valueFrom, valueTo));
    }

    private IEnumerator GetWidth() {
        yield return new WaitForEndOfFrame();
        Green.sizeDelta = new Vector2(GreenSize, Green.rect.height);
        
        RedWidth = Red.rect.width;
        GreenWidth = Green.rect.width;
        GreenSize = GreenWidth;
        GreenPrst = GreenWidth / RedWidth;
    }
}
