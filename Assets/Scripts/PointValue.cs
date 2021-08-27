using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointValue : MonoBehaviour
{
    [System.Serializable] public class UnityEventFloat : UnityEvent<double> { }

    public GameObject point1 = null;
    public GameObject point2 = null;

    public СontrollerCircle controllerCircle = null;

    public Vector2 diapazon = Vector2.zero;

    public UnityEventFloat onChengeValue = null;

    private void Start()
    {
        controllerCircle.onTouchDown.AddListener(ChengeValue);
        controllerCircle.onTouchStationary.AddListener(ChengeValue);
        controllerCircle.onTouchMove.AddListener(ChengeValue);
        controllerCircle.onTouchUp.AddListener(ChengeValue);
    }

    private void OnDisable()
    {
        controllerCircle.onTouchDown.RemoveListener(ChengeValue);
        controllerCircle.onTouchStationary.RemoveListener(ChengeValue);
        controllerCircle.onTouchMove.RemoveListener(ChengeValue);
        controllerCircle.onTouchUp.RemoveListener(ChengeValue);
    }

    private void ChengeValue(Touch touch)
    {
         ChengeValue(controllerCircle.point.transform.position);
    }
    private void ChengeValue(Vector3 pos)
    {
        Vector3 p1 = point1.transform.position;
        Vector3 p2 = point2.transform.position;

        Vector3 distanceVector = (p2 - p1);
        Vector3 distanceVectorNormal = distanceVector.normalized;
        float scalePoint = Vector3.Dot(distanceVectorNormal, pos);
        float scaleP1 = Vector3.Dot(distanceVectorNormal, p1);
        Vector3 posOnLine = distanceVectorNormal * (scalePoint - scaleP1);

        
        double distance = distanceVector.magnitude;
        double value = diapazon.y - diapazon.x;
        double result = value / distance;
        double resultValue = result * posOnLine.magnitude + diapazon.x;
        

        onChengeValue.Invoke(resultValue);

    }


}
