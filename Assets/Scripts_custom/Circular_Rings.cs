using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circular_Rings : MonoBehaviour
{

    public GameObject center_go;
    public GameObject objInst;
    public float radius = 10;
    public int noOfGos = 10;
    public float delta_angle = 10;
    private float angle=0,phi=0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("inst_gos_spherical", 2f, 1f);
        bool sphere = true;
        if(sphere==true)
        {
            noOfGos = (int)(360 / delta_angle);

            noOfGos = noOfGos * noOfGos;
        }
        else
        {
            //for sphere
            noOfGos = 6*3;//3 rings
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount>= noOfGos)
        {
            CancelInvoke("inst_gos_spherical");
        }
    }

    void inst_gos_circular()
    {

        //for (int i = 0; i < noOfGos; i++)
        //{
        //angle = Random.Range(0, 360);

        //angle = angle % 360;
       

            float rad = angle * Mathf.PI / 180;

            float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad),
                y_val = center_go.transform.position.y + radius * Mathf.Sin(rad),
                z_val = center_go.transform.position.z;

            GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);

        go.transform.parent = transform;
        //}

        angle = angle + 30;

        if (angle%360==0)
        {
            radius += 10;
        }
    }

    void inst_gos_spherical()
    {
        //****refer this link**************//
        //https://www.researchgate.net/post/Can-anyone-tell-me-how-to-find-coordinates-of-points-lying-on-the-surface-of-hemisphere
        for (int i = 0; i < noOfGos; i++)
        {
            //angle = Random.Range(0, 360);
            // phi = Random.Range(0, 360);

            float rad = angle * Mathf.PI / 180;
            float rad_phi = phi * Mathf.PI / 180;

            float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad) * Mathf.Sin(rad_phi),//x+r*cos(thet)*sin(phi)
                y_val = center_go.transform.position.y + radius * Mathf.Cos(rad_phi),//y+r*cos(phi)
                z_val = center_go.transform.position.z + radius * Mathf.Sin(rad) * Mathf.Sin(rad_phi);//z+r*sin(thet)*sin(phi)

            GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);

            go.transform.parent = transform;
            angle = angle + delta_angle;

            //if (angle % 360 == 0)
            //{
            //    radius += 10;
            //}

            if (angle % 360 == 0)
            {
            phi = phi + delta_angle;
                angle = 0;
                //radius += 10;
            }



            //if (phi % 360 == 0)
            //{
            //    radius+= 10;
            //}
        }


    }


}
