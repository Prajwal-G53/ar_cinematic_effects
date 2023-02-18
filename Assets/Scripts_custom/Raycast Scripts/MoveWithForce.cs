using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{

    public Transform rayUp;

    public float rayDist = 5.0f;

    public Rigidbody rb;

    public float force = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(rayUp.transform.position,rayUp.transform.up,out hit,rayDist))
        {
            print("Hit _" + hit.transform.name);

            Debug.DrawLine(rayUp.transform.position, hit.point, Color.red);

            rb.AddForce((hit.transform.position - transform.position) * force);
            //rb.AddForce(rayUp.transform.up.normalized*hit.distance * force);


        }
        
    }
}
