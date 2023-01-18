using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public bool isStarted = false;
    public float timer = 15f;
    public AudioClip fireClip;
    public AudioClip explosionClip;
    AudioSource audioSource;
    bool startFire = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
        {
            timer -= Time.deltaTime;
            if(startFire)
            {
                audioSource.loop = true;
                audioSource.clip = fireClip;
            }
            if(timer<=0)
            {
                audioSource.loop = false;
                audioSource.PlayOneShot(explosionClip);
                isStarted = false;
            }
        }
    }
}
