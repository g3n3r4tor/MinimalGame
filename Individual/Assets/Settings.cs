using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Canvas canvas;
    private AudioListener audioListener;
    public Text sound;
    public Text pause;
    public static Settings instance;
    // Use this for initialization
	void Awake () {
        Time.timeScale = 0;
        audioListener = (AudioListener)Camera.main.gameObject.GetComponent<AudioListener>();
        instance = this;
    }

    public void StartGame()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pause.text = "Resume Game";
        Time.timeScale = 0;
        canvas.enabled = true;
    }
    public void TriggerSound()
    {
        if (audioListener.enabled)
        {
            sound.text = "Turn ON Sound";
        }
        else
        {
            sound.text = "Turn OFF Sound";
        }
        audioListener.enabled = !audioListener.enabled;
    }
}
