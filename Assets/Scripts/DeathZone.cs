using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    Transform playerTransform;

    void Awake() {
        playerTransform = GameObject.Find("Player").transform;
    }

   void Update() {
         transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
   }

   void OnTriggerExit2D(Collider2D other)
   {
       if(other.CompareTag("Player")) {
           playerTransform.gameObject.GetComponent<PlayerHealth>().GameOver();
       }
   }
}
