using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Timer timer = null;
    public Text textTimer = null;

    public UnityEvent OnStartGame = new UnityEvent();
    public UnityEvent OnEndGame = new UnityEvent();

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        timer.OnTikTimer.AddListener(ChenegeTime);
    }

    private void OnDisable()
    {
        timer.OnTikTimer.RemoveListener(ChenegeTime);
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
        Time.timeScale = 1;
        timer.StartTimer();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        OnEndGame?.Invoke();
        timer.StopTimer();
    }

    private void ChenegeTime(float time)
    {
        textTimer.text = Timer.ConvertTime(time);
    }
}
