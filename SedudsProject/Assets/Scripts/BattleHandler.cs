using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform sedud;

    //private CharacterBattle playerCharacterBattle;
    //private CharacterBattle enemyCharacterBattle;

    //private CharacterBattle activeChar;

    private States state;


    private enum States
    {
        Waiting, Busy
    }

    private void Start()
    {
        SpawnSedud(true);
        SpawnSedud(false);

        //SetActiveChar(playerCharacterBattle);
        state = States.Waiting;
    }

    private void Update()
    {
        if(state == States.Waiting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = States.Busy;
                //attack
                //SwitchActive();
            }
        }
    }

    private void SpawnSedud(bool isPlayer)
    {
        Vector3 position;
        float rotation;

        if (isPlayer)
        {
            position = new Vector3(-6.29f, 1.1f);
            rotation = 90f;
        }
        else
        {
            position = new Vector3(6.29f, 1.1f);
            rotation = -90f;
        }

        //instantiate different seduds
        /*Transform characterTransform = Instantiate(sedud, position, Quaternion.Euler(0, rotation, 0));
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayer);*/

        // instantiate same sedud
        Instantiate(sedud, position, Quaternion.Euler(0, rotation, 0));
    }

    /*private void SetActiveChar(CharacterBattle characterBattle)
    {
        activeChar = characterBattle;
    }

    private void SwitchActive()
    {
        if(activeChar == playerCharacterBattle)
        {
            SetActiveChar(enemyCharacterBattle);
            state = States.Busy;

            //enemy attacks player
            SwitchActive();
        }
        else
        {
            SetActiveChar(playerCharacterBattle);
            state = States.Waiting;
        }
    }*/
}
