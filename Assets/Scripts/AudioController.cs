using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    public enum BGM
    {
        mainBGM,
        
    }
    public enum SE
    {
        moveButton,
        pushButton,
        enter,
        exit,
        flip,
        destroyMeteor,
        clashToMeteor,
        clashToEarth,
        alert,
        UFO,
        item,
        hit1,
        hit2,
        hit3,
        hit4,
        hit5,
        end,
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] BGMClips, SEClips;
    [SerializeField,Range(0, 1)] float[] volume ;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.PlayOneShot(BGMClips[0]);
        //audioSource.loop = true;
        //audioSource.PlayScheduled(AudioSettings.dspTime + BGMClips[0].length);

        PlayBGM(BGM.mainBGM,true);
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySE(SE num)
    {
        audioSource.PlayOneShot(SEClips[(int)num],volume[(int)num]);
    }

    public void PlayBGM(BGM num, bool loop)
    {
        audioSource.clip = BGMClips[(int)num];
        if (loop)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }
        audioSource.Play();
    }
}
