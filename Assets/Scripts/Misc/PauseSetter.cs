using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSetter : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI = null;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EnemyTargetManager.Instance.target == null)
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            Pause();
        } else
        {
            Unpause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        isPaused = false;
    }
}
