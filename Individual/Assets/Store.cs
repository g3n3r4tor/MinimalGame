using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {

    public GameObject prefab;
    public BounceCount bc;
    public GameObject errorText;
    public int[] kashNeeded = new int[4];

    float bounc = 0;
    // Use this for initialization
    void Start () {
        GameObject obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        obj.GetComponent<Collider2D>().sharedMaterial.bounciness = 0.0f;
        obj.GetComponent<Collider2D>().sharedMaterial.friction = 1.0f;
        bc = GetComponentInParent<BounceCount>();
        Bounce b = obj.GetComponent<Bounce>();
        b.setBouncecount(bc);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void increaseBounciness()
    {
        //Debug.Log(bc.kash);
        
        if (bc.kash >= kashNeeded[0] && bounc < 1)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            float tmp = bounc;
            PhysicsMaterial2D pm = balls[0].GetComponent<Collider2D>().sharedMaterial;
            pm.bounciness += 0.1f;
            bounc = pm.bounciness;
            foreach (GameObject ball in balls)
            {
                ball.GetComponent<Collider2D>().sharedMaterial = pm;
                ball.GetComponent<Rigidbody2D>().sharedMaterial = pm;
            }
            //Debug.Log("Bounciness: " + bounc);
            bc.kash -= kashNeeded[0];
            bc.updateKash();
        }
        else if(bounc < 1)
        {
            StartCoroutine(ScreenMessage("#Nopoors"));
        }
        else
        {
            StartCoroutine(ScreenMessage("Already Maxed"));
        }
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
        if (bc.kash >= kashNeeded[1])
        {
            Bounce.speed += 10;
            Debug.Log("Bounce speed: " + Bounce.speed);
            bc.kash -= kashNeeded[1];
            bc.updateKash();
        }
        else
        {
            StartCoroutine(ScreenMessage("#Nopoors"));
        }
    }
    public void split()
    {
        if (bc.size > 100 && bc.kash >= kashNeeded[2])
        {
            Debug.Log(bc.size);
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                Vector3 ballPos = ball.transform.position;
                GameObject obj = Instantiate(prefab, ballPos, Quaternion.identity) as GameObject;
                obj.GetComponent<Transform>().localScale = ball.transform.localScale / 1.5f;
                obj.GetComponent<Rigidbody2D>().velocity = (new Vector2(ball.GetComponent<Rigidbody2D>().velocity.x - 5, ball.GetComponent<Rigidbody2D>().velocity.y + 10));
                obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                Bounce b = obj.GetComponent<Bounce>();
                b.setBouncecount(bc);
                ball.GetComponent<Transform>().localScale = ball.transform.localScale / 1.5f;
                ball.GetComponent<Rigidbody2D>().velocity = (new Vector2(5 + ball.GetComponent<Rigidbody2D>().velocity.x, ball.GetComponent<Rigidbody2D>().velocity.y - 10));
            }
            bc.size /= 1.5f;
            balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                foreach (GameObject ballx in balls)
                    Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), ballx.GetComponent<Collider2D>());
            }
            bc.kash -= kashNeeded[2];
            bc.updateKash();
        }
        else
        {
            StartCoroutine(ScreenMessage("Balls aint big enough"));
        }
    }
    public void increaseSize()
    {
        if (bc.kash >= kashNeeded[3])
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                ball.transform.localScale *= 1.05f;

                ball.GetComponents<Rigidbody2D>();
            }
            bc.kash -= kashNeeded[3];
            bc.updateKash();
            bc.size *= 1.05f;
        }
        else
        {
            StartCoroutine(ScreenMessage("#Nopoors"));
        }
    }
    IEnumerator ScreenMessage(string message)
    {
        Text text = errorText.GetComponent<Text>();
        text.text = message;
        text.enabled = true;
        yield return new WaitForSeconds(1);
        text.enabled = false;
    }
}
