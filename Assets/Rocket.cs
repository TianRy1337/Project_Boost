using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigiBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
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
            rigiBody.AddRelativeForce(Vector3.up);
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
