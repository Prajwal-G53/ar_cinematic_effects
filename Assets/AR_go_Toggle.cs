using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_go_Toggle : MonoBehaviour
{ // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            gameObject.SetActive(false);

        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
