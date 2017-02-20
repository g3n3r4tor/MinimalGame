using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Canvas canvas;
    private AudioListener audioListener;
    public Text sound;
    public Text pause;
    private bool audio = true;
    public Button disableThis;
    private bool first = true;
    public static Settings instance;
    // Use this for initialization
	void Awake () {
        Time.timeScale = 0;
        audioListener = (AudioListener)Camera.main.gameObject.GetComponent<AudioListener>();
        instance = this;
        Screen.SetResolution(1400,600,false);
        Application.runInBackground = true;
    }

    public void StartGame()
    {
        canvas.enabled = false;
        if (first)
        {
            Store.instance.createBall(0, 100);
            first = false;
        }
        Time.timeScale = 1;
        disableThis.enabled = false;

    }
    public void QuitGame()
    {
        Save();
        Time.timeScale = 1;
        Application.Quit();
    }

    public void PauseGame()
    {

        pause.text = "Resume Game";
        
        canvas.enabled = true;
        disableThis.enabled = false;
        Time.timeScale = 0;
    }
    public void TriggerSound()
    {

        if (audio)
        {
            AudioListener.volume = 0;
            sound.text = "Turn ON Sound";
        }
        else
        {
            AudioListener.volume = 1;
            sound.text = "Turn OFF Sound";
        }
        audio = !audio;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);
        PlayerData data = new PlayerData();
        data.Bounciness = Store.instance.bounc;
        data.Size = BounceCount.instance.size;
        data.Force = Bounce.speed;
        data.CashNeeded = Store.instance.actualKash;
        data.Bounces = BounceCount.instance.bouncec;
        data.Kash = BounceCount.instance.kash;
        data.balls = GameObject.FindGameObjectsWithTag("Ball").Length;
        //Debug.Log("Am I here?");
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (first) { 
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                canvas.enabled = false;
                first = false;
                Time.timeScale = 1;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                Bounce.speed = data.Force;
                Store.instance.loadStuff(data.CashNeeded, data.Bounciness, data.balls, data.Size);
                BounceCount.instance.size = data.Size;
                BounceCount.instance.bouncec = data.Bounces;
                BounceCount.instance.kash = data.Kash;
                BounceCount.instance.callThis(0);
                
            }

        }
        disableThis.enabled = false;
    }
}

[Serializable]
class PlayerData
{
    public float Bounciness { get; set; }
    public float Size { get; set; }
    public float Force { get; set; }
    public float[] CashNeeded { get; set; }
    public int Bounces { get; set; }
    public int Kash { get; set; }
    public int balls { get; set; }

}
