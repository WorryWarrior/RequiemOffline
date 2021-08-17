using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMusicHandler : MonoBehaviour
{
    public AudioSource source = null;
    public AudioClip playInsideCity = null;
    public AudioClip playOutsideCity = null;
    public Transform cityCenter;
    public float cityRadius;
    private bool allowed = true;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            source.clip = playInsideCity;
            source.Play();
        }*/
        if (Vector2.Distance(FindObjectOfType<PlayerHealth>().transform.position, cityCenter.position) < cityRadius)
        {
            if (allowed)
            {
                source.clip = playInsideCity;
                source.Play();
                allowed = false;
            }
        }
        else
        {
            source.clip = playOutsideCity;
            source.Play();
        }
    }
}
