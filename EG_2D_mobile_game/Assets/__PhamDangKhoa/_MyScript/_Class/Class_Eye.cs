using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on Cast
/// </summary>
public class Class_Eye
{
    public Class_Eye()
    {

    }

    //Cast

    //Linecast

    /// <summary>
    /// LineCast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_LineCast_Check(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    //LineCast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.Linecast(v3_Start, v3_End, out ray_Hit, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// LineCast Vec Raycast
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_LineCast_RaycastHit(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    //LineCast Vec Raycast
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.Linecast(v3_Start, v3_End, out ray_Hit, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// LineCast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_LineCast_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    //LineCast Vec Check
    {
        if (Get_LineCast_Check(v3_Start, v3_End, l_Tarket))
        {
            return true;
        }
        return false;
    }

    //Raycast

    /// <summary>
    /// Raycast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_RayCast_Dir_Check(Vector3 v3_Start, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    //Raycast Dir Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Raycast Dir Raycast
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_RayCast_Dir_RaycastHit(Vector3 v3_Start, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    //Raycast Dir Raycast
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_RayCast_Vec_Check(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    //Raycast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        b_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_RayCast_Vec_RaycastHit(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    //Raycast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        b_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_RayCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket, LayerMask l_Barrier)
    //Raycast Vec Check
    {
        Class_Vector cl_Vector = new Class_Vector();
        if (Get_RayCast_Vec_Check(v3_Start, v3_End, cl_Vector.Get_Distance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (Get_RayCast_Vec_Check(v3_Start, v3_End, cl_Vector.Get_Distance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_RayCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    //Raycast Vec Check
    {
        if (Get_RayCast_Vec_Check(v3_Start, v3_End, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (Get_RayCast_Vec_Check(v3_Start, v3_End, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Boxcast

    /// <summary>
    /// Boxcast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_BoxCast_Dir_Check(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_Forward, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    //Boxcast Dir Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        b_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Boxcast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_BoxCast_Dir_RaycastHit(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_Forward, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    //Boxcast Dir Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        b_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_BoxCast_Vec_Check(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    //Boxcast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        b_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_BoxCast_Vec_RaycastHit(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    //Boxcast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        b_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_BoxCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    //Boxcast Vec Check
    {
        if (Get_BoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (Get_BoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_BoxCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, LayerMask l_Tarket, LayerMask l_Barrier)
    //Boxcast Vec Check
    {
        Class_Vector cl_Vector = new Class_Vector();
        if (Get_BoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, cl_Vector.Get_Distance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (Get_BoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, cl_Vector.Get_Distance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Spherecast

    /// <summary>
    /// Spherecast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_SphereCast_Dir_Check(Vector3 v3_Start, float f_Radius, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    //Spherecast Dir Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.SphereCast(v3_Start, f_Radius, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Spherecast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_SphereCast_Dir_RaycastHit(Vector3 v3_Start, float f_Radius, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    //Spherecast Dir Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        b_Hit = Physics.SphereCast(v3_Start, f_Radius, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool Get_SphereCast_Vec_Check(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    //Spherecast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        b_Hit = Physics.SphereCast(v3_Start, f_Radius / 2, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return b_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit Get_SphereCast_Vec_RaycastHit(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    //Spherecast Vec Check
    {
        bool b_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        b_Hit = Physics.SphereCast(v3_Start, f_Radius / 2, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_SphereCast_Vec_Check_LayerMask(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    //Spherecast Vec Check
    {
        if (Get_SphereCast_Vec_Check(v3_Start, f_Radius, v3_End, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (Get_SphereCast_Vec_Check(v3_Start, f_Radius, v3_End, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool Get_SphereCast_Vec_Check_LayerMask(Vector3 v3_Start, float f_Radius, Vector3 v3_End, LayerMask l_Tarket, LayerMask l_Barrier)
    //Spherecast Vec Check
    {
        Class_Vector cl_Vector = new Class_Vector();
        if (Get_SphereCast_Vec_Check(v3_Start, f_Radius, v3_End, cl_Vector.Get_Distance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (Get_SphereCast_Vec_Check(v3_Start, f_Radius, v3_End, cl_Vector.Get_Distance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }
}
