using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on AI Machine Learning Primary
/// </summary>
public class Class_AIML
//AI Machine Learning Primary
{
	public Class_AIML()
	{

	}

	//---------------------------------------------------------------------------

	/// <summary>
	/// Number of Layer in Neural (Input - Hidden - Output) (*)
	/// </summary>
	private int i_LayerCount = 2;

	/// <summary>
	/// List of Neural in Layer (gồm Input - Hidden - Output) (*)
	/// </summary>
	private List<List<float>> li2_Activation;

	/// <summary>
	/// Number of Neural in Layer (gồm Input - Hidden - Output) (0)
	/// </summary>
	private List<int> li_NeuralCount;

	/// <summary>
	/// List of Weight[L][L-1] Layer L (gồm Hidden - Output) (-1)
	/// </summary>
	private List<List<List<float>>> li3_Weight;

	/// <summary>
	/// List of Bias Layer (gồm Input - Hidden) (-1)
	/// </summary>
	private List<float> li_Bias;

	/// <summary>
	/// List of Sum Layer (gồm Hidden - Output) (-1)
	/// </summary>
	private List<List<float>> li2_Sum;

	/// <summary>
	/// List of Error Layer (gồm Hidden - Output) (-1)
	/// </summary>
	private List<List<float>> li2_Error;

	/// <summary>
	/// List of Neural Output wanted
	/// </summary>
	private List<float> li_Desired;

	////Error before BackPropagation
	//private float f_ErrorTotal = 0;

	////Count on run FeedForward và BackPropagation
	//private int i_LoopLearning = 0;

	/// <summary>
	/// Save Imformation
	/// </summary>
	private List<float> li_Comment_dou = new List<float>();
	/// <summary>
	/// Save Imformation
	/// </summary>
	private List<string> li_Comment_str = new List<string>();

	/// <summary>
	/// Reset Neural Network
	/// </summary>
	public void Set_Reset()
	{
		li_NeuralCount = new List<int>();
		li2_Activation = new List<List<float>>();
		li3_Weight = new List<List<List<float>>>();
		li_Bias = new List<float>();
		li2_Sum = new List<List<float>>();
		li2_Error = new List<List<float>>();
		li_Desired = new List<float>();
	}

	//---------------------------------------------------------------------------- Set / Get

	/// <summary>
	/// Set Number of Layer
	/// </summary>
	/// <param name="LayerCount"></param>
	public void Set_LayerCount(int LayerCount)
	{
		PlayerPrefs.SetInt("LC", (LayerCount < 0) ? 2 : LayerCount);
	}

	/// <summary>
	/// Get Number of Layer
	/// </summary>
	/// <returns></returns>
	public int Get_LayerCount()
    {
		return i_LayerCount;
    }

	/// <summary>
	/// Set new Number of Neural Layer
	/// </summary>
	/// <param name="Layer"></param>
	/// <param name="NeuralCount"></param>
	public void Set_NeuralCount(int Layer, int NeuralCount)
	{
		if (Layer >= 0)
			PlayerPrefs.SetInt("NC_" + Layer.ToString(), (NeuralCount > 0) ? NeuralCount : 0);
	}

	/// <summary>
	/// Get Number of Neural of Layer
	/// </summary>
	/// <param name="Layer"></param>
	/// <returns></returns>
	public int Get_NeuralCount(int Layer)
    {
		return li_NeuralCount[Layer];
    }

	/// <summary>
	/// Start create new Neural Network
	/// </summary>
	/// <param name="RandomNumber">If "True", Weight & Bias will gain random value</param>
	public void Set_NeuralNetworkCreate(bool RandomNumber)
	{
		//LayerCount
		i_LayerCount = PlayerPrefs.GetInt("LC");

		//NeuralCount
		li_NeuralCount = new List<int>();
		for (int lay = 0; lay < i_LayerCount; lay++)
		{
			li_NeuralCount.Add(PlayerPrefs.GetInt("NC_" + lay.ToString()));
		}

		//Activation
		li2_Activation = new List<List<float>>();
		for (int lay = 0; lay < i_LayerCount; lay++)
		{
			this.li2_Activation.Add(new List<float> { });
			for (int neu = 0; neu < li_NeuralCount[lay]; neu++)
			{
				li2_Activation[lay].Add(0);
			}
		}

		//int i_o = 0;
		//Weight
		li3_Weight = new List<List<List<float>>>();
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			this.li3_Weight.Add(new List<List<float>> { });
			for (int neuY = 0; neuY < this.li_NeuralCount[lay + 1]; neuY++)
			{
				this.li3_Weight[lay].Add(new List<float> { });
				for (int neuX = 0; neuX < this.li_NeuralCount[lay]; neuX++)
				{
					//i_o++;
					if (RandomNumber)
					{
						System.Random Rand = new System.Random();
						float Value =
							(float)(
							(i_LayerCount * Rand.Next(1, 500) + li_NeuralCount[lay] *
							Rand.Next(500, 1000) + neuX * Rand.Next(100, 200) + neuY *
							Rand.Next(200, 300) + lay * Rand.Next(300, 400)) * Rand.Next(1, 50) / 100000.0) / 100.0f;
						this.li3_Weight[lay][neuY].Add(Value);
					}
					else
					{
						this.li3_Weight[lay][neuY].Add(0.0f);
						//Debug.Log("Set_NeuralNetworkCreate: " lay + " " + neuY + " " + neuX + " : " + this.li3_Weight[lay][neuY][neuX]);
					}

				}
			}
		}

		//Bias
		li_Bias = new List<float>();
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			if (RandomNumber)
			{
				System.Random Rand = new System.Random();
				float Value = 0;
				this.li_Bias.Add(Value);
			}
			else
			{
				this.li_Bias.Add(0.0f);
			}
		}

		//Sum
		//Debug.Log("Sum");
		li2_Sum = new List<List<float>>();
		for (int lay = 1; lay < i_LayerCount; lay++)
		{
			this.li2_Sum.Add(new List<float> { });
			for (int neu = 0; neu < this.li_NeuralCount[lay]; neu++)
			{
				//Debug.Log(lay + " " + neu);
				this.li2_Sum[lay - 1].Add(0.0f);
			}
		}

		//Error
		li2_Error = new List<List<float>>();
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			this.li2_Error.Add(new List<float> { });
			for (int neu = 0; neu < this.li_NeuralCount[lay + 1]; neu++)
			{
				this.li2_Error[lay].Add(0.0f);
			}
		}

		//Desired
		li_Desired = new List<float>();
		for (int neu = 0; neu < this.li_NeuralCount[i_LayerCount - 1]; neu++) 
		{
			this.li_Desired.Add(0.0f);
		}
	}

	/// <summary>
	/// Set new Bias in Layer
	/// </summary>
	/// <param name="i_Layer"></param>
	/// <param name="f_Bias"></param>
	public void Set_Bias(int i_Layer, float f_Bias)
	{
		if (i_Layer < i_LayerCount && i_Layer >= 0)
			this.li_Bias[i_Layer] = f_Bias;
	}

	/// <summary>
	/// Set new Weight with X (L-1) << Y (L)
	/// </summary>
	/// <param name="i_Layer">L</param>
	/// <param name="i_NeuralY">Y (L)</param>
	/// <param name="i_NeuralX">X (L-1)</param>
	/// <param name="f_Weight"></param>
	public void Set_Weight(int i_Layer, int i_NeuralY, int i_NeuralX, float f_Weight)
	{
		if (i_Layer < i_LayerCount - 1 && i_Layer >= 0)
			this.li3_Weight[i_Layer][i_NeuralY][i_NeuralX] = f_Weight;
	}

	/// <summary>
	/// Set new Input
	/// </summary>
	/// <param name="i_Neu_Input"></param>
	/// <param name="f_ValueInput"></param>
	public void Set_Input(int i_Neu_Input, float f_ValueInput)
	{
		if (i_Neu_Input >= 0 && i_Neu_Input < li_NeuralCount[0])
			this.li2_Activation[0][i_Neu_Input] = f_ValueInput;
	}

	/// <summary>
	/// Set new Input
	/// </summary>
	/// <param name="l_ListInput"></param>
	public void Set_Input(List<float> l_ListInput)
    {
		if (l_ListInput == null)
			return;

		if(li2_Activation[0].Count == l_ListInput.Count)
			//Gán List of nếu độ dài List of bằng nhau
			li2_Activation[0] = l_ListInput;
        else
        {
			//Nếu độ dài không List of không bằng nhau >> Gán from đầu đến cuối from vị trí 0
			for (int i = 0; i < l_ListInput.Count; i++)
			{
				li2_Activation[0][i] = l_ListInput[i];
			}
		}
    }

	/// <summary>
	/// Set Input
	/// </summary>
	/// <param name="ListInput"></param>
	/// <param name="i_SetFrom"></param>
	public void Set_Input(List<float> ListInput, int i_SetFrom)
    {
		if (ListInput == null)
			return;

		for (int i = 0; i < ListInput.Count; i++) 
        {
			//Gán List of bắt đầu from SetFrom
			li2_Activation[0][i + i_SetFrom] = ListInput[i];
		}
	}

	/// <summary>
	/// Get List of Neural of Input
	/// </summary>
	/// <returns></returns>
	public List<float> Get_Input()
	{
		return li2_Activation[0];
	}

	/// <summary>
	/// Get Neural from Input
	/// </summary>
	/// <param name="i_Neural"></param>
	/// <returns></returns>
	public float Get_Input(int i_Neural)
    {
		return li2_Activation[0][i_Neural];
    }

	/// <summary>
	/// Set new Output Desired
	/// </summary>
	/// <param name="i_Neu_Desired"></param>
	/// <param name="i_ValueDesired"></param>
	public void Set_Desired(int i_Neu_Desired, float i_ValueDesired)
	{
		if (i_Neu_Desired >= 0 && i_Neu_Desired < li_NeuralCount[i_LayerCount - 1])
			this.li_Desired[i_Neu_Desired] = i_ValueDesired;
	}

	/// <summary>
	/// Get Output Desired
	/// </summary>
	/// <returns></returns>
	public List<float> Get_Desired()
	{
		return li_Desired;
	}

	/// <summary>
	/// Get List of Neural of Output
	/// </summary>
	/// <returns></returns>
	public List<float> Get_Output()
	{
		return li2_Activation[i_LayerCount - 1];
	}

	/// <summary>
	/// Get Neural of Output
	/// </summary>
	/// <param name="i_Neural"></param>
	/// <returns></returns>
	public float Get_Output(int i_Neural)
    {
		return li2_Activation[i_LayerCount - 1][i_Neural];
	}

	//Get Error Total sau khi chạy FeedForward và trước khi chạy BackPropagation
	//public float BrainGet_ErrorTotal()
	//{
	//	return f_ErrorTotal;
	//}

	//Get số lần đã chạy BackPropagation
	//public int BrainGet_LoopLearned()
	//{
	//	return i_LoopLearning;
	//}

	//---------------------------------------------------------------------------- File

	/// <summary>
	/// Check AIML File Exist
	/// </summary>
	/// <param name="s_Link"></param>
	/// <returns></returns>
	public bool File_Check(string s_Link)
    {
		Class_FileIO myFile = new Class_FileIO();
		return myFile.Get_FileExist(s_Link);
    }

	/// <summary>
	/// Save Current AIML Data to File work on this Script
	/// </summary>
	/// <param name="s_Link"></param>
	public void File_Save(string s_Link)
	{
		Class_FileIO myFile = new Class_FileIO();

		myFile.FileSet_Write_Add("LayerCount:");
		myFile.FileSet_Write_Add(this.i_LayerCount);

		myFile.FileSet_Write_Add("NeuralCount:");
		for (int lay = 0; lay < i_LayerCount; lay++)
		{
			myFile.FileSet_Write_Add(this.li_NeuralCount[lay]);
			//Lưu Number of Neural of  Layer
		}

		myFile.FileSet_Write_Add("Bias:");
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			myFile.FileSet_Write_Add(this.li_Bias[lay]);
			//Lưu Bias of  Layer
		}

		myFile.FileSet_Write_Add("Weight:");
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			//Xét fromng Layer (L-1)
			for (int neuY = 0; neuY < li_NeuralCount[lay + 1]; neuY++)
			{
				//Xét fromng Neural Y of Layer (L)
				for (int neuX = 0; neuX < li_NeuralCount[lay]; neuX++)
				{
					//Xét fromng Neural X of Layer (L-1)
					myFile.FileSet_Write_Add(this.li3_Weight[lay][neuY][neuX]);
					//Lưu Weight of  Layer
				}
			}
		}

		//for (int neu = 0; neu < li_NeuralCount[0]; neu++)
		//{
		//	myFile.FileSet_Write_Add(BrainGet_Input()[neu]);
		//	//Lưu Input
		//}

		//for (int neu = 0; neu < li_NeuralCount[i_LayerCount - 1]; neu++)
		//{
		//	myFile.FileSet_Write_Add(BrainGet_Desired()[neu]);
		//	//Lưu Desired
		//}

		//myFile.FileSet_Write_Add(BrainGet_ErrorTotal());
		////Lưu Error Total

		//myFile.FileSet_Write_Add(BrainGet_LoopLearned());
		////Lưu Loop Learning

		myFile.FileSet_Write_Add("Comment:");
		myFile.FileSet_Write_Add(li_Comment_dou.Count);
		//myFile.FileWrite(li_Comment_str.Count);
		//two List of này có cùng Number of phần tử
		for (int i = 0; i < li_Comment_dou.Count; i++)
		{
			myFile.FileSet_Write_Add(li_Comment_str[i]);
			myFile.FileSet_Write_Add(li_Comment_dou[i]);
			//Lưu Imformation
		}

		//Kích hoạt lưu File
		myFile.FileActive_Write_Start(s_Link);
	}

	/// <summary>
	/// Read AIML Data from File work on this Script
	/// </summary>
	/// <param name="s_Link"></param>
	public void File_Open(string s_Link)
	{
		Class_FileIO myFile = new Class_FileIO();

		//Kích hoạt đọc File
		myFile.FileActive_Read_Start(s_Link);

		string t;

		t = myFile.FileGet_Read_Auto_String();
		int LayerCount = myFile.FileGet_Read_Auto_Int();
		Set_LayerCount(LayerCount);

		t = myFile.FileGet_Read_Auto_String();
		for (int lay = 0; lay < LayerCount; lay++)
		{
			Set_NeuralCount(lay, myFile.FileGet_Read_Auto_Int());
			//Ghi Number of Neural of  Layer
		}

		//Debug.Log("Create");
		Set_NeuralNetworkCreate(false);
		//Debug.Log("Weight");

		t = myFile.FileGet_Read_Auto_String();
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			Set_Bias(lay, myFile.FileGet_Read_Auto_Float());
			//Ghi Bias of  Layer
		}

		t = myFile.FileGet_Read_Auto_String();
		for (int lay = 0; lay < i_LayerCount - 1; lay++)
		{
			//Xét fromng Layer (L-1)
			for (int neuY = 0; neuY < li_NeuralCount[lay + 1]; neuY++)
			{
				//Xét fromng Neural Y of Layer (L)
				for (int neuX = 0; neuX < li_NeuralCount[lay]; neuX++)
				{
					//Xét fromng Neural X of Layer (L-1)
					//Debug.Log(lay + " " + neuY + " " + neuX + " : " + this.li3_Weight[lay][neuY][neuX]);
					//this.li3_Weight[lay][neuY][neuX] = myFile.FileGet_Read_Auto_Float();
					Set_Weight(lay, neuY, neuX, myFile.FileGet_Read_Auto_Float());
					//Ghi Weight of  Layer
				}
			}
		}

		//for (int neu = 0; neu < li_NeuralCount[0]; neu++)
		//{
		//	BrainSet_Input(neu, myFile.FileGet_Read_Auto_Float());
		//	//Ghi Input
		//}

		//for (int neu = 0; neu < li_NeuralCount[i_LayerCount - 1]; neu++)
		//{
		//	BrainSet_Desired(neu, myFile.FileGet_Read_Auto_Float());
		//	//Ghi Desired
		//}

		//this.f_ErrorTotal = myFile.FileGet_Read_Auto_Float();
		////Ghi Error Total

		//this.i_LoopLearning = myFile.FileGet_Read_Auto_Int();
		////Ghi Loop Learning

		t = myFile.FileGet_Read_Auto_String();
		int CommentCount = myFile.FileGet_Read_Auto_Int();
		//myFile.FileReader(li_Comment_str.Count);
		//two List of này có cùng Number of phần tử
		for (int i = 0; i < CommentCount; i++)
		{
			li_Comment_str.Add(myFile.FileGet_Read_Auto_String());
			li_Comment_dou.Add(myFile.FileGet_Read_Auto_Float());
			//Ghi Imformation
		}
	}

	//---------------------------------------------------------------------------- FeedForward

	/// <summary>
	/// Caculate Sum of between two Layer X (L-1) >> Y (L)
	/// </summary>
	/// <param name="i_Layer"></param>
	private void Set_FeedForward_Sum(int i_Layer)
	{
		//Debug.Log("After");
		for (int neuY = 0; neuY < li_NeuralCount[i_Layer]; neuY++)
		{
			//Xét Layer Y (L)

			//Sum = Weight * li_Activation(L-1) + Bias
			//Debug.Log(Layer + " " + neuY + " ");
			li2_Sum[i_Layer - 1][neuY] = li_Bias[i_Layer - 1];

			for (int neuX = 0; neuX < li_NeuralCount[i_Layer - 1]; neuX++)
			{
				//Xét Layer X (L-1)
				li2_Sum[i_Layer - 1][neuY] +=
					li3_Weight[i_Layer - 1][neuY][neuX] * li2_Activation[i_Layer - 1][neuX];
			}
		}
	}

	/// <summary>
	/// Caculate Sigmoid
	/// </summary>
	/// <param name="Value"></param>
	/// <returns></returns>
	public float Set_FeedForward_Sigmoid_Single(float Value)
	{
		return (float)1.0 / ((float)1.0 + (float)Math.Exp(-Value));
	}

	/// <summary>
	/// Caculate Sigmoid from Sum of Layer Y (L)
	/// </summary>
	/// <param name="Layer"></param>
	private void Set_FeedForward_Sigmoid(int Layer)
	{
		for (int neuY = 0; neuY < li_NeuralCount[Layer]; neuY++)
		{
			//Xét Layer Y (L)

			//li_Activation(L) = Sigmoid(Sum)
			li2_Activation[Layer][neuY] =
				Set_FeedForward_Sigmoid_Single(li2_Sum[Layer - 1][neuY]);
		}
	}

	//Caculate Error after FeedForward và before BackPropagation
	//private void Set_Error()
	//{
	//	f_ErrorTotal = 0;
	//	for (int neuY = 0; neuY < li_NeuralCount[i_LayerCount - 1]; neuY++)
	//	{
	//		float Delta = li_Desired[neuY] - li2_Activation[i_LayerCount - 1][neuY];
	//		f_ErrorTotal += (float)0.5 * Delta * Delta;
	//	}
	//}

	/// <summary>
	/// Active FeedForward
	/// </summary>
	public void Set_FeedForward()
    {
        for (int lay = 1; lay < i_LayerCount; lay++)
        {
            Set_FeedForward_Sum(lay);
            Set_FeedForward_Sigmoid(lay);
        }
        //Set_Error();
    }

	//---------------------------------------------------------------------------- BackPropagation

	/// <summary>
	/// Caculate Error between Layer Output >> Desired
	/// </summary>
	private void Set_BackPropagation_ErrorOuput()
	{
		int layerY = i_LayerCount - 1;
		for (int neuY = 0; neuY < li_NeuralCount[layerY]; neuY++)
		{
			//Xét Layer Y (L) với Desired
			li2_Error[layerY - 1][neuY] =
				-(li_Desired[neuY] - li2_Activation[layerY][neuY]);
		}
	}

	/// <summary>
	/// Caculate Sigmoid
	/// </summary>
	/// <param name="f_Value"></param>
	/// <returns></returns>
	public float Set_BackPropagation_Sigmoid_Single(float f_Value)
	{
		float Sigmoid = Set_FeedForward_Sigmoid_Single(f_Value);
		return Sigmoid * ((float)1.0 - Sigmoid);
	}

	/// <summary>
	/// Caculate Error between Layer Y (L) >> Layer Z (L+1)
	/// </summary>
	/// <param name="i_Layer"></param>
	private void Set_BackPropagation_ErrorHidden(int i_Layer)
	{
		int layerZ = i_Layer + 1;
		int layerY = i_Layer;
		for (int neuY = 0; neuY < li_NeuralCount[layerY]; neuY++)
		{
			//Xét Layer Y (L) với Layer Z (L+1)
			//Debug.Log(Layer + " " + neuY);
			li2_Error[layerY - 1][neuY] = 0;

			for (int neu_Z = 0; neu_Z < li_NeuralCount[layerZ]; neu_Z++)
			{
				//Xét Layer Y (L) với Layer Z (L+1)
				li2_Error[layerY - 1][neuY] +=
					li2_Error[layerZ - 1][neu_Z] *
					Set_BackPropagation_Sigmoid_Single(li2_Sum[layerZ - 1][neu_Z]) *
					li3_Weight[layerZ - 1][neu_Z][neuY];
			}
		}
	}

	/// <summary>
	/// Set Weight Layer
	/// </summary>
	/// <param name="Layer"></param>
	private void Set_BackPropagation_Update(int Layer)
	{ 
		//Layer Output
		int layerX = Layer - 1;
		int layerY = Layer;
		for (int neuX = 0; neuX < li_NeuralCount[layerX]; neuX++)
		{
			//Xét Layer X (L-1) >> Layer Y (L)
			for (int neuY = 0; neuY < li_NeuralCount[layerY]; neuY++)
			{
				//Xét Layer X (L-1) >> Layer Y (L)
				li3_Weight[layerY - 1][neuY][neuX] -=
					(float)0.5 * (
						li2_Error[layerY - 1][neuY] *
						Set_BackPropagation_Sigmoid_Single(li2_Sum[layerY - 1][neuY]) *
						li2_Activation[layerX][neuX]);
			}
		}
	}

	/// <summary>
	/// Active BackPropagation
	/// </summary>
	public void Set_BackPropagation()
	{
		for (int lay = i_LayerCount - 1; lay > 0; lay--)
		{
			//Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

			//Caculate Error
			if (lay == i_LayerCount - 1)
			{
				Set_BackPropagation_ErrorOuput();
			}
			else
			{
				Set_BackPropagation_ErrorHidden(lay);
			}
		}

		for (int lay = i_LayerCount - 1; lay > 0; lay--)
		{
			//Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

			//Set
			Set_BackPropagation_Update(lay);
		}

		//i_LoopLearning++;
	}

	//---------------------------------------------------------------------------- Ghi chú

	/// <summary>
	/// Get Note Index if Exist
	/// </summary>
	/// <param name="CommentString"></param>
	/// <returns></returns>
	public int Get_CommentExist(string CommentString)
	{
		for (int i = 0; i < li_Comment_str.Count; i++)
		{
			if (li_Comment_str[i] == CommentString)
				return i;
		}
		return -1;
	}

	/// <summary>
	/// Set Note
	/// </summary>
	/// <param name="CommentString"></param>
	/// <param name="CommentNumber"></param>
	public void Set_Comment(String CommentString, float CommentNumber)
	{
		int Index = Get_CommentExist(CommentString);
		if (Index != -1)
		{
			li_Comment_dou[Index] = CommentNumber;
		}
		else
		{
			li_Comment_str.Add(CommentString);
			li_Comment_dou.Add(CommentNumber);
		}
	}

	/// <summary>
	/// Get Note
	/// </summary>
	/// <param name="CommentString"></param>
	/// <returns>If not found, it will return "int.MaxValue"</returns>
	public float Get_Comment(string CommentString)
	{
		int Index = Get_CommentExist(CommentString);
		if (Index != -1)
		{
			return li_Comment_dou[Index];
		}
		return int.MaxValue;
	}

	//---------------------------------------------------------------------------- Delay

	/// <summary>
	/// Delay Time
	/// </summary>
	private int i_BrainDelayTime = 3;
	private int i_BrainDelayTime_Cur = 0;

	/// <summary>
	/// Set Delay Time
	/// </summary>
	/// <param name="Value"></param>
	public void Set_myBrainDelayTime(int Value)
	{
		i_BrainDelayTime = Value;
	}

	/// <summary>
	/// Check Delay Time
	/// </summary>
	/// <returns>Will return "True" if Delay Time = 0</returns>
	public bool Get_myBrainDelayTime_Value()
	{
		if (i_BrainDelayTime_Cur > 0)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Check Delay Time & Continue work on Delay Time
	/// </summary>
	/// <returns>Will return "True" if Delay Time = 0</returns>
	public bool Get_myBrainDelayTime_Over()
	{
		if (i_BrainDelayTime_Cur > 0)
		{
			i_BrainDelayTime_Cur -= 1;
			return false;
		}
		i_BrainDelayTime_Cur = i_BrainDelayTime;
		return true;
	}

	//---------------------------------------------------------------------------- Debug
}