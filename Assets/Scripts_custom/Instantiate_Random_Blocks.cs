using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Instantiate_Random_Blocks : MonoBehaviour
{
    public NavMeshAgent cube;
    public GameObject plane;
    private float x_Rand, y_Rand,z_Rand;
    private Vector3 rand_Pos;
    // Start is called before the first frame update
    void Start()
    {
        x_Rand = Random.Range(-plane.transform.localScale.x / 2, plane.transform.localScale.x / 2);
        z_Rand = Random.Range(-plane.transform.localScale.z / 2, plane.transform.localScale.z / 2);
        rand_Pos.Set(x_Rand,plane.transform.position.y, z_Rand);
    }

    // Update is called once per frame
    void Update()
    {
        cube.SetDestination(rand_Pos);

        if(cube.transform.position.x==rand_Pos.x&&cube.transform.position.z==rand_Pos.z)
        {
            print("New position generated");

            x_Rand = Random.Range(-plane.transform.localScale.x / 2, plane.transform.localScale.x / 2);
            z_Rand = Random.Range(-plane.transform.localScale.z / 2, plane.transform.localScale.z / 2);

            rand_Pos.Set(x_Rand, plane.transform.position.y, z_Rand);
        }
    }
}
