using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGrow : MonoBehaviour
{

    //Fruit should be a prefab that already has this script attached. Place the Fruits where they need to be.

    private Vector3 endsize;
    private float timer = 0;
    public bool picked;

    void Start()
    {
        endsize = transform.localScale;
    }

    void Update()
    {
        /*if(transform.localScale == endsize)
        {
            //change color
            GetComponent<Renderer>().material.color = Color.red;
        }*/
    }

    void FixedUpdate()
    {
        if (picked && transform.localScale==endsize)    //pick a finished fruit
        {
            transform.localScale = Vector3.zero;
            timer = 0;
            picked = false;
        }
        else //failed to pick a growing fruit
        {
            picked = false;
        }

        //get bigger until max
        transform.localScale = Vector3.Lerp(Vector3.zero, endsize, timer);
        timer+=Time.deltaTime*.1f;
    }
}
