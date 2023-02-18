using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectBehaviour : MonoBehaviour
{

    // base script for all the gameobject behaviour. Gameobject inherits and those scipts are attached to the gameobject itself

    Transform arCamera;
    TeleportEffect teleportEffect;

    //[HideInInspector]
    public bool objectMoving;

    Transform front_view_child;
    bool stopped_cour=false;
    // Start is called before the first frame update
    void Start()
    {

        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
       


        front_view_child = arCamera.Find("FrontViewGo");
        stopped_cour = false;
    }

    public virtual void OnlyMoveGameobject(float time = 3f)
    {
        StartCoroutine(OnlyMoveGameobjectRoutine(time));
    }
    public virtual IEnumerator OnlyMoveGameobjectRoutine(float lerpTimeFactor, float offset = 0.01f)//0.08f)
    {

        arCamera = Camera.main.transform;
        front_view_child = arCamera.Find("FrontViewGo");

        Follow follow_script = null;
        if (transform.parent != null)
            if (transform.parent.GetComponent<Follow>() != null)
            {

                follow_script = transform.parent.GetComponent<Follow>();
                //For Follow Script to work The scale of Game Objects should be 1
                if (InstantiationController_AR.ModelParent != null)
                {
                    InstantiationController_AR.ModelParent.transform.localScale = Vector3.one;
                    if (InstantiationController_AR.ModelParent.transform.childCount > 0)
                        InstantiationController_AR.ModelParent.transform.GetChild(0).localScale = Vector3.one;
                }
            }

        //front_view_child = arCamera;
        if (front_view_child != null)
            while ((transform.position - front_view_child.position).magnitude > offset)
            {
                objectMoving = true;
                InputController_AR.pinchScale_enable = false;
                //Debug.Log((transform.position - arCamera.position).magnitude);
                transform.position = Vector3.Lerp(transform.position, front_view_child.position, Time.deltaTime * lerpTimeFactor);
                //transform.rotation = Quaternion.Slerp(transform.rotation, front_view_child.rotation, Time.deltaTime * lerpTimeFactor);

                //PlantCellBehaviour_2 plantCellBehaviour_2_script = gameObject.GetComponent<PlantCellBehaviour_2>();

                bool plantcell_beh_2_ = false;
                //if (plantCellBehaviour_2_script != null)
                //{
                //    if (plantCellBehaviour_2_script.up_rot)
                //    {
                //        plantCellBehaviour_2_script.up_rot = false;
                //        plantcell_beh_2_ = true;
                //    }
                //}


                //PlantCellBehaviour plantCellBehaviour_script = gameObject.GetComponent<PlantCellBehaviour>();

                //bool plantcell_beh_ = false;
                //if (plantCellBehaviour_script != null)
                //{
                //    if (plantCellBehaviour_script.up_rot)
                //    {
                //        plantCellBehaviour_script.up_rot = false;
                //        plantcell_beh_ = true;
                //    }
                //}
                //if (follow_script!=null)
                //{

                //    objectMoving = true;
                //    InputController_AR.pinchScale_enable = false;
                //    transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, Vector3.up, Time.deltaTime * lerpTimeFactor);
                //    //yield return null;

                //}
                //else
                //{
                    transform.rotation = Quaternion.Slerp(transform.rotation, front_view_child.rotation, Time.deltaTime * lerpTimeFactor);

                //}
                yield return null;
            }

        InputController_AR.pinchScale_enable = true;
        objectMoving = false;
        //StopAllCoroutines();
        //StopCoroutine(OnlyMoveGameobjectRoutine(3));
    }

    // Update is called once per frame
    void Update()
    {
        if(arCamera.transform.hasChanged&&objectMoving==false)
        {
            arCamera.transform.hasChanged = false;
            stopped_cour = false;
            OnlyMoveGameobject();
        }

        if(!arCamera.transform.hasChanged&&objectMoving==false&&stopped_cour==false)
        {
            stopped_cour = true;
            StopCoroutine(OnlyMoveGameobjectRoutine(3));
        }
    }
}
