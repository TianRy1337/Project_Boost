﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float levelLoadDelay = 1.5f;


    [SerializeField] AudioClip mainEngine, deathEngine, winLevel;
    [SerializeField] ParticleSystem mainEngineParticle, deathEngineParticle, winLevelParticle;

    Rigidbody rigiBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
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
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加載當前場景 buildIndex可替換成 name 效果並無差別 加載字串和數值都差別而已
        }
#endif
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (state != State.Alive) return; //不會觸發其他碰撞效果
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                Debug.Log("Hit Finish");
                audioSource.PlayOneShot(winLevel);
                winLevelParticle.Play();
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathEngine);
                deathEngineParticle.Play();
                Invoke("LoadFirstLevel", levelLoadDelay);
                break;
        }
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene+1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene =0;
        }
        SceneManager.LoadScene(nextScene);
        state = State.Alive;
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加載當前場景 buildIndex可替換成 name 效果並無差別 加載字串和數值都差別而已
        state = State.Alive;
    }

    private void Rotate()
    {
        float RotateThis = rcsThrust * Time.deltaTime;
        rigiBody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotating Left");
            transform.Rotate(Vector3.forward * RotateThis);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotateThis);
        }
        

    }
    private void Thrust()
    {
        float upThis = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigiBody.AddRelativeForce(Vector3.up * upThis );
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            mainEngineParticle.Play();//只會出現沒按的時候
        }
        else
        {
            audioSource.Stop();
            //mainEngineParticle.Stop();
        }
    }
}
