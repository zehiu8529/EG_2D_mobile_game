using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rigid3D_Component))]

public class Control3D_MovePlatform : MonoBehaviour
//Move Control Platform (X)
{
    private Rigid3D_Component cs_Rigid;
    //Use "Move" of this Script

    [Header("Keyboard")]
    public KeyCode k_MoveLeft = KeyCode.LeftArrow;
    //Control Move Left

    public KeyCode k_MoveRight = KeyCode.RightArrow;
    //Control Move Right

    public KeyCode k_SpeedChance = KeyCode.LeftShift;
    //Control Speed Chance

    [Header("Move")]
    public float f_SpeedNormal = 2f;
    //Normal Speed Move

    public float f_SpeedChance = 5f;
    //Chance Speed Move
    private float f_SpeedCur;
    //Current Speed Move

    public bool b_StopRightAway = false;
    //Control Stop without Speed Stop Velocity

    public float f_SpeedStop = 3f;
    //Speed Stop

    private void Awake()
    {
        cs_Rigid = GetComponent<Rigid3D_Component>();
    }

    private void Update()
    {
        Set_SpeedChance();
        Set_MoveButton();
    }

    public void Set_MoveButton()
    //Control Move by Keyboard
    {
        if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
            //Press "Left" & "Right" same time >> Not Move
            Set_Stop();
        else
            if (Input.GetKey(k_MoveLeft))
            //Left
            Set_Move(-1);
        else
            if (Input.GetKey(k_MoveRight))
            //Right
            Set_Move(1);
        else
            //Not Press
            Set_Stop();
    }

    private void Set_Move(int i_ButtonMove)
    //Control Move
    {
        cs_Rigid.Set_MoveX_Velocity(i_ButtonMove, f_SpeedCur, f_SpeedCur);
    }

    private void Set_Stop()
    //Control Stop
    {
        if (b_StopRightAway)
            cs_Rigid.Set_StopX_Velocity();
        else
            cs_Rigid.Set_StopX_Velocity(f_SpeedStop);
    }

    public void Set_SpeedChance()
    //Control Speed Chance
    {
        f_SpeedCur = (Input.GetKey(k_SpeedChance))? f_SpeedChance : f_SpeedNormal;
    }
}