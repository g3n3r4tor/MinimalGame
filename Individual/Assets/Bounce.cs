using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {



    public Rigidbody2D rb;
    public static float speed = 50;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnMouseDown(){
        rb.velocity = new Vector2(rb.velocity.x, speed);

        Debug.Log("YEP");
    }
}
