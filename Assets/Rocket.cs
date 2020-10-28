using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1000f;

    Rigidbody rigiBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        float RotateThis = rcsThrust * Time.deltaTime;
        rigiBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotating Left");
            transform.Rotate(Vector3.forward * RotateThis);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotateThis);
        }
        rigiBody.freezeRotation = false;

    }
    private void Thrust()
    {
        float upThis = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigiBody.AddRelativeForce(Vector3.up * upThis);
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
