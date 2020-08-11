using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public static bool singlePlayerMode;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerMode(bool selection) {
        singlePlayerMode = selection;
    }
}
