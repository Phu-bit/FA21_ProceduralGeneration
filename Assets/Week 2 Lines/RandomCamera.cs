using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;

    void Start()
    {
        
        cam.backgroundColor = UnityEngine.Random.ColorHSV();

        moveobject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveobject()
    {
        //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5)); //will get the middle of the screen

        Vector3 screenPosition = new Vector3(Random.Range(0, 3), Random.Range(0, 4), Random.Range(-12, 0));
        Vector3 screenrotation = new Vector3(Random.Range(-90, 15), Random.Range(-45, 45), Random.Range(-60,60));
        transform.position = screenPosition;
        transform.localEulerAngles = screenrotation;
    }


}
