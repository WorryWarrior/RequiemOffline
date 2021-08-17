using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingUI : MonoBehaviour
{
    private Slider slider;
    public Image image;
    public bool isSupposedToBeSeen = false;
    public static CastingUI Instance { get; private set; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (slider.value == slider.minValue || slider.value == slider.maxValue)
        {
            image.enabled = false;
        } else
        {
            image.enabled = true;
        }
        if (isSupposedToBeSeen)
        {
            slider.value = Time.time;
        }
    }
    public void SetCastTime(float castTime)
    {
        isSupposedToBeSeen = true;
        slider.minValue = Time.time;
        slider.maxValue = Time.time + castTime;
    }
}
