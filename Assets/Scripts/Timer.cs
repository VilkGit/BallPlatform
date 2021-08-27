using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public class UnityEventFloat : UnityEvent<float> { }

    public UnityEventFloat OnTikTimer = new UnityEventFloat();

    private Coroutine _timerCoroutine = null;
    private float time = 0;

    public void StartTimer()
    {
        if (_timerCoroutine == null)
        {
            _timerCoroutine = StartCoroutine(ActiveTimer());
        }
    }
    public void StopTimer()
    {
        StopCoroutine(_timerCoroutine);
        time = 0;
    }

    public static string ConvertTime(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string result = string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);

        return result;
    }

    private IEnumerator ActiveTimer()
    {
        while (true)
        {
            time += Time.deltaTime;
            OnTikTimer?.Invoke(time);
            yield return new WaitForEndOfFrame();
        }
    }

}
