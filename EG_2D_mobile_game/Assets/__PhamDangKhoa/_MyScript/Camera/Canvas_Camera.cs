using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Camera : MonoBehaviour
{
    [Header("Camera")]
    public KeyCode k_Next = KeyCode.RightBracket; 
    //Keycode: ]
    public KeyCode k_Back = KeyCode.LeftBracket;
    //Keycode: [
    //Chance Camera button

    public List<GameObject> l1_Camera = new List<GameObject>();
    //List of Camera (Start at First Camera)
    private int i_Camera = 0;
    //Index of Camera in List is Enable

    private Canvas c_Canvas;

    private void Awake()
    {
        c_Canvas = GetComponent<Canvas>();

        Set_CameraEnable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(k_Next))
        {
            i_Camera++;
            if (i_Camera > l1_Camera.Count - 1)
                i_Camera = 0;
            Set_CameraEnable();
        }
        else
        if (Input.GetKeyDown(k_Back))
        {
            i_Camera--;
            if (i_Camera < 0)
                i_Camera = l1_Camera.Count - 1;
            Set_CameraEnable();
        }
    }

    private void Set_CameraEnable()
    {
        c_Canvas.worldCamera = l1_Camera[i_Camera].GetComponent<Camera>();
        l1_Camera[i_Camera].SetActive(true);

        for (int i = 0; i < l1_Camera.Count; i++)
        {
            if (i != i_Camera) 
            {
                l1_Camera[i].SetActive(false);
            }
        }
    }

}
