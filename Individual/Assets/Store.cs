using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {

    public GameObject prefab;
	// Use this for initialization
	void Start () {
        GameObject obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        obj.GetComponent<Collider2D>().sharedMaterial.bounciness = 0.0f;
        obj.GetComponent<Collider2D>().sharedMaterial.friction = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void increaseBounciness()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        float bounc = 0;
        foreach (GameObject ball in balls)
        {
            PhysicsMaterial2D pm = ball.GetComponent<Collider2D>().sharedMaterial;
            bounc = pm.bounciness;
            if (bounc < 1)
                pm.bounciness += 0.1f;
            bounc = pm.bounciness;
            ball.GetComponent<Collider2D>().sharedMaterial = pm;
            ball.GetComponent<Rigidbody2D>().sharedMaterial = pm;
        }
        Debug.Log("Bounciness: " + bounc);
    }
    public void decreaseFriction()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        float frict =0;
        foreach (GameObject ball in balls)
        {
            PhysicsMaterial2D pm = ball.GetComponent<Collider2D>().sharedMaterial;
            frict = pm.friction;
            if (frict >0)
                pm.friction -= 0.05f;
            frict = pm.friction;
            ball.GetComponent<Collider2D>().sharedMaterial = pm;
            ball.GetComponent<Rigidbody2D>().sharedMaterial = pm;
            
        }
        Debug.Log("Friction: " + frict);
    }
    public void increaseForce()
    {
        Bounce.speed += 10;
        Debug.Log("Bounce speed: " + Bounce.speed);
    }
    public void split()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Vector3 ballPos = ball.transform.position;
            GameObject obj = Instantiate(prefab, ballPos, Quaternion.identity) as GameObject;
            obj.GetComponent<Transform>().localScale = ball.transform.localScale / 1.5f;
            obj.GetComponent<Rigidbody2D>().velocity = (new Vector2(ball.GetComponent<Rigidbody2D>().velocity.x - 5, ball.GetComponent<Rigidbody2D>().velocity.y));
            obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            foreach(GameObject ballx in balls)
            {
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(),ballx.GetComponent<Collider2D>());
            }
            ball.GetComponent<Transform>().localScale = ball.transform.localScale /1.5f;
            ball.GetComponent<Rigidbody2D>().velocity = (new Vector2(5+ ball.GetComponent<Rigidbody2D>().velocity.x, ball.GetComponent<Rigidbody2D>().velocity.y));
        }

    }
    public void increaseSize()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.transform.localScale *= 1.05f;
        }
    }
}
