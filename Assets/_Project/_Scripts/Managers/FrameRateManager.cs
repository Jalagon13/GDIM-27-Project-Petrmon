 using UnityEngine;
 using System.Collections;
 
 public class FrameRateManager : MonoBehaviour {
 
     public int frameRate = 165;
     private bool updated = false;
 
     void Update() {
         StartCoroutine(changeFramerate());
     }

     IEnumerator changeFramerate() {
         yield return new WaitForSeconds(1);
         if (!updated){
            Application.targetFrameRate = frameRate;
            updated = true;
         }
         
     }
     
 }