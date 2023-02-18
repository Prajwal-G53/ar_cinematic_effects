using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Cubes : MonoBehaviour
{
    public GameObject cube;
    public GameObject plane;
    private float x_Rand, y_Rand, z_Rand;
    private Vector3 rand_Pos;
    Bounds plane_bounds;
    // Start is called before the first frame update
    void Start()
    {
        plane_bounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        y_Rand = plane.transform.position.y + 2 * cube.transform.localScale.x;
        x_Rand = Random.Range(-10*plane_bounds.size.x / 2, 10*plane_bounds.size.x / 2);
        z_Rand = Random.Range(-10*plane_bounds.size.z / 2, 10*plane_bounds.size.z / 2);

        
        rand_Pos.Set(x_Rand, y_Rand, z_Rand);
        InvokeRepeating("Instantiate_Cube", 2.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Instantiate_Cube()
    {
        GameObject cube1 = Instantiate(cube,transform);

        //setting instantiated cubes position
        cube1.transform.position = (rand_Pos);

        if (cube1.transform.position.x == rand_Pos.x && cube1.transform.position.z == rand_Pos.z)
        {
            print("New position generated");

            x_Rand = Random.Range(-10*plane_bounds.size.x / 2, 10*plane_bounds.size.x / 2);
            z_Rand = Random.Range(-10*plane_bounds.size.z / 2, 10*plane_bounds.size.z / 2);

            rand_Pos.Set(x_Rand, y_Rand, z_Rand);

            float x_lim = 10 * plane_bounds.size.x,z_lim = 10 * plane_bounds.size.z;

            if(cube1.transform.position.x<0&&cube1.transform.position.z<0)
            {
                cube1.GetComponent<MeshRenderer>().material.color = Color.blue;
            }

            if (cube1.transform.position.x > 0&& cube1.transform.position.x <x_lim && cube1.transform.position.z < 0)
            {
                cube1.GetComponent<MeshRenderer>().material.color = Color.green;
            }

            if (cube1.transform.position.x > 0 && cube1.transform.position.x < x_lim && cube1.transform.position.z >0 && cube1.transform.position.z <z_lim)
            {
                cube1.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            if (cube1.transform.position.x < 0  && cube1.transform.position.z > 0 && cube1.transform.position.z < z_lim)
            {
                cube1.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
