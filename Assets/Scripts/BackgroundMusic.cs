using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;
     public static BackgroundMusic Instance {
         get { return instance; }
     }
     void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
     }
}
