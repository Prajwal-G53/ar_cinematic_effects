
using Doozy.Engine;
using Lean.Touch;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InstantiationController_AR : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject arEnvironment;

    private GameObject prefabClone;


    public static GameObject ModelParent;

    [HideInInspector]
    public bool objectInstantiated = false;



    public Camera m_ARCamera;
    public Light SceneDirectionalLight;
    public ARRaycastManager m_RaycastManager;
    public ARAnchorManager m_AnchorManager;
    public ARSession m_ARSession;
    public ARPlaneManager m_PlaneManager;
    ARAnchor spawnedModelAnchor;
    public bool isAREnabled;
    public GameObject PlaceAnimation;

    Pose poseForMultipleObjects;
    int instantiationIndex = -1;

    public List<GameObject> NGSPrefabs = new List<GameObject>();





    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Message.AddListener<GameEventMessage>(OnMessage);
        }
    }

    private void OnDisable()
    {
        if (!Application.isEditor)
        {

            Message.RemoveListener<GameEventMessage>(OnMessage);
        }
    }

    private void OnMessage(GameEventMessage message)
    {
        Debug.Log("Entered OnMessage");

        if (message.EventName.Equals("InitializeScene"))
        {
            Debug.Log("Entered OnMessage1");
            InitializeScene();

            if (isAREnabled) AskUserToPlace();
        }

        else if (message.EventName.Equals("InitializeSceneWithoutModel"))
        {
            Debug.Log("Entered OnMessage");
            InitializeScene();
        }

        else if (message.EventName.Equals("DeInitializeScene"))
        {
            DestroySpawnedObject();
            DeInitializeScene();
        }
    }

    private void Start()
    {
        inst_1st = false;

        //PlaceAnimation.SetActive(true);
        //ToggleARPlanes(true);
        if (!Application.isEditor)
        {
            AskUserToPlace();
        }
    }




    public void InitializeScene()
    {
        Debug.Log("init called");
        m_ARSession.enabled = isAREnabled;
        m_ARCamera.gameObject.SetActive(isAREnabled);
        SceneDirectionalLight.gameObject.SetActive(isAREnabled);
    }

    public void DeInitializeScene()
    {
        m_ARSession.enabled = false;
        m_ARCamera.gameObject.SetActive(false);
        SceneDirectionalLight.gameObject.SetActive(false);
        PlaceAnimation.SetActive(false);
    }

    private void ToggleARPlanes(bool enable)
    {
        m_PlaneManager.enabled = enable;
        foreach (ARPlane plane in m_PlaneManager.trackables)
        {
            plane.gameObject.SetActive(enable);
        }
    }
    bool inst_1st = false;
    private void Update()
    {
        if(!(inst_1st==false&&Application.isEditor))
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,Mathf.Infinity))
            {
                    if (hit.collider.tag.Contains("plane"))
                    {
                        Transform hit_GO = hit.collider.transform;
                        Pose p = new Pose();
                        p.position = hit_GO.position;
                        p.rotation = hit_GO.rotation;

                        InstantiateModel(p, false);
                        inst_1st = true;
                    }
            }
        }
    }
    private void ARInstantiationRoutine(Vector2 screenPos)
    {
        if (m_PlaneManager.enabled == false) return;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        TrackableType raycastFilter = TrackableType.PlaneWithinPolygon;
        if (m_RaycastManager.Raycast(screenPos, hits, raycastFilter))
        {
            Pose p = hits[0].pose;
            InstantiateModel(p);
        }
    }

    

    private void InstantiateModel(Pose pose, bool spawnWithARAnchor = true)
    {
        poseForMultipleObjects.position = pose.position;
        poseForMultipleObjects.rotation = pose.rotation;

        var prefabInScene = GameObject.Find(objectPrefab.name);
        if (!Application.isEditor)
        {

            PlaceAnimation.SetActive(false);
            ToggleARPlanes(false);
        }

        if (prefabInScene == null)
        {
            ModelParent = new GameObject("ModelParent");
            ModelParent.transform.SetPositionAndRotation(pose.position, pose.rotation);
            prefabClone = Instantiate(objectPrefab, pose.position, pose.rotation);
            prefabClone.name = objectPrefab.name;
            prefabClone.transform.parent = ModelParent.transform;
            prefabClone.transform.Rotate(0, -90, 0, Space.Self);


            objectInstantiated = true;

            
            //Set AR Anchor
            if (spawnWithARAnchor)
            {
                spawnedModelAnchor = m_AnchorManager.AddAnchor(pose);
                ModelParent.transform.parent = spawnedModelAnchor.transform;

            }

            //FindObjectOfType<StepController_AR>().CheckTriggerForNextStep();


        }
    }

    public GameObject InstantiateModel(float newHeight = 0f)
    {


        Destroy(ModelParent);
        ModelParent = null;
        ModelParent = new GameObject("ModelParent");

        instantiationIndex++;
        ModelParent.transform.SetPositionAndRotation(poseForMultipleObjects.position, poseForMultipleObjects.rotation);

        prefabClone = Instantiate(NGSPrefabs[instantiationIndex], poseForMultipleObjects.position, poseForMultipleObjects.rotation);
        ModelParent.transform.position = prefabClone.transform.position;
        
        //Transform actualPose = prefabClone.transform.Find("")

        prefabClone.transform.parent = ModelParent.transform;

        //prefabClone.transform.Rotate(0, 90, 0, Space.Self);
        //Vector3 rotation = (Quaternion.LookRotation(Camera.main.transform.position, Vector3.up)).eulerAngles;
        Vector3 rotation = (Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up)).eulerAngles;
        prefabClone.transform.Rotate(new Vector3(prefabClone.transform.rotation.x, rotation.y, prefabClone.transform.rotation.z));
        // prefabClone.transform.rotation =  Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);

        prefabClone.transform.position = new Vector3(prefabClone.transform.position.x, newHeight, prefabClone.transform.position.z);
        return prefabClone;
    }

    public GameObject InstantiateModelPreviosFunc(float newHeight = 0f)
    {


        Destroy(ModelParent);
        ModelParent = null;
        ModelParent = new GameObject("ModelParent");

        instantiationIndex++;
        ModelParent.transform.SetPositionAndRotation(poseForMultipleObjects.position, poseForMultipleObjects.rotation);

        prefabClone = Instantiate(NGSPrefabs[instantiationIndex], poseForMultipleObjects.position, poseForMultipleObjects.rotation);

        //Transform actualPose = prefabClone.transform.Find("")

        prefabClone.transform.parent = ModelParent.transform;
        //prefabClone.transform.Rotate(0, 90, 0, Space.Self);
        //Vector3 rotation = (Quaternion.LookRotation(Camera.main.transform.position, Vector3.up)).eulerAngles;
        Vector3 rotation = (Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up)).eulerAngles;
        prefabClone.transform.Rotate(new Vector3(prefabClone.transform.rotation.x, rotation.y, prefabClone.transform.rotation.z));
        // prefabClone.transform.rotation =  Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);

        prefabClone.transform.position = new Vector3(prefabClone.transform.position.x, newHeight, prefabClone.transform.position.z);
        return prefabClone;
    }

    public static Bounds Calculate_b(Transform TheObject)//To calculate bound of all the gos 
    {
        var renderers = TheObject.GetComponentsInChildren<Renderer>();
        Bounds combinedBounds = renderers[0].bounds;
        for (int i = 0; i < renderers.Length; i++)
        {
            combinedBounds.Encapsulate(renderers[i].bounds);
        }
        return combinedBounds;
    }
    public void InstantiateModel_particular_pos(Vector3 newPos)
    {


        Destroy(ModelParent);
        ModelParent = null;
        ModelParent = new GameObject("ModelParent");

        instantiationIndex++;
        ModelParent.transform.SetPositionAndRotation(poseForMultipleObjects.position, poseForMultipleObjects.rotation);

        prefabClone = Instantiate(NGSPrefabs[instantiationIndex], poseForMultipleObjects.position, poseForMultipleObjects.rotation);

        //Transform actualPose = prefabClone.transform.Find("")

        prefabClone.transform.parent = ModelParent.transform;
        //prefabClone.transform.Rotate(0, 90, 0, Space.Self);
        //Vector3 rotation = (Quaternion.LookRotation(Camera.main.transform.position, Vector3.up)).eulerAngles;
        Vector3 rotation = (Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up)).eulerAngles;
        prefabClone.transform.Rotate(new Vector3(prefabClone.transform.rotation.x, rotation.y, prefabClone.transform.rotation.z));
        // prefabClone.transform.rotation =  Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);

        //prefabClone.transform.position = new Vector3(prefabClone.transform.position.x, newHeight, prefabClone.transform.position.z);

        prefabClone.transform.position = newPos;
    }


    public void InstantiateAREnvironment()
    {
        //Sphere black
        //if (Application.isEditor)
        //    return;

        GameObject ArEnviromentParent = new GameObject("ARenviParent");

        ArEnviromentParent.transform.SetPositionAndRotation(poseForMultipleObjects.position, poseForMultipleObjects.rotation);
        Vector3 arEnviPos = new Vector3(poseForMultipleObjects.position.x, poseForMultipleObjects.position.y - 2f, poseForMultipleObjects.position.z);
        GameObject arEnvironmentClone = Instantiate(arEnvironment, arEnviPos, poseForMultipleObjects.rotation);
        arEnvironmentClone.transform.parent = ArEnviromentParent.transform;
        if(spawnedModelAnchor!=null)
        ArEnviromentParent.transform.parent = spawnedModelAnchor.transform;

    }



    public void TapToPlace(LeanFinger finger)
    {
        if (!Application.isEditor)
        {
            if (isAREnabled) ARInstantiationRoutine(finger.StartScreenPosition);
        }
    }

    public void AskUserToPlace()
    {
        PlaceAnimation.SetActive(true);
        ToggleARPlanes(true);

    }

    public void DestroySpawnedObject()
    {
        if (ModelParent)
        {
            Destroy(ModelParent);
            ModelParent = null;
            objectInstantiated = false;

            if (isAREnabled && spawnedModelAnchor) m_AnchorManager.RemoveAnchor(spawnedModelAnchor);
        }
    }

    public void ToggleARMode(bool enable)
    {
        DestroySpawnedObject();
        isAREnabled = enable;
    }


}
