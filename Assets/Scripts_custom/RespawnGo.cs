using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGo : MonoBehaviour
{

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetInitialPosition(Vector3 _startPos)
    {
        startPos = _startPos;
        InvokeRepeating("RespawnGameObject", 2.1f,1f);
    }
    private void RespawnGameObject()
    {
        transform.position = startPos;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
