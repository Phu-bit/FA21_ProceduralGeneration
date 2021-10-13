using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{

    public bool worldSpace, isMoving, useMethods, localAxis, useSin;
    public Vector3 position, rotation, scale, move, rotate, size;
    public Vector3 frequency, amplitude, offeset;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (worldSpace)
            {
                this.transform.position = position;
                this.transform.eulerAngles = rotation;

            }
            else //localspace
            {
                this.transform.localPosition = position;
                this.transform.localEulerAngles = rotation;
                this.transform.localScale = scale;

            }

        }
        else
        {
            if (useMethods)
            {
                if (localAxis)
                {
                    this.transform.Translate(move);
                    this.transform.Rotate(rotate);

                }
                else
                {
                    this.transform.Translate(this.transform.forward * move.z);
                    this.transform.Rotate(rotate);
                }
            }
            else
            {
                float x = Mathf.Sin(frequency.x * Time.deltaTime ) * amplitude.x;
                float y = Mathf.Sin(frequency.x * Time.deltaTime ) * amplitude.y;
                float z = Mathf.Sin(frequency.x * Time.deltaTime ) * amplitude.z;

                Vector3 sinVector = new Vector3(x, y, z);
                this.transform.localPosition += move;
                this.transform.localEulerAngles += rotate;
                this.transform.localScale += size;
            }
        }

    }
}
