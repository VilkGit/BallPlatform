using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEndGame : MonoBehaviour
{
    public UnityEvent OnTriggerEndGame = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ball>() != null)
        {
            OnTriggerEndGame?.Invoke();
        }
    }
}
