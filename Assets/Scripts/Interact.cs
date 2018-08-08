using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{

    [SerializeField] private Text InteractableText;

    private bool inRange;
    private bool usedUp;

    public UnityEvent InteractionMade;

    // Use this for initialization
    void Start()
    {
        InteractableText.enabled = false;
        inRange = false;
        usedUp = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InteractableText.enabled = false;
                usedUp = true;
                
                // TRIGGER EVENT
                InteractionMade.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Avatar" && !usedUp)
        {
            InteractableText.enabled = true;
            inRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Avatar")
        {
            InteractableText.enabled = false;
            inRange = false;
        }
    }

}
