using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class InventoryItem : MonoBehaviour   
{
    [SerializeField] private int spinX;
    [SerializeField] private int spinY;
    [SerializeField] private int spinZ;
    
    [SerializeField] private bool itemShownOnStart;

    [SerializeField] private string inventoryKey = "";
    [SerializeField] private Vector3 lockPosition;
    [SerializeField] private Text pickUpText;

    private bool inRange;

    public UnityEvent ItemPickedUp;

    private bool shouldBeSpinning;
    private bool itemUsed;
    private Quaternion originalRotationValue;

    // Use this for initialization
    void Start()
    {
        originalRotationValue = transform.rotation;
        pickUpText.enabled = false;
        gameObject.SetActive(itemShownOnStart);
        shouldBeSpinning = true;
        inRange = false;
        itemUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldBeSpinning)
        {
            transform.Rotate(spinX, spinY, spinZ);
        }

        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickUpText.enabled = false;

                // TRIGGER EVENT
                ItemPickedUp.Invoke();

                gameObject.SetActive(false);
                shouldBeSpinning = false;
            }
        }
    }

    public void ItemUsed()
    {
        // When the key is used the key should move to the lock and stop spinning.
        if (inventoryKey == "Key")
        {
            gameObject.SetActive(true);
            gameObject.transform.position = lockPosition;
            gameObject.transform.rotation = originalRotationValue;
            transform.Rotate(0, 0, 90);
            itemUsed = true;
        }
    }

    public void ShowItem()
    {
        gameObject.SetActive(true);
    }
    

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Avatar" && !itemUsed)
        {
            pickUpText.enabled = true;
            inRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Avatar")
        {
            pickUpText.enabled = false;
            inRange = false;
        }
    }
    
}

