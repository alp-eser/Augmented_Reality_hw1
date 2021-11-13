using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class imageRecognitionExample : MonoBehaviour
{
    private ARTrackedImageManager _arTracjedImageManager;

    private void Awake(){

        _arTracjedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable(){
        _arTracjedImageManager.trackedImagesChanged += OnImageChanged;

    }

    public void OnDisable(){
        _arTracjedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args){
        foreach(var trackedImage in args.added){
            Debug.Log(trackedImage.name);
        }
    }

}
