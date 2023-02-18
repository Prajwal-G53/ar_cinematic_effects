using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward_withDelay : MonoBehaviour
{
    [SerializeField]
    private bool moved;

    [SerializeField]
    private Transform front_go;
    // Start is called before the first frame update
    void Start()
    {
    //    moved = false;
    //StartCoroutine(giveDelay());
    }

    IEnumerator giveDelay()
    {
        yield return new WaitForSeconds(1);

        Vector3 forwd_target_local_pos = transform.localPosition + Vector3.forward * 15;

        Vector3 forward_target_global_pos;//= transform.TransformPoint(forwd_target_local_pos);

        if(front_go)
        {
            //forward_target_global_pos = transform.Find("Forward_pos").position;

            forward_target_global_pos = front_go.position;
            print($"Moved pos {transform.name} " + transform.position);
            while ((transform.position - forward_target_global_pos).magnitude > 0.08f)
            {

                transform.position = Vector3.Lerp(transform.position, forward_target_global_pos, Time.deltaTime * 3f);
                yield return null;
            }
        }
        print($"Moved pos {transform.name} " + transform.position);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Instantiate_Circular.move&&moved==false)
        {
            moved = true; 
           StartCoroutine(giveDelay());
        }
    }
}
