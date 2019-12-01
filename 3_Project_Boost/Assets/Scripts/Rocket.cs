using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //public SwitchCharacterScript player;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

 

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip sucsess;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem sucsessParticles;
    [SerializeField] ParticleSystem deathParticles;

   

    Scene currentScene;
    AudioSource audioSource;
    Rigidbody rigidBody;

    enum State {Alive, Dying, Transcending }

    
    State state = State.Alive;
    private int currentSceneNum;

    // Start is called before the first frame update
    void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    void Update()
    {
        // if (player.SwitchAvatar() == 1 || player.SwitchAvatar() == 0)
        //{
        if (state == State.Alive)
        {
             currentSceneNum = SceneManager.GetActiveScene().buildIndex;
            Thrust();
            Rotate();
        }
        //}
    }

    void OnCollisionEnter(Collision collision)
    {

        if(state  != State.Alive){   return; } // ignore collisions when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            case "Finish":
                StartSucsessSequence();
                break;
           default:

               StartDeathSequence();
                break;

        }

      
    }

    private void StartSucsessSequence()
    {
        state = State.Transcending;
        Invoke("loadNextLevel", 2f);
        
        audioSource.PlayOneShot(sucsess);
        sucsessParticles.Play();
   

    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();

        deathParticles.Play();

        Invoke("began", 2f);

        audioSource.PlayOneShot(death);
    }
   

    private void began()
    {
        SceneManager.LoadScene(currentSceneNum);
    }

    private void loadNextLevel()
    {

      

         var totalNumOfScenes = SceneManager.sceneCountInBuildSettings;

         
        var nextScene = currentSceneNum + 1;

        if (nextScene == totalNumOfScenes)
            SceneManager.LoadScene(0);

        SceneManager.LoadScene(nextScene);
    }


    

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.W))
            ApplyThrust();
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying)// so it dosent layer on top of each other
            audioSource.PlayOneShot(mainEngine);

        mainEngineParticles.Play();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {

            
            
            transform.Rotate(Vector3.forward *rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(-Vector3.forward * rotationThisFrame);

        }
        rigidBody.freezeRotation = false;

    }

  


}
