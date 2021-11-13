 using UnityEngine;
 using System.Collections;
 
 public class test : MonoBehaviour {
     
     public GameObject emptyGameObjectPrefab;
     
     void Start () {
     Instantiate(emptyGameObjectPrefab, transform.position,Quaternion.identity);
         
     }
     
 }