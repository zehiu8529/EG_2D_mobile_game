using UnityEngine;

[ExecuteInEditMode]
//Run in EditMode

public class Control3D_Sticky : MonoBehaviour
//Stick to Tarket GameObject
{
    public Transform t_Tarket;
    //Tarket

    public bool b_Lock_Pos = true;
    //Auto Stick to Tarket
    public Vector3 v_Lock_Pos = new Vector3(1, 1, 0);
    //Offset Pos Stick to Tarket

    public bool b_Lock_Sca = true;
    //Not Chance Scale follow Tarket Scale
    private Vector3 v_Lock_Scale;

    private void Awake()
    {
        if (t_Tarket == null)
            return;

        if (b_Lock_Sca)
        {
            v_Lock_Scale = t_Tarket.localScale;
        }
    }

    private void Update()
    {
        if (t_Tarket == null)
            return;

        Auto_Follow();
        Auto_Scale();
    }

    private void Auto_Follow()
    {
        if (!b_Lock_Pos)
            return;
        transform.position = t_Tarket.transform.position + v_Lock_Pos;
    }

    private void Auto_Scale()
    {
        if (!b_Lock_Sca)
            return;
        Vector2 v_Lock_Cur = new Vector2(t_Tarket.localScale.x, t_Tarket.localScale.y);
        if (v_Lock_Cur.x > 0 && v_Lock_Scale.x < 0 || v_Lock_Cur.x < 0 && v_Lock_Scale.x > 0)
        {
            v_Lock_Scale.x = v_Lock_Cur.x;
            transform.localScale = 
                new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if(v_Lock_Cur.y > 0 && v_Lock_Scale.y < 0 || v_Lock_Cur.y < 0 && v_Lock_Scale.y > 0)
        {
            v_Lock_Scale.y = v_Lock_Cur.y;
            transform.localScale = 
                new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }
    }
}
