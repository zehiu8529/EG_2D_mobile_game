using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3D_Area : MonoBehaviour
{
    public Transform t_Next;
    //CheckPoint tiếp theo (nếu có) để gán CheckPoint mục tiêu cho GameObject chứa "CheckPoint3D_Get.cs"

    public Vector3 v3_Size = new Vector3(1f, 1f, 1f);
    //Kích thước CheckPoint Cube

    public LayerMask l_Tarket;
    //LayerMask sẽ nhận CheckPoint

    private void Update()
    {
        Set_Message();
    }

    private void Set_Message()
    {
        Collider[] c_Collide = Get_Collide();

        for (int i = 0; i < c_Collide.Length; i++) 
        {
            if(c_Collide[i].gameObject.GetComponent<CheckPoint3D_Get>() != null)
            {
                c_Collide[i].gameObject.GetComponent<CheckPoint3D_Get>().Set_Next(this.t_Next);
            }
        }
    }

    private Collider[] Get_Collide()
    {
        Class_Vector cl_Vector = new Class_Vector();

        return
            Physics.OverlapBox(
            transform.position,
            v3_Size / 2f,
            cl_Vector.Get_Rot_VectorToTransform(0, 0, 0),
            l_Tarket);
    }

    private void OnDrawGizmos()
    {
        Class_Vector cl_Vector = new Class_Vector();

        if (Physics.OverlapBox(
            transform.position,
            v3_Size / 2f,
            cl_Vector.Get_Rot_VectorToTransform(0, 0, 0),
            l_Tarket).Length > 0) 
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, v3_Size);

        Gizmos.color = Color.white;

        //if (t_Next != null)
        //{
        //    Gizmos.DrawLine(
        //        transform.position,
        //        transform.position - new Vector3(transform.position.x - t_Next.transform.position.x, 0f, 0f));

        //    Gizmos.DrawLine(
        //        transform.position - new Vector3(transform.position.x - t_Next.transform.position.x, 0f, 0f),
        //        transform.position - new Vector3(transform.position.x - t_Next.transform.position.x, transform.position.y - t_Next.transform.position.y, 0f));

        //    Gizmos.DrawLine(
        //        transform.position - new Vector3(transform.position.x - t_Next.transform.position.x, transform.position.y - t_Next.transform.position.y, 0f),
        //        transform.position - new Vector3(transform.position.x - t_Next.transform.position.x, transform.position.y - t_Next.transform.position.y, transform.position.z - t_Next.transform.position.z));
        //}

        if (t_Next!=null)
        {
            Gizmos.DrawLine(transform.position, t_Next.position);
        }
    }
}
