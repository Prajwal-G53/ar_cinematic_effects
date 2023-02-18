using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInstantiate : MonoBehaviour
{
    public GameObject capsule_pf;
    public int go_count=100;
    void Start()
    {
        for (int i = 0; i < go_count; i++)
        {
            Instantiate(capsule_pf);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
