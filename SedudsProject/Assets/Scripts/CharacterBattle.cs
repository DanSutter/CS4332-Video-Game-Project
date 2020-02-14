using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private States state;
    private Vector3 SlideTargetPosition;
    private HealthSystem healthSystem;

    private enum States
    {
        Idle, Busy, Slide
    }

    private void Awake()
    {
        state = States.Idle;
    }

    public void Setup(bool isPlayer)
    {
        if(isPlayer)
        {
            //set sedud to player's sedud appearance/stats
        }
        else
        {
            //set sedud to enemy sedud
        }
        //healthSystem = new HealthSystem(100);

    }

    private void Update()
    {
        switch(state)
        {
            case States.Idle:
                break;
            case States.Busy:
                break;
            case States.Slide:
                float speed = 10f;
                transform.position += (SlideTargetPosition - GetPosition()) * speed * Time.deltaTime;

                float reachedDistance = 1f;
                if(Vector3.Distance(GetPosition(), SlideTargetPosition) < reachedDistance)
                {
                    //arrived at slide target position
                    transform.position = SlideTargetPosition;
                    bool slideComplete = true;
                }
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage(int damageAmount)
    {
        //healthSystem.Damage(damageAmount);
    }

    public void Attack(CharacterBattle targetCharacterBattle)
    {
        targetCharacterBattle.Damage(10);
        Debug.Log("10 damage");
    }
}


