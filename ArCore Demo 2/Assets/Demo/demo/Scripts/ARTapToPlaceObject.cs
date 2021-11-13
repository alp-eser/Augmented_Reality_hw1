using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;
    
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject[] spawnedObject;

    public static event Action onPlacedObject;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager m_ARPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private int count;


    void Awake(){
        m_ARPlaneManager  = GetComponent<ARPlaneManager>();
        _arRaycastManager = GetComponent<ARRaycastManager>();
        count = 0;
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        plane.gameObject.SetActive(value);
    }
    
    void Update()
    {
        if (Input.touchCount > 0){

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){

                if(_arRaycastManager.Raycast(touch.position,hits,TrackableType.PlaneWithinPolygon) && count <3)
                {
                    Pose hitPose = hits[0].pose;

                    spawnedObject[count] = Instantiate(m_PlacedPrefab,hitPose.position,hitPose.rotation);
                    Debug.Log("=============================="+spawnedObject[count]);
                    count += 1;
                    Debug.Log("--------------------------Count :"+count);

                    if (onPlacedObject != null && count <= 3){
                       onPlacedObject();
                    }

                    if (count >= 3){
                        SetAllPlanesActive(false);    
                    }
                            
                }        
            }
        }        
    }
}
