using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    float amount;
    //private Transform bar;

    public void setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }

    private void Update()
    {
        amount = healthSystem.GetPlayerHealthPercent();
        //Debug.Log(amount);
        transform.Find("Bar").localScale = new Vector3(amount, 1f);
    }
}
