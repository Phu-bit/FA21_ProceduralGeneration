using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinStuff1 : MonoBehaviour
{
    public Vector3 frequency, amplitude, offset;
    public bool move, rotate, size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(frequency.x * Time.time + offset.x) * amplitude.x;
        float y = Mathf.Sin(frequency.x * Time.time + offset.y) * amplitude.y;
        float z = Mathf.Sin(frequency.x * Time.time + offset.z) * amplitude.z;

        Vector3 sinVector = new Vector3(x * x / amplitude.x, y, z);

        if (move)
        {
            this.transform.localPosition = sinVector;
        }
        if (rotate)
            this.transform.localEulerAngles = sinVector;
        if (size)
            this.transform.localScale = sinVector;
    }
}
