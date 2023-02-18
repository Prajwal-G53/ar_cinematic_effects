using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Circular : MonoBehaviour
{
    public GameObject center_go;
    public GameObject objInst;
    public float radius=10;
    public int noOfGos=10;

    private float angle;

    GameObject ModelParent;

    public static bool move;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        //inst_gos_spherical();
        inst_gos_spherical_consistant();

        //inst_gos_cylinder();
    }

    void inst_gos_spherical()
    {
        //****refer this link**************//
        //https://www.researchgate.net/post/Can-anyone-tell-me-how-to-find-coordinates-of-points-lying-on-the-surface-of-hemisphere
        for (int i = 0; i < noOfGos; i++)
        {
            angle = Random.Range(0, 360);
           float phi = Random.Range(0, 360);

            float rad = angle * Mathf.PI / 180;
            float rad_phi = phi * Mathf.PI / 180;

            float x_val = center_go.transform.position.x+ radius * Mathf.Cos(rad)*Mathf.Sin(rad_phi),//x+r*cos(thet)*sin(phi)
                y_val = center_go.transform.position.y+radius*Mathf.Cos(rad_phi),//y+r*cos(phi)
                z_val=center_go.transform.position.z+radius*Mathf.Sin(rad) * Mathf.Sin(rad_phi);//z+r*sin(thet)*sin(phi)

            GameObject go = Instantiate(objInst, new Vector3(x_val,y_val,z_val), Quaternion.identity);
            

        }


    }

    void inst_gos_spherical_consistant()
    {
        float phi;
        //****refer this link**************//
        //https://www.researchgate.net/post/Can-anyone-tell-me-how-to-find-coordinates-of-points-lying-on-the-surface-of-hemisphere
        //for (int i = 0; i < noOfGos; i++)
        //{
        //    angle = i*30;
        //     phi = i*30;

        //    float rad = angle * Mathf.PI / 180;
        //    float rad_phi = phi * Mathf.PI / 180;

        //    float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad) * Mathf.Sin(rad_phi),//x+r*cos(thet)*sin(phi)
        //        y_val = center_go.transform.position.y + radius * Mathf.Cos(rad_phi),//y+r*cos(phi)
        //        z_val = center_go.transform.position.z + radius * Mathf.Sin(rad) * Mathf.Sin(rad_phi);//z+r*sin(thet)*sin(phi)

        //    GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);


        //}

        //360/30=12

        //ModelParent = new GameObject("ModelParent");

        //ModelParent.transform.position = center_go.transform.position;

        int sector_angle = 30;

        int total_angle = 360;

        int inst_count_circumferance = total_angle / sector_angle;
        for (int i = 0; i < inst_count_circumferance; i++)//12
        {

            //360/30=12
        for (int j = 0; j < inst_count_circumferance; j++)//12
        {
            angle = j * 30;
            phi = i * 30;

                //to avoid duplicates at 180 deg
               if(phi==180 && j>0)
                {
                    continue;
                }

                    float rad = angle * Mathf.PI / 180;
            float rad_phi = phi * Mathf.PI / 180;

            float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad) * Mathf.Sin(rad_phi),//x+r*cos(thet)*sin(phi)
                y_val = center_go.transform.position.y + radius * Mathf.Cos(rad_phi),//y+r*cos(phi)
                z_val = center_go.transform.position.z + radius * Mathf.Sin(rad) * Mathf.Sin(rad_phi);//z+r*sin(thet)*sin(phi)

            GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);

                Vector3 dir = center_go.transform.position - go.transform.position;

                go.transform.forward = -dir;

                go.name = "Humanoid_" + angle + "_" + phi;

                go.transform.parent = center_go.transform;

                //to avoid duplicate gameobjects at 0 deg and 360 deg ,same positions (here at same angles)
                if (phi == 0 )
                {
                    break;
                }

                //if (angle == 180) continue;

                //if(i==j)
                //{
                //    break;
                //}

            }

        }
        move = true;
    }

    void inst_gos_circular()
    {

        for (int i = 0; i < noOfGos; i++)
        {
            angle = Random.Range(0, 360);

            float rad = angle * Mathf.PI / 180;

            float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad),
                y_val = center_go.transform.position.y,
                z_val = center_go.transform.position.z + radius * Mathf.Sin(rad);

            GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);


        }


    }
    //We have 360 deg in a circle here we instantiate game_object for every 30 deg, 360/30=12
    //similarly at each 30 deg angle we instantiate game_object for every 30 deg, 360/30=12 above
    void inst_gos_cylinder()
    {
        ModelParent = new GameObject("ModelParent");
        for (int j = 0; j < 10; j++)
        {
           
        for (int i = 0; i < 12; i++)
        {
            //angle = Random.Range(0, 360);

            angle = i * 30;

            float rad = angle * Mathf.PI / 180;

            float x_val = center_go.transform.position.x + radius * Mathf.Cos(rad),
                y_val = center_go.transform.position.y + radius * Mathf.Sin(rad),
                z_val = center_go.transform.position.z ;

            GameObject go = Instantiate(objInst, new Vector3(x_val, y_val, z_val), Quaternion.identity);

                go.transform.forward = center_go.transform.forward;
               MeshFilter mf= go.transform.GetChild(0).GetComponentInChildren<MeshFilter>();

                float max_len = j * mf.mesh.bounds.size.y;


                go.transform.Translate(center_go.transform.forward * max_len, center_go.transform);


                go.name = "Humanoid_" + j + "_" + i; go.name = "Humanoid_" + j + "_" + i;
                go.transform.parent = ModelParent.transform;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
