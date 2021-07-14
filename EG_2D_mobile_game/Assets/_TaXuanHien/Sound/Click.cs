using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimatedButton;

public class Click : MonoBehaviour
{
    public AudioSource audiosrc;
    public AudioClip kick;
    public float Soundkick = 1f;

    public void EvenKick()
    {
        audiosrc.PlayOneShot(kick, Soundkick);
    }
}
