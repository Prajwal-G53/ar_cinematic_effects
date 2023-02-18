using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_follow : MonoBehaviour
{

    public NavMeshAgent agent;

    public GameObject follow_player;

    private Vector3 dest,foll_pl_pos;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        foll_pl_pos = follow_player.transform.position;
        dest = new Vector3(foll_pl_pos.x, transform.position.y, foll_pl_pos.z);
        agent.SetDestination(dest);
    }
}
