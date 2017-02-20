using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BounceCount : MonoBehaviour {
    public static BounceCount instance;
    public int bouncec;
    public int kash { get; set; }
    public float size { get; set; }
    public GameObject text;
    public GameObject kashText;
    public List<GameObject> balls ;
	// Use this for initialization
	void Start () {
        instance = this;
        balls = new List<GameObject>();
        bouncec = 0;
        size = 100;
        kash = 0;
        text = GameObject.Find("BounceCounter");
        kashText = GameObject.Find("Kash");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject item in balls)
            {
                Bounce tmp = item.GetComponent<Bounce>();
                tmp.rabble(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
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
    public void addBall(GameObject ball)
    {
        balls.Add(ball);
    }

}
