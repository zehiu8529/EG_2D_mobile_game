using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public KeyCode K_Pause = KeyCode.Escape;

    private bool pause = false;

    public GameObject pauseUI;

    public GameObject pauseUIMenuOptions;

    private void Awake()
    {
        
        pauseUI.SetActive(true);
        pauseUIMenuOptions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // bật tắt mang hình pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            pause = !pause;

        }
        if (!pause)
        {
            // trả lại thời gian bình thường
            pauseUIMenuOptions.SetActive(false);
            pauseUI.SetActive(false);

        }

    }

    public void Resume()
    {
        // tạm con mẹ nó dừng kết thúc 
        pause = false;
        pauseUI.SetActive(false);
        pauseUIMenuOptions.SetActive(false);

    }

    public void Options()
    {

        pauseUI.SetActive(false);
        pauseUIMenuOptions.SetActive(true);

    }

    public void Restart()
    {

        // ân dô sẻ về lại cái màng vừa chơi 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetString("playercm", "Playgame");
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
