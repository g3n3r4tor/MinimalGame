using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceCount : MonoBehaviour {

    public int bouncec;
    public int kash { get; set; }
    public float size { get; set; }
    public GameObject text;
    public GameObject kashText;
	// Use this for initialization
	void Start () {
        bouncec = 0;
        size = 100;
        kash = 0;
        text = GameObject.Find("BounceCounter");
        kashText = GameObject.Find("Kash");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void callThis(float height)
    {
        Debug.Log("Size: " + size);
        int add = (int)(((height + 140) / 20) * (size / 100));
        kash += add;
        Debug.Log(add);
        bouncec++;
        text.GetComponent<Text>().text = "Bounces: " + bouncec;
        updateKash();
    }
    public void updateKash()
    {
        kashText.GetComponent<Text>().text = "Kash: " + kash;
    }

}
