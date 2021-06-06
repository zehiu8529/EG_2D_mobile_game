using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Sound_Component : MonoBehaviour
//Nếu muốn GameObject phát ra âm thanh, gán hàm này và tự lập trình sử dụng hàm dưới trong các Script khác
{
    public void Set_3DSound()
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
            return;
        GetComponent<AudioSource>().spatialBlend = 1f;
    }

    public void Set_2DSound()
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
            return;
        GetComponent<AudioSource>().spatialBlend = 0f;
    }

    public void Set_PlaySound(AudioClip a_Sound, float f_Volumn, bool b_Loop) //Gọi hàm khi cần phát âm thanh
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
            return;
        if(GetComponent<AudioSource>().clip != a_Sound)
        {
            GetComponent<AudioSource>().clip = a_Sound;
            GetComponent<AudioSource>().volume = f_Volumn;
            GetComponent<AudioSource>().loop = b_Loop;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().volume = f_Volumn;
            GetComponent<AudioSource>().loop = b_Loop;
        }
        //GetComponent<AudioSource>().PlayOneShot(a_Sound, f_Volumn);
    }

    public void Set_PlaySound(AudioClip a_Sound, float f_Volumn, bool b_Loop, bool b_Continue) //Gọi hàm khi cần phát âm thanh
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
            return;
        GetComponent<AudioSource>().clip = a_Sound;
        GetComponent<AudioSource>().volume = f_Volumn;
        GetComponent<AudioSource>().loop = b_Loop;
        if (!b_Continue)
            GetComponent<AudioSource>().Play();
    }

    public void Set_MuteSound(bool b_MuteSound)
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
            return;
        GetComponent<AudioSource>().mute = b_MuteSound;
    }
}
