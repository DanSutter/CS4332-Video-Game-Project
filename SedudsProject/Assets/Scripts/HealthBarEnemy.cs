using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : MonoBehaviour
{
    private HealthSystem healthSystem;
    float amount;

    public void setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }

    private void Update()
    {
        amount = healthSystem.GetNPCHealthPercent();
        //Debug.Log(amount);
        transform.Find("Bar").localScale = new Vector3(amount, 1f);
    }
}
