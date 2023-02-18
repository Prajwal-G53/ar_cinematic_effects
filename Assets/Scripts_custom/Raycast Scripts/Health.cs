using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{

    public TextMeshPro healthUI;
    int healthNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    healthNumber++;
        //    healthUI.text = healthNumber.ToString();
        //}
    }

    public void increaseHealth()
    {
        healthNumber++;
        healthUI.text = healthNumber.ToString();

       
    }
}
