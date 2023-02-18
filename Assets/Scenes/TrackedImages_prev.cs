using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent (typeof (ARTrackedImageManager))]
public class TrackedImages_prev : MonoBehaviour
{

    [SerializeField] GameObject[] arObjectsToPlace;

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach (GameObject arObject in arObjectsToPlace)
        {

            Debug.Log("arObjects  :  " + arObject.name);

            GameObject newArObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newArObject.name = arObject.name;
            arObjects.Add(arObject.name, newArObject);
            newArObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        Debug.Log("trackedImage  :  ");

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateARImage(trackedImage);
            }
            else
            {
                //arObjects[trackedImage.referenceImage.name].SetActive(false);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.referenceImage.name].SetActive(false);
        }
    }


    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Assign and place Gameobject
        AssignGameObject(trackedImage);
    }


    public void AssignGameObject(ARTrackedImage rTrackedImage)
    {
        Debug.Log("AssignedGameObject  :  " + rTrackedImage.referenceImage.name);

        GameObject prefab = arObjects[rTrackedImage.referenceImage.name];
        prefab.transform.position = rTrackedImage.transform.position;
        prefab.transform.rotation = rTrackedImage.transform.rotation;
        prefab.SetActive(true);

        foreach (GameObject go in arObjects.Values)
        {
            if (go.name != rTrackedImage.referenceImage.name)
            {
                //go.SetActive(false);
            }
        }
    }

}
