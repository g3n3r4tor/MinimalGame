using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {

    public static Store instance;
    public GameObject prefab;
    public BounceCount bc;
    public GameObject errorText;
    public Image errorOverlay;
    public int[] kashNeeded = new int[4];
    public float[] actualKash = new float[4];
    public float bounc { get; private set; }
    // Use this for initialization
    void Start () {
        instance = this;
        bounc = 0;
        kashNeeded[0] = 1;
        actualKash[0] = 1;
        kashNeeded[1] = 2;
        actualKash[1] = 2;
        kashNeeded[2] = 4;
        actualKash[2] = 4;
        kashNeeded[3] = 8;
        actualKash[3] = 8;

    }
    public void createBall(float bounciness,float size)
    {
        GameObject obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        obj.GetComponent<Collider2D>().sharedMaterial.bounciness = bounciness;
        obj.GetComponent<Collider2D>().sharedMaterial.friction = 1.0f;
        obj.GetComponent<Transform>().localScale = new Vector3(size,size,1);
        bc = GetComponentInParent<BounceCount>();
        BounceCount.instance.addBall(obj);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            foreach (GameObject ballx in balls)
                Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), ballx.GetComponent<Collider2D>());
        }
        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings.instance.PauseGame();
        }
    }
    public void loadStuff(float[] cashNeeded, float bouncy, int ballAmount,float size)
    {
        actualKash = cashNeeded;
        bounc = bouncy;
        for(int i = 0; i < ballAmount; i++) {
            createBall(bounc, size);
        }
        
        int[] kashneeded = new int[4];
        for (int i = 0; i < 4; i++)
        {
            kashneeded[i] = Mathf.FloorToInt(cashNeeded[i]);
        }
        CostHolder.instance.updateCost(kashneeded);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        PhysicsMaterial2D pm = balls[0].GetComponent<Collider2D>().sharedMaterial;
        pm.bounciness = bounc;
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Collider2D>().sharedMaterial = pm;
            ball.GetComponent<Rigidbody2D>().sharedMaterial = pm;
        }
    }
    public void increaseBounciness()
    {
        //Debug.Log(bc.kash);
        
        if (bc.kash >= kashNeeded[0] && bounc < 1)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            PhysicsMaterial2D pm = balls[0].GetComponent<Collider2D>().sharedMaterial;
            bounc += 0.1f;
            pm.bounciness = bounc;
            
            foreach (GameObject ball in balls)
            {
                ball.GetComponent<Collider2D>().sharedMaterial = pm;
                ball.GetComponent<Rigidbody2D>().sharedMaterial = pm;
            }
            if (bounc >= 1)
            {
                CostHolder.instance.maxed = true;
            }
            //Debug.Log("Bounciness: " + bounc);
            bc.kash -= kashNeeded[0];
            actualKash[0] *= 2f;
            kashNeeded[0] = Mathf.FloorToInt(actualKash[0]);
            bc.updateKash();
            CostHolder.instance.updateCost(kashNeeded);
            
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
        if (bc.kash >= kashNeeded[2])
        {
            if(Bounce.speed <= float.MaxValue)
                Bounce.speed *= 1.05f;
            else
            {
                Bounce.speed = float.MaxValue;
            }
            Debug.Log("Bounce speed: " + Bounce.speed);
            bc.kash -= kashNeeded[2];
            actualKash[2] *= 2.5f;
            kashNeeded[2] = Mathf.FloorToInt(actualKash[2]);
            CostHolder.instance.updateCost(kashNeeded);
            bc.updateKash();
        }
        else
        {
            StartCoroutine(ScreenMessage("#Nopoors"));
        }
    }
    public void split()
    {
        if (bc.size > 100 && bc.kash >= kashNeeded[3])
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
                BounceCount.instance.addBall(obj);
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
            bc.kash -= kashNeeded[3];
            actualKash[3] *= 3f;
            kashNeeded[3] = Mathf.FloorToInt(actualKash[3]);
            bc.updateKash();
            CostHolder.instance.updateCost(kashNeeded);

        }
        else
        {
            StartCoroutine(ScreenMessage("Balls aint big enough"));
        }
    }
    public void increaseSize()
    {
        if (bc.kash >= kashNeeded[1])
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                ball.transform.localScale *= 1.05f;

                ball.GetComponents<Rigidbody2D>();
            }
            bc.kash -= kashNeeded[1];
            actualKash[1] *= 1.20f;
            kashNeeded[1] = Mathf.FloorToInt(actualKash[1]);
            bc.updateKash();
            bc.size *= 1.05f;
            CostHolder.instance.updateCost(kashNeeded);

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
        errorOverlay.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1);
        errorOverlay.GetComponent<Image>().enabled = false;
        text.enabled = false;
    }
}
