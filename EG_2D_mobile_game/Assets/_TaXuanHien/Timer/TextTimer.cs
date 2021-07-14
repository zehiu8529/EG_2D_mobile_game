using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTimer : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private char characterSpliter = ':';

    [Header("Logic")]
    float timer;
    private bool isActive;
    //[SerializeField] float starttimer = 0f ;

    public GameObject Lose;
    //public Slider slider;
    public GameObject Setting;
    public void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            UpdateText();
        }
    }
    private void UpdateText()
    {
        // Get the amount time since start
        int seconds = (int)timer % 60;
        int minutes = ((int)(timer / 60) % 60);
      //  int hours = (int)(timer / 3600);
      
        text.text = /*hours.ToString("00") + characterSpliter + */  minutes.ToString("00") + characterSpliter + seconds.ToString("00");
        Debug.Log((int)timer);
        if ((int)timer <= 0 )
        {
            Lose.SetActive(true);
            Setting.SetActive(false);
            isActive = !isActive;
        }
    }

    public void StartTimer(float seconds)
    {
    
        isActive = true;
        timer = seconds;
        UpdateText();
    }


    public void PauseTimer()
    {
        isActive = !isActive;
    }

    public void ResetTimer()
    {
        timer = 0;
        UpdateText();
    }
    public float GetTimeSinceStart()
    {
        return timer;
    }
}
