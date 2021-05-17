using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rigid3D_Component))]

public class Eye3D_Fan : MonoBehaviour
//Eye Fan Cast
//Distance 0 mean not Cast (Even "INSIDE" other GameObject)
{
    [Header("Fan in 'XZ' or 'XY'?")] 
    public bool b_XZ = true;
    //World

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int i_Cast = 0;
    //Cast Type

    public LayerMask l_Barrier;
    //Layer Cast

    [Header("Centre")]
    public float f_Distance = 2f;
    //Radius Cast

    [Header("Fan")]
    public int i_Fan = 1;
    //Half of Cast from Centre Cast
    [Range(0, 360)]
    public float f_OffFan = 15f;
    //Offset between Cast

    [Header("Chance")]
    public Vector3 v3_OffPos = new Vector3();
    //Offset Eye

    [Header("BoxCast")]
    public Vector3 v3_Square = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("SphereCast")]
    public float f_Sphere = 0.1f;

    //Eye

    private RaycastHit Get_Eye(int i_CastIndex)
    //Get EyeCast (0: Centre; +1: Top; -1: Bot)
    {
        List<RaycastHit> l_RaycastHit = new List<RaycastHit>();

        Class_Vector cl_Vector = new Class_Vector();

        Class_Eye cs_Eye = new Class_Eye();
        //Use this Script to use all Methode of Eye
        Rigid3D_Component cs_Rigid = GetComponent<Rigid3D_Component>();
        //Use this Script to get "Rotation" of this Object

        if (!b_XZ)
        {
            //LineCast
            if (i_Cast == 1)
            {
                if (cs_Eye.Get_LineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), l_Barrier))
                {
                    //Always Hit Tarket on Line
                    return cs_Eye.Get_LineCast_RaycastHit(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        l_Barrier);
                }
            }
            //RayCast
            else
            if (i_Cast == 2)
            {
                if (cs_Eye.Get_RayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), f_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_RayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                }
            }
            //BoxCast
            else
            if (i_Cast == 3) 
            {
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    new Vector3(0, 0, 0),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        new Vector3(0, 0, 0),
                        f_Distance,
                        l_Barrier);
                }
            }
            //SphereCast
            else
            if (i_Cast == 4)
            {
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    f_Sphere,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        f_Sphere,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                }
            }
        }
        else
    if (b_XZ)
        {
            //LineCast
            if (i_Cast == 1)
            {
                if (cs_Eye.Get_LineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_LineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        l_Barrier);
                }
            }
            //RayCast
            else
            if (i_Cast == 2)
            {
                if (cs_Eye.Get_RayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), f_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_RayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                }
            }
            //BoxCast
            else
            if (i_Cast == 3)
            {
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    new Vector3(0, 0, 0),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        new Vector3(0, 0, 0),
                        f_Distance,
                        l_Barrier);
                }
            }
            //SphereCast
            else
            if (i_Cast == 4) 
            {
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    f_Sphere,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        f_Sphere,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                }
            }
        }

        return new RaycastHit();
    }

    public List<RaycastHit> Get_List()
    //Get List EyeCast (0:Centre; %2!=0: Top; %2==0: Bot)
    {
        List<RaycastHit> l_RaycastHit = new List<RaycastHit>();

        //Centre
        l_RaycastHit.Add(Get_Eye(0));

        for (int i = 1; i <= i_Fan; i++)
        {
            //Top (%2!=0)
            l_RaycastHit.Add(Get_Eye(i));
            //Bot (%2==0)
            l_RaycastHit.Add(Get_Eye(-i));
        }

        return l_RaycastHit;
    }

    public List<float> Get_List_Distance(int i_Minimize)
    //Get List Distance (Decrease by Minimize)
    {
        List<RaycastHit> l1_Ray = Get_List();

        List<float> l1_Distance = new List<float>();

        for(int i = 0; i< l1_Ray.Count; i++)
        {
            if(l1_Ray[i].collider == null)
                l1_Distance.Add(-1f);
            else
                l1_Distance.Add(l1_Ray[i].distance / i_Minimize);
        }
        return l1_Distance;
    }

    public List<float> Get_List_Distance()
    //Get List Distance
    {
        List<RaycastHit> l1_Ray = Get_List();

        List<float> l1_Distance = new List<float>();

        for (int i = 0; i < l1_Ray.Count; i++)
        {
            if (l1_Ray[i].collider == null)
                l1_Distance.Add(-1f);
            else
                l1_Distance.Add(l1_Ray[i].distance);
        }
        return l1_Distance;
    }

    public RaycastHit Get_Eye_Top(int i_Index)
    //Get EyeCast Top
    {
        return Get_Eye(i_Index * 2 - 1);
    }

    public bool Get_Eye_Top_Check(int i_Index)
    //Get Check EyeCast Top
    {
        return Get_Eye(i_Index * 2 - 1).collider != null;
    }

    public RaycastHit Get_Eye_Bot(int i_Index)
    //Get EyeCast Bot
    {
        return Get_Eye(i_Index * 2);
    }

    public bool Get_Eye_Bot_Check(int i_Index)
    //Get Check EyeCast Bot
    {
        return Get_Eye(i_Index * 2).collider != null;
    }

    public int Get_Fan_Count()
    //Get Count Fan Eye (Always have 1 Fan Centre)
    {
        return 1 + i_Fan * 2;
    }

    //=== Gizmos

    private void Set_Gizmos(int i_CastIndex)
    {
        Class_Vector cl_Vector = new Class_Vector();

        Class_Eye cs_Eye = new Class_Eye();
        Rigid3D_Component cs_Rigid = GetComponent<Rigid3D_Component>();

        if (!b_XZ)
        {
            //LineCast
            if (i_Cast == 1)
            {
                if (cs_Eye.Get_LineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_LineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
            }
            //RayCast
            else
            if (i_Cast == 2)
            {
                if (cs_Eye.Get_RayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_RayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
            }
            //BoxCast
            else
            if (i_Cast == 3)
            {
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    new Vector3(0, 0, 0),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        new Vector3(0, 0, 0),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance),
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        v3_Square);
                }
            }
            //SphereCast
            else
            if (i_Cast == 4) 
            {
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    f_Sphere,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        f_Sphere,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance),
                        f_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XY(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Sphere / 2);
                }
            }
        }
        else
        if (b_XZ)
        {
            //LineCast
            if (i_Cast == 1)
            {
                if (cs_Eye.Get_LineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_LineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
            }
            //RayCast
            else
            if (i_Cast == 2)
            {
                if (cs_Eye.Get_RayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance), f_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_RayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                }
            }
            //BoxCast
            else
            if (i_Cast == 3)
            {
                if (cs_Eye.Get_BoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    new Vector3(0, 0, 0),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_BoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        new Vector3(0, 0, 0),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance),
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        v3_Square);
                }
            }
            //SphereCast
            else
            if (i_Cast == 4) 
            {
                if (cs_Eye.Get_SphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    f_Sphere,
                    transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                    f_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.Get_SphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        f_Sphere,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, ray_Hit.distance),
                        f_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ() + i_CastIndex * f_OffFan, f_Distance),
                        f_Sphere / 2);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Centre
        Set_Gizmos(0);

        //Fan
        Gizmos.color = Color.red;

        for (int i = 1; i <= i_Fan; i++)
        {
            Set_Gizmos(i);
            Set_Gizmos(-i);
        }
    }
}