using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_OneAboveAnother : MonoBehaviour
{

    public GameObject go_pf;

    public int noOfgos=5;
    public float dis_apart=3;
    private Vector3 pos = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        float dis = 0;
        if (go_pf != null)
        {
            for (int i = 0; i < noOfgos; i++)
            {
                GameObject go=  Instantiate(go_pf, pos, Quaternion.identity);
                go.transform.Translate(Vector3.up * dis, Space.World);

                dis += go.transform.localScale.y+dis_apart;


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
