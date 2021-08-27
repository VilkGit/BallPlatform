using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField]
    private Platform platform = null;


    public void MoveRotationX(double value)
    {
        platform.MoveRotationOneAxis((float)value, SnapAxis.X);
    }
    public void MoveRotationZ(double value)
    {
        platform.MoveRotationOneAxis((float)value, SnapAxis.Z);
    }
}
