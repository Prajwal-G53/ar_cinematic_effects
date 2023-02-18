using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHealthShoot : MonoBehaviour
{

    public GameObject ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray.transform.position, ray.transform.forward, out hit, Mathf.Infinity))
            {
                hit.transform.GetComponent<Health>().increaseHealth();
                hit.transform.GetComponent<Rigidbody>().AddExplosionForce(10, ray.transform.position, 20);


            }
        }
    }
}
