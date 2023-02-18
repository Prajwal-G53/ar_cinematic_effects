using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent (typeof (ARTrackedImageManager))]
public class TrackedImages : MonoBehaviour
{
    public bool isZoomEnabled;

    [SerializeField] GameObject arObjectToPlace;

    private ARTrackedImageManager m_TrackedImageManager;
    private ARAnchor anchor;

    private GameObject objectToInteract;

    private float initialDistance;
    private Vector3 initialScale;
    private Transform initialTransform;

    private bool isTimeDelyaDone = false;

    public Vector2 image_size;

    public static bool added_track = false;

    public TMPro.TextMeshProUGUI text_message;
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
       
        GameObject newArObject = Instantiate(arObjectToPlace, Vector3.zero, Quaternion.identity);
        newArObject.name = arObjectToPlace.name;
        newArObject.SetActive(false);
        objectToInteract = newArObject;
    }

    private void Update()
    {
        #region old
        if (Input.touchCount == 2 && isZoomEnabled)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return; // basically do nothing
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = arObjectToPlace.transform.localScale;
                Debug.Log("Initial Disatance: " + initialDistance + "GameObject Name: " + arObjectToPlace.name); // Just to check in console
            }
            else // if touch is moved
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                //if accidentally touched or pinch movement is very very small
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; // do nothing if it can be ignored where inital distance is very close to zero
                }

                var factor = currentDistance / initialDistance;
                arObjectToPlace.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated
            }
        }
        #endregion
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
            added_track = true;
            text_message.text = "tracked";
            //StartCoroutine(TimeDelay());
            //UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //UpdateARImage(trackedImage);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            objectToInteract.SetActive(false);
            isTimeDelyaDone = false;
        }
    }


    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Assign and place Gameobject
        AssignGameObject(trackedImage);
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(7f);
        isTimeDelyaDone = true;
    }

    public static Pose pose_img;
    public void AssignGameObject(ARTrackedImage rTrackedImage)
    {

        GameObject prefab = objectToInteract;

        image_size = rTrackedImage.size;

        pose_img.position = rTrackedImage.transform.position;
        pose_img.rotation = rTrackedImage.transform.rotation;
        //First 7seconds this condition will be true
        if(!isTimeDelyaDone)
        {
            prefab.transform.SetPositionAndRotation(rTrackedImage.transform.position, rTrackedImage.transform.rotation);
            initialTransform = prefab.transform;
        }

        //After first 7seconds if distance btwn current and new position is more than 2. It will not change
        if (Vector3.Distance(prefab.transform.position, rTrackedImage.transform.position) <= Mathf.Min(rTrackedImage.size.x,rTrackedImage.size.y) && isTimeDelyaDone)
        {
            prefab.transform.SetPositionAndRotation(rTrackedImage.transform.position, rTrackedImage.transform.rotation);
        }
        else if(isTimeDelyaDone)
        {
            prefab.transform.SetPositionAndRotation(initialTransform.position, initialTransform.rotation);
        }


        prefab.SetActive(true);

        if (anchor == null)
        {
            prefab.AddComponent<ARAnchor>();
        }

    }

}
