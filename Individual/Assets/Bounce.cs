using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {


    public AudioClip bounceAudio;
    public Rigidbody2D rb;
    public static float speed = 10000000;
    float height;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        height = 0;
        GetComponent<AudioSource>().playOnAwake = false;
        if (CostHolder.instance.first)
        {
            firstOne();
            CostHolder.instance.first = false;
        }
        BounceCount.instance.addBall(this.gameObject);
        
    }
	public void firstOne()
    {
        PhysicsMaterial2D pm = this.gameObject.GetComponent<Collider2D>().sharedMaterial;
        pm.bounciness = 0;
        pm.friction = 1;
        this.gameObject.GetComponent<Collider2D>().sharedMaterial = pm;
        this.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = pm;
       
    }
	// Update is called once per frame
	void Update () {
        if (this.gameObject.transform.position.x > 2000 || this.gameObject.transform.position.x < -2000 || this.gameObject.transform.position.y > 2000 || this.gameObject.transform.position.y < -2000)
            this.gameObject.transform.position = new Vector2(0, 0);
        if (height < transform.position.y)
            height = transform.position.y;
    }
    public void rabble(Vector2 force)
    {
        Vector2 heading = new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y) - force;
        float distance = heading.magnitude;
        //Debug.Log("Distance: " + distance);
        //Debug.Log("heading: " + heading.normalized);
        float scal = this.gameObject.transform.localScale.x;
        if(distance < 5)
        {
            distance = 5;
        }
        if(distance+100 < 25+scal ||distance < 25)
            rb.AddForce(heading.normalized*(speed/distance));
    }
   /* void OnMouseDown(){
        rb.velocity = new Vector2(rb.velocity.x, speed);
        
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        if (collision.collider.name == "botWall")
        {
            float tmp;
            if (height >= -35)
            {
                tmp = height;
                BounceCount.instance.callThis(tmp);
               // Debug.Log("Height:" + height);
            }

        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        height = -140;
    }
}
