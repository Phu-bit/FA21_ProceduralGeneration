using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    private int counter;
    public GameObject drawer;

    [SerializeField]
    private bool switch_counter;

    public bool switchCounter
    {
        get { return switch_counter; }
        set
        {
            if (counter % 5000 == 0)
            {
                switch_counter = true;
            }

        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1;
        Debug.Log(counter);
        if (counter % 5000 == 0)
        {
            moveobject();
            counter = 0;
        }

    }
    public void moveobject()
    {
        //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5)); //will get the middle of the screen

        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Random.Range( Camera.main.farClipPlane / 4, 2)));
        transform.position = screenPosition;
    }


}
