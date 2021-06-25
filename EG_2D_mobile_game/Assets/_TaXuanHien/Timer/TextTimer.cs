using TMPro;
using UnityEngine;

public class TextTimer : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private char characterSpliter = ':';

    [Header("Logic")]
    private float timer;
    private bool isActive;

    public GameObject Lose;

    public void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            UpdateText();
        }
    }
    private void UpdateText()
    {

        // Get the amount time since start
        float seconds = (timer % 60);
        float minutes = ((int)(timer / 60) % 60);
        float hours = (int)(timer / 3600);
        text.text = hours.ToString("00") + characterSpliter + minutes.ToString("00") + characterSpliter + seconds.ToString("00");
        Debug.Log((int)timer);
        Wingame();
    }

    public float TimeLose;

    private void Wingame()
    {
        if ((int)timer == TimeLose)
        {
            Lose.SetActive(true);
            isActive = !isActive;
        }
    }

    public void StartTimer()
    {
        StartTimer(0);
    }
    public void StartTimer(float seconds)
    {
        isActive = true;
        timer = seconds;
        UpdateText();
    }


    //public void AddTime(float seconds)
    //{
    //    timer += seconds ;
    //    
    //    UpdateText();
    //}

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
