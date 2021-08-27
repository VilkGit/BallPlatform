using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private Rigidbody _body = null;
    private Quaternion _targetRotation = new Quaternion();

    private void Start()
    {
        this._body = GetComponent<Rigidbody>();
    }
    public void MoveRotation(Quaternion rotation)
    {
        _targetRotation = rotation;
        this._body.MoveRotation(rotation);
    }
    public void MoveRotationOneAxis(float value, SnapAxis axis)
    {
        Vector3 newRotation = _targetRotation.eulerAngles;

        switch (axis)
        {
            case SnapAxis.X:
                newRotation.x = value;
                break;
            case SnapAxis.Y:
                newRotation.y = value;
                break;
            case SnapAxis.Z:
                newRotation.z = value;
                break;
        }
        
        _targetRotation = Quaternion.Euler(newRotation);
        this._body.MoveRotation(_targetRotation);
    }
}
