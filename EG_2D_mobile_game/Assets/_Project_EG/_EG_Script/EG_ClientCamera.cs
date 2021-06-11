using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_ClientCamera : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Client Tag")]
    [SerializeField]
    private string s_Client_Tag = "IsometricClient";

    [SerializeField]
    private Camera_Component cl_Camera;

    private Socket_ClientManager cl_ClientManager;

    private Transform t_ClientTransform;

    private void Start()
    {
        cl_ClientManager = GetComponent<Socket_ClientManager>();
    }

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (t_ClientTransform == null)
            {
                if (s_Client_Tag != "")
                {
                    t_ClientTransform = GameObject.FindGameObjectWithTag(s_Client_Tag).GetComponent<Transform>();
                    cl_Camera.t_Follow = t_ClientTransform;
                }
            }
        }
    }

}
