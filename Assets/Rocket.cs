using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("Thrusting");
        } 
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("Rotating Left");
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            print("Rotating Right");
        }
    }
}
