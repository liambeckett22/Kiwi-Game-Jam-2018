using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<Animation>().Play("Cube|CubeAction");
        }
    }

    public void PickedUpSword()
    {
        gameObject.SetActive(true);
        GetComponent<Animation>().Play("Cube|CubeAction.001");
    }

    /*void OnTriggerEnter(Collider collider)
    {
        Debug.Log("HERE");
        if (collider.gameObject.name == "Jar")
        {
            BreakSword();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("HERE");
        if (collider.gameObject.name == "Jar")
        {
            BreakSword();
        }
    }*/

    private void BreakSword()
    {
        GetComponent<Animation>().Play("Cube|CubeAction.002");
        gameObject.SetActive(false);
    }
}
