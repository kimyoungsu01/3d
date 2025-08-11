using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curvalue;
    public float starValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;
    // Start is called before the first frame update
    void Start()
    {
        curvalue = starValue;
    }

    // Update is called once per frame
    void Update()
    {
        // ui 업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage() 
    { 
      return curvalue / maxValue;
    }

    public void Add(float value) 
    {
        curvalue = Mathf.Min(curvalue + value, maxValue);
    }

    public void Subtract(float value) 
    {
        curvalue = Mathf.Max(curvalue - value, 0);
    }
}
