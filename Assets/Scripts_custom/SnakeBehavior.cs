using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject humanoid_pfb;
    [SerializeField]
    List<GameObject> cube_points;

    GameObject[] cp_arr;
    // Start is called before the first frame update
    void Start()
    {
        
        //cube_points = new List<GameObject>();
       cp_arr= GameObject.FindGameObjectsWithTag("cube_point");

        foreach(GameObject cp in cp_arr)
        {
            GameObject go = Instantiate(humanoid_pfb);
            go.transform.position = cp.transform.position;
            go.transform.forward = cp.transform.forward;
            go.transform.parent = cp.transform;
            go.transform.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
