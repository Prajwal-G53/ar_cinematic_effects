using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLocalValue : MonoBehaviour
{
    //public Quaternion localRotation;
    public Vector3 localEulerAngles;
    //public Vector3 newlocalAngles;
    //public Vector3 init_Angles;
    //public float diff_ang;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    diff_ang = -180;
    //    init_Angles = localEulerAngles;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    localRotation = transform.localRotation;
    //    localEulerAngles = transform.localEulerAngles;
    //    newlocalAngles.Set(WrapAngle(localEulerAngles.x), WrapAngle(localEulerAngles.y), WrapAngle(localEulerAngles.z));
    //    prev_x = localEulerAngles.x;
    //}
    //float x_val,prev_x;
    //private Vector3 angles_mod(Vector3 angles)
    //{
    //    //angles.Set(init_Angles)
    //    return angles;
    //}

    //private  float WrapAngle(float angle)
    //{
    //    //angle %= 360;
    //    //if (angle > 180)
    //    //    return angle - 360;

    //    //return angle;
    //    //angle = (angle > 180) ? angle - 360 : angle;
    //    //rotateObject.transform.localEulerAngles = new Vector3(rotateObject.transform.localEulerAngles.x, angle, rotateObject.transform.localEulerAngles.z);
    //    //if (prev_x>angle)
    //    //{
    //    //    angle--;
    //    //}

    //    angle = angle - 360;// init_Angles.x;
    //    return angle;
    //}

    [SerializeField]
    private float angle = 10;

    //float max_height = 400;

    [SerializeField]
    private float max_angle = 180;
    [SerializeField]
    private float min_angle = -180;

    public float new_x;
    // Start is called before the first frame update
    void Start()
    {
        new_x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(Mathf.Clamp(map_height_to_angle(angle), 0, min_angle), -90, -90);
        localEulerAngles = transform.localEulerAngles;
        new_x = map_height_to_angle(transform.localEulerAngles.x);

        //transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, Vector3.up, Time.deltaTime * 2f);
    }

    float map_height_to_angle(float hgt)
    {
        float ang = 0;

        //ang = (hgt / max_angle) * 100;
        //ang=  (hgt +180) % 360-180 ;

        //if(hgt<180)
        //{
        //    ang = hgt;
        //}
        //else
        //{
        //    ang = hgt - 360;
        //}

        //if(hgt<90)
        //{
        //    ang = hgt;
        //}
        //else if(hgt>=90&&hgt<180)
        //{
        //    ang = hgt;
        //}
        //else if(hgt>=180&&hgt<270)
        //{
        //    ang = hgt - 360;
        //}
        if (hgt < 180)
        {
            ang = hgt;
        }
      
        else if (hgt >= 180 && hgt <=360)
        {
            ang = hgt - 360;
        }

        return ang;
    }
}
