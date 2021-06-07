using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Sound : MonoBehaviour
{
    [Header("Sound")]
    public AudioMixer audioSound;

    // Use this for initialization
   
   
    public void SetSound(float Sound)
    {

        audioSound.SetFloat("Sound", Sound);
        Debug.Log(audioSound.SetFloat("Sound", Sound)); // lỗi 

    }


}
