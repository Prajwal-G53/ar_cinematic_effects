using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_Active : MonoBehaviour
{
    public Button toogle_button;
    public bool active;

    public List<GameObject> gos;
    // Start is called before the first frame update
    void Start()
    {
        toogle_button.onClick.AddListener(activate_deactivate);
        active = true;
    }

    void activate_deactivate()
    {
        if(active==true)
        {
            active = false;
        }
        else
        {
            active = true;
        }

        foreach(GameObject go in gos)
        {
            go.SetActive(active);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
