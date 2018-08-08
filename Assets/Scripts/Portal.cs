using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] private string nextLevel;

    private bool portalOpen;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        portalOpen = false;
    }

    public void OpenPortal()
    {
        gameObject.SetActive(true);
        portalOpen = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Avatar" && portalOpen)
        {
            // Go to next Level
            Application.LoadLevel(nextLevel);
        }
    }

}
