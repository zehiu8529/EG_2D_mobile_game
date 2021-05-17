//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthCreate : MonoBehaviour
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
//    /// Input Field RE-PASSWORD
//    /// </summary>
//    public InputField i_Password_Re;
//    /// <summary>
//    /// Input Field DISPLAY-NAME
//    /// </summary>
//    public InputField i_DisplayName;

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
//        i_Password_Re.inputType = InputField.InputType.Password;
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cl_Firebase.Get_FirebaseAuth_Login())
//        //If Auth LOGIN or CREATE Success
//        {
//            t_EmailAuth.text = cl_Firebase.Get_FirebaseAuth_Email().ToUpper();
//        }
//        else
//        //If Auth not LOGIN or CREATE yet
//        {
//            t_EmailAuth.text = "Unknown".ToUpper();
//        }

//        if (cl_Firebase.Get_FirebaseAuth_Register_Done())
//        {
//            t_Info.text = cl_Firebase.Get_FirebaseAuth_Message();
//            cl_Firebase.Set_FirebaseAuth_Register_Done(false);
//        } 
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseCreate: OnDestroy");
//    }

//    //Create

//    /// <summary>
//    /// Button CREATE
//    /// </summary>
//    public void Button_Create()
//    {
//        //cl_Firebase.Set_FirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        cl_Firebase.Set_FirebaseAuth_Message_Clear();

//        if(i_Email.text == "")
//        {
//            t_Info.text = "Email not allow emty";
//            return;
//        }

//        if (i_Password.text == "")
//        {
//            t_Info.text = "Password not allow emty";
//            return;
//        }

//        if (i_Password_Re.text == "")
//        {
//            t_Info.text = "Re-Password not same to Password";
//            return;
//        }

//        if(i_Password.text.Length < 6 || i_Password.text.Length > 12)
//        {
//            t_Info.text = "Password allow 6-12 Characters";
//            return;
//        }

//        if (i_DisplayName.text == "")
//        //If DisplayName emty >> DisplayName will "Newbie"
//        {
//            i_DisplayName.text = "Newbie";
//        }

//        i_DisplayName.text = i_DisplayName.text.ToUpper();

//        StartCoroutine(
//            cl_Firebase.Set_FirebaseAuth_Register_IEnumerator(
//                i_Email.text,
//                i_Password.text,
//                i_Password_Re.text,
//                i_DisplayName.text,
//                true,
//                new Android_FirebasePlayer_Data(i_DisplayName.text)));
//        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"

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
//        Application.Quit();
//    }
//}
