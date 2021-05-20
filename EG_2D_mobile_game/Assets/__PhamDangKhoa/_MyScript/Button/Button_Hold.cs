using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Hold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	/// <summary>
	/// Allow Reset Time for Stop Hold Pressed?
	/// </summary>
	[Header("Event After Hold Time")]
	[SerializeField]
	private bool b_AfterHoldTime = false;

	/// <summary>
	/// Hold Time Required
	/// </summary>
	[SerializeField]
	private float f_HoldTime = 1f;

	[Header("Event Instanlly")]
	[SerializeField]
	private Color c_Color_NotHold = Color.white;

	[SerializeField]
	private Color c_Color_Hold = Color.red;

	/// <summary>
	/// Unity Long Click Event Handle
	/// </summary>
	[Header("Event")]
	[SerializeField]
	private UnityEvent Event_HoldClick;

	/// <summary>
	/// Check if Hold Pressed?
	/// </summary>
	private bool b_HoldDown;

	/// <summary>
	/// Time Count in Hold Pressed
	/// </summary>
	private float f_HoldTime_Cur;

	private SpriteRenderer sp_Renderer;
	private Image i_Image;

	private void Start()
    {
		sp_Renderer = GetComponent<SpriteRenderer>();
		i_Image = GetComponent<Image>();
	}

    private void Update()
	{
		if (b_HoldDown)
		//If Hold Pressed >> Do...
		{
			Debug.Log("ButtonHold!");

            if (b_AfterHoldTime)
			//If Need Time to do Event >> Do...
            {
				f_HoldTime_Cur -= Time.deltaTime;

				if (f_HoldTime_Cur < 0)
				//If out of Time Hold >> Do Event
				{
					Set_Event_HoldClick();
					Set_OnReset();
				}
			}
			else
			//If NOT Need Time to do Event >> Do Event Right away
            {
				if (sp_Renderer != null)
                {
					sp_Renderer.color = c_Color_Hold;
				}

				if (i_Image != null) 
                {
					i_Image.color = c_Color_Hold;
				}
				
				Set_Event_HoldClick();
			}


		}
        else
		//If Not Hold Pressed >> Do...
		{
			if (b_AfterHoldTime)
			//If Need Time to do Event >> Do...
			{

			}
			else
			//If NOT Need Time to do Event >> Do Event Right away
			{
				if (sp_Renderer != null)
                {
					sp_Renderer.color = c_Color_NotHold;
				}

				if (i_Image != null)
                {
					i_Image.color = c_Color_NotHold;
				}
			}
		}
	}

	/// <summary>
	/// Do Hold Click Event
	/// </summary>
	private void Set_Event_HoldClick()
    {
		if (Event_HoldClick != null)
			Event_HoldClick.Invoke();
	}

	/// <summary>
	/// Reset and End Time count for Hold Pressed
	/// </summary>
	private void Set_OnReset()
	{
		b_HoldDown = false;
		f_HoldTime_Cur = 0;

		//Debug.Log("Set_OnReset");
	}

	/// <summary>
	/// Check if Button Hold Pressed?
	/// </summary>
	/// <returns></returns>
	public bool Get_Button_Hold()
    {
		return b_HoldDown;
	}

	/// <summary>
	/// Event Handle In "IPointerDownHandler"
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerDown(PointerEventData eventData)
	{
		b_HoldDown = true;

		//Debug.Log("Set_OnHoldDown");
	}

	/// <summary>
	/// Event Handle In "IPointerUpHandler"
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerUp(PointerEventData eventData)
	{
		Set_OnReset();

		//Debug.Log("Set_OnHoldUp");
	}
}