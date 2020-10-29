using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1000f;

    Rigidbody rigiBody;
    AudioSource audioSource;

    enum State{Alive,Dying,Transcending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(state==State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
       if(state !=State.Alive)return; //不會觸發其他碰撞效果
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Debug.Log("Hit Finish");
                Invoke("LoadNextLevel",1.5f); 
                break;
            default:
                state = State.Dying;
                Debug.Log("Dead");
                Invoke("LoadFirstLevel",1.5f); 
                break;
        }
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        state = State.Alive;
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
