using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {

    public void StartButtonPressed()
    {
        Application.LoadLevel("Starting_Level");
    }
}
