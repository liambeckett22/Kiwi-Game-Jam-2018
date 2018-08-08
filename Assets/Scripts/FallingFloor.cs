using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {

    [SerializeField] private float fallDelay;
    [SerializeField] private bool fallOnceOff;
    [SerializeField] private bool neverFall;

    private bool steptOn = false;
    private bool steptOff = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Avatar")
        {
            steptOn = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Avatar")
        {
            steptOff = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((steptOn && steptOff || steptOn && !fallOnceOff) && !neverFall)
        {
            StartCoroutine(Fall());
            steptOn = false;
            steptOff = false;
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().useGravity = true;
    }
}
