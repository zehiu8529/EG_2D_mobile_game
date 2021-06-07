using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimatedButton;

public class Click : MonoBehaviour
{
    public AudioSource audiosrc;
    public AudioClip kick;
    public float Soundkick = 1f;


    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();


    public ButtonClickedEvent onClick
    {
        get { audiosrc.PlayOneShot(kick, Soundkick); return m_OnClick; }
        set { m_OnClick = value; }
    }
    public void EvenKick()
    {
        audiosrc.PlayOneShot(kick, Soundkick);
    }
}
