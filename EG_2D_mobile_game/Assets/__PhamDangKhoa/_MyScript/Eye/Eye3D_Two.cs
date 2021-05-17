using UnityEngine;

public class Eye3D_Two : MonoBehaviour
{
    [Header("Main")]
    public LayerMask l_Tarket;
    public LayerMask l_Barrier;
    //LayerMask Debug

    public Transform t_Start;
    public Transform t_End;
    //Point

    [Header("End Point is 'Pos' or 'Dir'?")]
    public bool b_EndIsPos = true;
    public float f_Distance = 5f;

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int i_Cast = 0;
    //Type of Cast

    [Header("BoxCast")]
    public Vector3 v3_Square = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 v3_Square_Rot = new Vector3();

    [Header("SphereCast")]
    public float f_Sphere = 0.1f;

    public float Get_Distance_Transform()
    //Distance from Start to End Point
    {
        Class_Vector cl_Vector = new Class_Vector();
        return cl_Vector.Get_Distance(t_Start, t_End);
    }

    //Cast của Start và End

    public bool Get_LineCast_Check_LayerMask()
    //LineCast from Start to End
    {
        Class_Eye cs_Eye = new Class_Eye();
        if (cs_Eye.Get_LineCast_Check(t_Start.position, t_End.position, l_Tarket))
        {
            //Always Hit if Tarket on LineCast
            return true;
        }
        return false;
    }

    public bool Get_RayCast_Check_LayerMask()
    //Raycast from Start to End
    {
        Class_Eye cs_Eye = new Class_Eye();
        if (cs_Eye.Get_RayCast_Vec_Check(t_Start.position, t_End.position, Get_Distance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (cs_Eye.Get_RayCast_Vec_Check(t_Start.position, t_End.position, Get_Distance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool Get_BoxCast_Check_LayerMask()
    //BoxCast from Start to End
    {
        Class_Eye cs_Eye = new Class_Eye();
        if (cs_Eye.Get_BoxCast_Vec_Check(t_Start.position, v3_Square, t_End.position, v3_Square_Rot, Get_Distance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (cs_Eye.Get_BoxCast_Vec_Check(t_Start.position, v3_Square, t_End.position, v3_Square_Rot, Get_Distance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool Get_SphereCast_Check_LayerMask()
    //SphereCast from Start to End
    {
        Class_Eye cs_Eye = new Class_Eye();
        if (cs_Eye.Get_SphereCast_Vec_Check(t_Start.position, f_Sphere, t_End.position, Get_Distance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (cs_Eye.Get_SphereCast_Vec_Check(t_Start.position, f_Sphere, t_End.position, Get_Distance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (i_Cast == 0 || t_Start == null || t_End == null)
            return;

        Class_Vector cl_Vector = new Class_Vector();
        Class_Eye cs_Eye = new Class_Eye();

        switch (i_Cast)
        {
            case 1:
                //LineCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                //End Point is Black
                if (cs_Eye.Get_LineCast_Check(
                    t_Start.position,
                    t_End.position,
                    l_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = cs_Eye.Get_LineCast_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    //Red is Hit
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_End.position);
                }
                else
                {
                    Gizmos.color = Color.white;
                    //Start Point is White, if Hit is Red
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_End.position);
                }
                Gizmos.DrawWireSphere(t_Start.position, f_Sphere / 2);
                break;
            case 2:
                //RayCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                if (cs_Eye.Get_RayCast_Vec_Check(
                    t_Start.position,
                    t_End.position,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Barrier))
                {
                    //Hit Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_RayCast_Vec_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                }
                else
                if (cs_Eye.Get_RayCast_Vec_Check(
                    t_Start.position,
                    t_End.position,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = cs_Eye.Get_RayCast_Vec_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ((b_EndIsPos) ? Get_Distance_Transform() : f_Distance));
                }
                Gizmos.DrawWireSphere(t_Start.position, f_Sphere / 2);
                break;
            case 3:
                //BoxCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    f_Sphere / 2);
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    t_Start.position,
                    v3_Square,
                    t_End.position,
                    v3_Square_Rot,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        t_Start.position,
                        v3_Square,
                        t_End.position,
                        v3_Square_Rot,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        v3_Square);
                }
                else
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    t_Start.position,
                    v3_Square,
                    t_End.position,
                    v3_Square_Rot,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        t_Start.position,
                        v3_Square,
                        t_End.position,
                        v3_Square_Rot,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((b_EndIsPos) ? Get_Distance_Transform() : f_Distance));
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * f_Distance,
                        v3_Square);
                }
                Gizmos.DrawWireCube(
                    t_Start.position,
                    v3_Square);
                break;
            case 4:
                //SphereCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    f_Sphere / 2);
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    t_Start.position,
                    f_Sphere,
                    t_End.position,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        t_Start.position,
                        f_Sphere,
                        t_End.position,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        f_Sphere / 2);
                }
                else
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    t_Start.position,
                    f_Sphere,
                    t_End.position,
                    (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                    l_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        t_Start.position,
                        f_Sphere,
                        t_End.position,
                        (b_EndIsPos) ? Get_Distance_Transform() : f_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        f_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((b_EndIsPos) ? Get_Distance_Transform() : f_Distance));
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((b_EndIsPos) ? Get_Distance_Transform() : f_Distance),
                        f_Sphere / 2);
                }
                Gizmos.DrawWireSphere(
                    t_Start.position,
                    f_Sphere / 2);
                break;
        }
    }
}
