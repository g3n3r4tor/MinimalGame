﻿using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {


    public AudioClip bounceAudio;
    public Rigidbody2D rb;
    public static float speed = 50;
    public BounceCount bc;
    float height;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        height = 0;
        GetComponent<AudioSource>().playOnAwake = false;
	}
	
    public void setBouncecount(BounceCount bc)
    {
        this.bc = bc;
    }
	// Update is called once per frame
	void Update () {
        if(height < transform.position.y)
            height = transform.position.y;
    }

    void OnMouseDown(){
        rb.velocity = new Vector2(rb.velocity.x, speed);

        Debug.Log("YEP");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        if (collision.collider.name == "botWall")
        {
            float tmp;
            if (height >= -35)
            {
                tmp = height;
                bc.callThis(tmp);
               // Debug.Log("Height:" + height);
            }

        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        height = -140;
    }
}
