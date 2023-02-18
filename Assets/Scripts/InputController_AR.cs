using UnityEngine;
using Lean.Touch;
using Doozy.Engine.UI;



public class InputController_AR : MonoBehaviour
{
    bool EnableManipulation = true;
    public Camera _arCamera;
    public InstantiationController_AR instantiationController;
    
    public static bool pinchScale_enable=true;

    private void Start()
    {
        pinchScale_enable = true;
    }

    void Update()
    {

        if (!instantiationController.objectInstantiated) return;
        if (pinchScale_enable == true)
        {
            PinchToScaleModel();
        }

    }



    public void SwipeToRotateModel(LeanFinger finger)
    {


        if (finger.IsOverGui || !instantiationController.objectInstantiated) return;
        
        InstantiationController_AR.ModelParent.transform.GetChild(0).Rotate(Vector3.up, LeanGesture.GetScaledDelta().x * -0.1f);

    }


    public void TapToSelectPart(LeanFinger finger)
    {
        if (!EnableManipulation) return;
        Vector2 screenPos = finger.StartScreenPosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
    }



    public void PinchToScaleModel()
    {
        if (!EnableManipulation || !instantiationController.objectInstantiated) return;
        foreach (LeanFinger finger in LeanTouch.Fingers)
        {
            if (finger.IsOverGui) return;
        }
        float newScale = LeanGesture.GetPinchScale() * InstantiationController_AR.ModelParent.transform.GetChild(0).localScale.x; //Assuming all three axis scale factors are equal
        InstantiationController_AR.ModelParent.transform.GetChild(0).localScale = Mathf.Clamp(newScale, 0.2f, 5f) * Vector3.one;
        //InstantiationController_AR.ModelParent.transform.GetChild(0).localScale = Vector3.one;
        Debug.Log("Scale :" + InstantiationController_AR.ModelParent.transform.GetChild(0).localScale);

    }

    public void ToggleManipulationGestures(bool enable)
    {
        EnableManipulation = enable;
    }


}
