using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {



    public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x + 10, rb.velocity.y);
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnMouseDown(){
        Vector2 curVelocity = rb.velocity;
        rb.velocity = new Vector2(curVelocity.x, curVelocity.y+200);

        Debug.Log("YEP");
    }
}
