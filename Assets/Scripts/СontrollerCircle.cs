using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class СontrollerCircle : MonoBehaviour
{
    [System.Serializable] public class UnityEventTouch : UnityEvent<Touch> { }
    public GameObject point = null;
    public GameObject canvas = null;

    public UnityEventTouch onTouchDown = new UnityEventTouch();
    public UnityEventTouch onTouchStationary = new UnityEventTouch();
    public UnityEventTouch onTouchMove = new UnityEventTouch();
    public UnityEventTouch onTouchUp = new UnityEventTouch();
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private bool _isThisSelect = false;
    private int _selectFingerId = 0;

    void Start()
    {
        
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        
        m_EventSystem = GetComponent<EventSystem>();
    }
    private void OnEnable()
    {
        onTouchDown.AddListener(MovePoint);
        onTouchMove.AddListener(MovePoint);
        onTouchUp.AddListener(MovePoint);
    }
    private void OnDisable()
    {
        onTouchDown.RemoveListener(MovePoint);
        onTouchMove.RemoveListener(MovePoint);
        onTouchUp.RemoveListener(MovePoint);
    }

    private void MovePoint(Touch touch)
    {

        if (touch.phase != TouchPhase.Ended)
        {
            MovePoint(touch.position);

        }
        else
        {

            MovePoint(transform.position);
        }
    }

    private void Update()
    {
        CheckTouch();
    }

    private void MovePoint(Vector3 touchPosition)
    {
        float radius = GetComponent<Image>().preferredWidth / 2;
        radius = (radius * 100f / 1080f) / 100f;
        radius = radius * Screen.width;
        Vector3 pos = touchPosition - transform.position;
        if (pos.magnitude > radius)
        {
            pos = pos.normalized * radius;
        }

        point.transform.position = pos + transform.position;

    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];
                if (touch.phase == TouchPhase.Began)
                {
                    m_PointerEventData = new PointerEventData(m_EventSystem);
                    m_PointerEventData.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_Raycaster.Raycast(m_PointerEventData, results);

                    for (int j = 0; j < results.Count; j++)
                    {
                        if (results[j].gameObject == gameObject)
                        {
                            _isThisSelect = true;
                            _selectFingerId = touch.fingerId;
                            onTouchDown.Invoke(touch);
                            break;
                        }
                    }

                }
                else if (touch.phase == TouchPhase.Stationary && _isThisSelect)
                {
                    onTouchStationary.Invoke(touch);
                }
                else if (touch.phase == TouchPhase.Moved && _isThisSelect)
                {
                    if (touch.fingerId == _selectFingerId)
                    {
                        onTouchMove.Invoke(touch);

                    }

                }
                else if (touch.phase == TouchPhase.Ended && _isThisSelect)
                {
                    if (touch.fingerId == _selectFingerId)
                    {
                        onTouchUp.Invoke(touch);
                        _isThisSelect = false;
                    }

                }
            }
        }
        else
        {
            Touch touch = new Touch();
            touch.fingerId = -1;
            touch.position = Input.mousePosition;
            touch.phase = TouchPhase.Stationary;

            if (Input.GetMouseButtonDown(0))
            {
                touch.phase = TouchPhase.Began;
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);

                for (int j = 0; j < results.Count; j++)
                {
                    if (results[j].gameObject == gameObject)
                    {
                        onTouchDown.Invoke(touch);
                        _isThisSelect = true;
                        break;
                    }
                }

            }

            else if (Input.GetMouseButton(0) && _isThisSelect)
            {
                touch.phase = TouchPhase.Moved;
                onTouchMove.Invoke(touch);

            }
            else if (Input.GetMouseButtonUp(0) && _isThisSelect)
            {
                touch.phase = TouchPhase.Ended;
                onTouchMove.Invoke(touch);
                _isThisSelect = false;

            }
            else if (touch.phase == TouchPhase.Stationary && _isThisSelect)
            {
                onTouchStationary.Invoke(touch);
            }
        }
    }

}
