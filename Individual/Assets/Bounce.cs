using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {


    
    float speedx;
    float speedy;
    float posX = 0;
    float posY = 0;
    bool goUp = false;
    bool goLeft = true;   
	// Use this for initialization
	void Start () {
        int randLeft = Random.Range(0, 2);
        goLeft = randLeft == 1 ? true : false;
        
        float randX = Random.Range(0f, 2f);
        float randY = Random.Range(1f, 5f);
        speedx = randX;
        speedy = randY;
	}
	
	// Update is called once per frame
	void Update () {
        posY = transform.position.y;
        posX = transform.position.x; 
        if(posY <= -5)
        {
            goUp = true;
        }
        else if (posY >= 5)
        {
            goUp = false;
        }
        if(posX <= -10)
        {
            goLeft = false;
        }
        else if(posX >= 5)
        {
            goLeft = true;
        }
        if (goUp)
        {
            posY += (speedy * Time.deltaTime);

        } else
        {
            posY -= (speedy * Time.deltaTime);
            
        }
        if (goLeft)
        {
            posX -= (speedx * Time.deltaTime);
        }
        else
        {
            posX += (speedx * Time.deltaTime);
        }
        transform.position = new Vector2(posX, posY);
    }
}
