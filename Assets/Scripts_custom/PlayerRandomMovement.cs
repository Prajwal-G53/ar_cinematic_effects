using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRandomMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    //public GameObject dest;
    private Vector3 randPos;
    private float xRand, yRand, zRand;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        xRand = Random.Range(-50, 50);
        yRand = transform.position.y;
        zRand = Random.Range(-50, 50);

        randPos = new Vector3(xRand, yRand, zRand);
    }

    
    void Update()
    {
        //agent.SetDestination(dest.transform.position);

        agent.SetDestination(randPos);

        if (Mathf.Floor( transform.position.x) == Mathf.Floor(xRand) && Mathf.Floor(transform.position.z) == Mathf.Floor(zRand))
        {
            print("New Position generated");

            xRand = Random.Range(-50, 50);
            yRand = transform.position.y;
            zRand = Random.Range(-50, 50);

            randPos = new Vector3(xRand, yRand, zRand);

        }


    }
}
