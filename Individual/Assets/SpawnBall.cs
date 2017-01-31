using UnityEngine;
using System.Collections;

public class SpawnBall : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public GameObject prefab;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject obj = Instantiate(prefab, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z), Quaternion.identity) as GameObject;
            obj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

    }
}
