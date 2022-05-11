using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    Image hitIndicator;
    float alpha = 0f;

    public static Action<float> onIndicatorChange;

    void Start()
    {
        hitIndicator = gameObject.GetComponent<Image>();
    }

    void OnEnable()
    {
        //hitIndicator.color = new Color(1f, 1f, 1f, 0f);
        onIndicatorChange += IndicatorChange;
    }

    void OnDisable()
    {
        onIndicatorChange -= IndicatorChange;
    }

    void IndicatorChange(float value)
    {
        alpha += value;
    }

    void Update()
    {
        if(alpha > 0f)
        {
            alpha -= 0.1f * Time.deltaTime;
            hitIndicator.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}
