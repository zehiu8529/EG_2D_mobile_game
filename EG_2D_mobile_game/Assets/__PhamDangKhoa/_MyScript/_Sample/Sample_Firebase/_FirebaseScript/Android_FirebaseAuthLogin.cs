//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthLogin : MonoBehaviour
//{
//    /// <summary>
//    /// FIREBASE
//    /// </summary>
//    private Class_Firebase cl_Firebase;

//    /// <summary>
//    /// Input Field EMAIL
//    /// </summary>
//    public InputField i_Email;
//    /// <summary>
//    /// Input Field PASSWORD
//    /// </summary>
//    public InputField i_Password;

//    /// <summary>
//    /// Text EMAIL Auth
//    /// </summary>
//    public Text t_EmailAuth;

//    /// <summary>
//    /// Text INFO
//    /// </summary>
//    public Text t_Info;

//    /// <summary>
//    /// Scene START
//    /// </summary>
//    public string s_SceneBack = "Android_FirebaseStart";

//    private void Start()
//    {
//        cl_Firebase = new Class_Firebase();

//        i_Password.inputType = InputField.InputType.Password;
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cl_Firebase.Get_FirebaseAuth_Login())
//        //If Auth is LOGIN Sucess
//        {
//            t_EmailAuth.text = cl_Firebase.Get_FirebaseAuth_Email().ToUpper();
//        }
//        else
//        //If Auth not LOGIN yet
//        {
//            t_EmailAuth.text = "Unknown".ToUpper();
//        }

//        if (cl_Firebase.Get_FirebaseAuth_Login_Done())
//        {
//            t_Info.text = cl_Firebase.Get_FirebaseAuth_Message();
//            cl_Firebase.Set_FirebaseAuth_Register_Done(false);
//        }
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseLogin: OnDestroy");
//    }

//    //Login

//    /// <summary>
//    /// Button LOGIN
//    /// </summary>
//    public void Button_Login()
//    {
//        //cl_Firebase.Set_FirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        cl_Firebase.Set_FirebaseAuth_Message_Clear();

//        if (i_Email.text == "")
//        {
//            t_Info.text = "Email not allow emty";
//            return;
//        }

//        if (i_Password.text == "")
//        {
//            t_Info.text = "Password not allow emty";
//            return;
//        }

//        StartCoroutine(cl_Firebase.Set_FirebaseAuth_Login_IEnumerator(i_Email.text, i_Password.text));

//        t_Info.text = cl_Firebase.Get_FirebaseAuth_Message();
//    }

//    //Back

//    /// <summary>
//    /// Button BACK
//    /// </summary>
//    public void Button_Cancel()
//    {
//        Class_Scene cl_Scene = new Class_Scene(s_SceneBack);
//        //Chance Scene to "Back"
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
