using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArmSave : MonoBehaviour
{
    public bool cosOrSin;
    private Vector3 initialRotation;
    public Vector3 initialAngle;
    private Vector3 angle;


    public float angleToAdd;
    public float offset;
    public float addOffset;
    public float speed;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
        angle = initialRotation;

    }

    // Update is called once per frame
    void Update()
    {
        angle.y += speed;
        angle.z += speed;
        angle.x += speed;

        if (cosOrSin)
        {

            initialAngle.z = Mathf.Sin(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);
            initialAngle.y = Mathf.Sin(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);
            initialAngle.x = Mathf.Sin(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);


        }
        else
        {
            initialAngle.x = Mathf.Cos(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);
            initialAngle.y = Mathf.Cos(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);
            initialAngle.z = Mathf.Cos(angle.z * offset + addOffset) * angleToAdd * (180 / Mathf.PI);

        }
        transform.localEulerAngles = initialAngle;
        //angle.z = initialAngle.z;

    }
}
