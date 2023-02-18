using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_finding : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject center_go = new GameObject("center");

        center_go.transform.position = Calculate_b(transform).center;
    }

    public static Bounds Calculate_b(Transform TheObject)//To calculate bound of all the bars 
    {
        var renderers = TheObject.GetComponentsInChildren<Renderer>();
        Bounds combinedBounds = renderers[0].bounds;
        for (int i = 0; i < renderers.Length; i++)
        {
            combinedBounds.Encapsulate(renderers[i].bounds);
        }
        return combinedBounds;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
