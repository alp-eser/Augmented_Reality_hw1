using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;



public class interactWithBox : MonoBehaviour
{
    
    [SerializeField]
    GameObject m_tapOnDifferentBoxText;

    public GameObject tapOnDifferentBoxText
    {
        get { return m_tapOnDifferentBoxText; }
        set { m_tapOnDifferentBoxText = value; }
    }

    private GameObject tapOnDifferentBoxText_spawned;

    ///////////////////////////////////////

    [SerializeField]
    GameObject m_showMeTheQueen;

    public GameObject showMeTheQueen
    {
        get { return m_showMeTheQueen; }
        set { m_showMeTheQueen = value; }
    }

    private GameObject showMeTheQueen_spawned;

    ////////////////////////////////////////

    [SerializeField]
    GameObject m_showKing;

    public GameObject showKing
    {
        get { return m_showKing; }
        set { m_showKing = value; }
    }

    private GameObject showKing_spawned;

    ////////////////////////////////////////////



    private GameObject[] placedObjects;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.white;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    private ARSessionOrigin m_ArSessionOrigin;
    private ARTapToPlaceObject referenceSpawned;
    private GameObject[] listOfGameObjects;

    private bool flagFirstTime = true;

    private int i = 0;

    void Awake()
    {
        Debug.Log("AWAAKE ......");
        m_ArSessionOrigin = GetComponent<ARSessionOrigin>();        
        referenceSpawned  =  GetComponent<ARTapToPlaceObject>();

        tapOnDifferentBoxText_spawned =  (GameObject)Instantiate(m_tapOnDifferentBoxText, new Vector3(0.5f,0.5f,0.5f),Quaternion.identity);
        tapOnDifferentBoxText_spawned.SetActive(false);

        showMeTheQueen_spawned =  (GameObject)Instantiate(m_showMeTheQueen, new Vector3(0.5f,0.5f,0.5f),Quaternion.identity);
        showMeTheQueen_spawned.SetActive(false);

        showKing_spawned =  (GameObject)Instantiate(m_showKing, new Vector3(0.5f,0.5f,0.5f),Quaternion.identity);
        showKing_spawned.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (referenceSpawned.spawnedObject[0] != null && referenceSpawned.spawnedObject[1] != null && referenceSpawned.spawnedObject[2] != null)
        {
           
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                touchPosition = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;

                    if (Physics.Raycast(ray,out hitObject))
                    {
                        //GameObject placementObject = hitObject.transform.GetComponent<GameObject>();
                       
                        if (hitObject.collider.gameObject == referenceSpawned.spawnedObject[0]){
                            changeSelectedObject(referenceSpawned.spawnedObject[0]);

                            Vector3 newPosition = new Vector3(referenceSpawned.spawnedObject[0].transform.position.x,referenceSpawned.spawnedObject[0].transform.position.y,referenceSpawned.spawnedObject[0].transform.position.z + 0.5f);
                            showKing_spawned.transform.position = newPosition;
                            showKing_spawned.transform.rotation = referenceSpawned.spawnedObject[0].transform.rotation;
                            showKing_spawned.SetActive(true);

                            showMeTheQueen_spawned.SetActive(false);
                            tapOnDifferentBoxText_spawned.SetActive(false);  
                        }
                        else if (hitObject.collider.gameObject == referenceSpawned.spawnedObject[1]){
                            changeSelectedObject(referenceSpawned.spawnedObject[1]);    

                            Vector3 newPosition = new Vector3(referenceSpawned.spawnedObject[1].transform.position.x,referenceSpawned.spawnedObject[1].transform.position.y,referenceSpawned.spawnedObject[1].transform.position.z + 0.5f);
                            showMeTheQueen_spawned.transform.position = newPosition;
                            showMeTheQueen_spawned.transform.rotation = referenceSpawned.spawnedObject[1].transform.rotation;
                            showMeTheQueen_spawned.SetActive(true); 


                            showKing_spawned.SetActive(false);
                            tapOnDifferentBoxText_spawned.SetActive(false);

                        }
                        else if (hitObject.collider.gameObject == referenceSpawned.spawnedObject[2]){

                            if (flagFirstTime){
                                flagFirstTime = false;
                            }
                            else
                            {
                                changeSelectedObject(referenceSpawned.spawnedObject[2]);
                                
                                Vector3 newPosition = new Vector3(referenceSpawned.spawnedObject[2].transform.position.x,referenceSpawned.spawnedObject[2].transform.position.y,referenceSpawned.spawnedObject[2].transform.position.z + 0.5f);
                                tapOnDifferentBoxText_spawned.transform.position = newPosition;
                                tapOnDifferentBoxText_spawned.transform.rotation = referenceSpawned.spawnedObject[2].transform.rotation;
                                tapOnDifferentBoxText_spawned.SetActive(true);


                                showMeTheQueen_spawned.SetActive(false);
                                showKing_spawned.SetActive(false); 
                            }
                               
                        } 
                        
                        else
                        {
                            Debug.Log("YOU TOUCHED NULL");
                        }
                    }
                }
            }    
        }
            
    }


    void changeSelectedObject(GameObject selected){

        foreach(GameObject current in referenceSpawned.spawnedObject)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (selected != current)
            {
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                meshRenderer.material.color = activeColor;
            }
        }
    }


}
