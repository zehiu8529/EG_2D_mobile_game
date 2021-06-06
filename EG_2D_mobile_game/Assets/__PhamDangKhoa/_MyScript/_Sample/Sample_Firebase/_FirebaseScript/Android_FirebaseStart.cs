//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseStart : MonoBehaviour
//{
//    /// <summary>
//    /// FIREBASE
//    /// </summary>
//    private Class_Firebase cl_Firebase;

//    /// <summary>
//    /// Text EMAIL Auth
//    /// </summary>
//    public Text t_EmailAuth;

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public GameObject g_StartButton;

//    /// <summary>
//    /// Text INFO
//    /// </summary>
//    public Text t_Info;

//    /// <summary>
//    /// Button LOG OUT
//    /// </summary>
//    public GameObject g_ButtonLogOut;

//    private void Start()
//    {
//        cl_Firebase = new Class_Firebase();

//        g_StartButton.SetActive(false);
//        g_ButtonLogOut.SetActive(false);

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cl_Firebase.Get_FirebaseAuth_Login())
//        //If Auth LOGIN Success
//        {
//            t_EmailAuth.text = cl_Firebase.Get_FirebaseAuth_Email().ToUpper();

//            g_ButtonLogOut.SetActive(true);
//            //Active Button LOG OUT

//            if (!cl_Firebase.Get_FirebaseAuth_EmailVerification_Check())
//            //If EMAIL VERIFICATION not check yet
//            {
//                t_Info.text = "Waiting for Email Verification check";

//                g_StartButton.SetActive(false);
//                //De-Active Button START
//            }
//            else
//            //If EMAIL VERIFICATION check Success
//            {
//                t_Info.text = "Click \"START\" to continue";

//                g_StartButton.SetActive(true);
//                //Active Button START
//            }
//        }
//        else
//        //If Auth not LOGIN yet
//        {
//            t_EmailAuth.text = "Unknown".ToUpper();

//            t_Info.text = "Click \"LOGIN\" or \"CREATE\" to continue";

//            g_StartButton.SetActive(false);
//            //De-Active Button START
//            g_ButtonLogOut.SetActive(false);
//            //De-Active Button LOG OUT
//        }
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseStart: OnDestroy");
//    }

//    //Create

//    /// <summary>
//    /// Scene CREATE AUTH
//    /// </summary>
//    public string s_SceneCreate = "Android_FirebaseCreate";

//    /// <summary>
//    /// Button CREATE
//    /// </summary>
//    public void Button_Create()
//    {
//        Class_Scene cl_Scene = new Class_Scene(s_SceneCreate);
//        //Chance Scene to "Create"
//    }

//    //Login

//    /// <summary>
//    /// Scene LOGIN Auth
//    /// </summary>
//    public string s_SceneLogin = "Android_FirebaseLogin";

//    /// <summary>
//    /// Button LOGIN
//    /// </summary>
//    public void Button_Login()
//    {
//        Class_Scene cl_Scene = new Class_Scene(s_SceneLogin);
//        //Chance Scene to "Login"
//    }

//    //Log out

//    /// <summary>
//    /// Button LOG OUT
//    /// </summary>
//    public void Button_LogOut()
//    {
//        cl_Firebase.Set_FirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase
//    }

//    //Info

//    /// <summary>
//    /// Scene INFO
//    /// </summary>
//    public string s_SceneInfo = "Android_FirebaseInfo";

//    /// <summary>
//    /// Button INFO
//    /// </summary>
//    public void Button_Info()
//    {
//        Class_Scene cl_Scene = new Class_Scene(s_SceneInfo);
//        //Chance Scene to "Login"
//    }

//    //Start

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public string s_SceneStart = "";

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public void Button_Start()
//    {
//        Class_Scene cl_Scene = new Class_Scene(s_SceneStart);
//        //Chance Scene to "Login"
//    }

//    //Exit

//    /// <summary>
//    /// Button EXIT
//    /// </summary>
//    public void Button_Exit()
//    {
//        cl_Firebase.Set_FirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        Application.Quit();
//    }
//}
