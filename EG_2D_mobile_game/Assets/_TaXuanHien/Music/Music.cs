using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [Header("Music")]
    public AudioMixer audioMusic;

   


    public void SetVolume(float Music)
    {

        audioMusic.SetFloat("Music", Music);
    }

  
}
