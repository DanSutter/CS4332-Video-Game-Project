using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    [SerializeField] private Transform sedud;
    public HealthSystem healthSystem = new HealthSystem();
    public HealthBar healthBar;
    public HealthBarEnemy healthBarEnemy;

    public Sedud playerSedud;

    public float atk;
    public float def;
    public float satk;
    public float sdef;
    public float spd;

    public Button AttackButton;
    public Button SAttackButton;

    public Text gameOver;
    public Text victory;

    bool buttonPressed = false;
    private int number;

    float damage;

    public Text myText;

    string myLog;
    Queue myLogQueue = new Queue();

    // Spawn two seduds
    private void SpawnSedud(bool isPlayer)
    {
        Vector3 position;
        float rotation;

        if (isPlayer)
        {
            position = new Vector3(-6.29f, 1.1f);
            rotation = 90f;

            sedud.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            position = new Vector3(6.29f, 1.1f);
            rotation = -90f;

            sedud.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        //instantiate different seduds
        /*Transform characterTransform = Instantiate(sedud, position, Quaternion.Euler(0, rotation, 0));
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayer);*/

        // instantiate same sedud
        Instantiate(sedud, position, Quaternion.Euler(0, rotation, 0));
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        victory.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);

        int previousLevel = PlayerPrefs.GetInt("previousLevel");

        SpawnSedud(true);
        SpawnSedud(false);

        healthBar.setup(healthSystem);
        healthBarEnemy.setup(healthSystem);

        healthSystem.isDead = false;
        healthSystem.gameOver = false;

        //Calculate Player max health
        healthSystem.playerMaxHealth = Random.Range(20.0f, 30.0f);
        healthSystem.playerHealth = healthSystem.playerMaxHealth;

        // Assign NPC Max Health
        healthSystem.npcMaxHealth = Random.Range(20.0f, 30.0f);
        healthSystem.npcHealth = healthSystem.npcMaxHealth;

        int counter = 2;

        /*if(playerSedud.isMain == true)
        {
            //Calculate Player max health
            healthSystem.playerMaxHealth = playerSedud.HP;
            healthSystem.playerHealth = healthSystem.playerMaxHealth;

            //Set sedud stats
            atk = playerSedud.ATK;
            def = playerSedud.DEF;
            satk = playerSedud.SPATK;
            sdef = playerSedud.SPDEF;
            spd = playerSedud.SPE;
        }*/

        // Infinite loop
        while (true)
        {
            Debug.Log("\n");


            Debug.Log("Player health = " + healthSystem.GetPlayerHealth());



            Debug.Log("NPC health = " + healthSystem.GetNPCHealth());



            // Call player turn on even count
            if (counter % 2 == 0)
            {
                buttonPressed = false;

                // Pause until user makes a selection
                yield return StartCoroutine(WaitForClick());

                if (healthSystem.isDead == true)
                {
                    //add exp
                    //if (certain amount of exp) level up?
                    Debug.Log("NPC Dead");

                    victory.gameObject.SetActive(true);

                    yield return StartCoroutine(Delay1());

                    SceneManager.LoadScene(previousLevel);
                    break;
                }
                if (healthSystem.gameOver == true)
                {
                    Debug.Log("Player Dead");

                    gameOver.gameObject.SetActive(true);

                    yield return StartCoroutine(Delay1());

                    //change to previous scene
                    SceneManager.LoadScene(previousLevel);

                    //sedud dies
                    break;
                }
                counter++;
            }
            // Otherwise call npc turn
            else
            {
                yield return StartCoroutine(Delay());

                npcTurn();

                if (healthSystem.isDead == true)
                {
                    Debug.Log("NPC Dead");

                    victory.gameObject.SetActive(true);

                    yield return StartCoroutine(Delay1());

                    SceneManager.LoadScene(previousLevel);

                    //increase sedud stats (howwwwwww)
                    break;
                }
                if (healthSystem.gameOver == true)
                {
                    Debug.Log("Player Dead");

                    gameOver.gameObject.SetActive(true);

                    yield return StartCoroutine(Delay1());

                    //change to previous scene
                    SceneManager.LoadScene(previousLevel);

                    //sedud dies
                    break;
                }

                counter--;
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }

        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
            myLogQueue.Dequeue();
        }
    }

    void OnGUI()
    {
        //GUILayout.BeginArea(new Rect(860, 0, 200, 200));

        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 40;

        GUI.Label(new Rect(680, 0, 200, 200), myLog, myStyle);

        //GUILayout.TextArea(myLog);
        //GUILayout.EndArea();
    }

    // Coroutine
    IEnumerator WaitForClick()
    {
        playerTurn();

        yield return new WaitUntil(() => buttonPressed);

        attack(number);
    }

    void playerTurn()
    {
        Debug.Log("player turn");

        //AttackButton.onClick.AddListener(delegate { attack(1); });
        //SAttackButton.onClick.AddListener(delegate { attack(2); });

        // if attack button is clicked, call attack function
        AttackButton.onClick.AddListener(clickAtk);

        // if special attack button is clicked, call special attack function
        SAttackButton.onClick.AddListener(clickSAtk);

    }

    // Handles attack button click
    public void clickAtk()
    {
        buttonPressed = true;
        number = 1;

    }

    // Handles special attack button click
    public void clickSAtk()
    {
        buttonPressed = true;
        number = 2;

    }

    // Handles attacks
    void attack(int number)
    {
        buttonPressed = true;

        if (number == 1)
        {
            //regular attack
            Debug.Log("attack");

            //calculate attack damage
            //damage = 1 * (atk/2);

            damage = Random.Range(1.0f, 5.0f);

            Application.logMessageReceived += HandleLog;

            Debug.Log("Player does " + damage + " damage!");

            Application.logMessageReceived -= HandleLog;

            //call player damage function to deal damage to npc
            healthSystem.PlayerDamage(damage);
        }

        if (number == 2)
        {
            //special attack
            Debug.Log("special attack");
            //calculate special attack damage
            //damage = 1 * (satk / 2);

            damage = Random.Range(2.0f, 6.0f);

            Application.logMessageReceived += HandleLog;

            Debug.Log("Player does " + damage + " damage!");

            Application.logMessageReceived -= HandleLog;

            //call player damage function to deal damage to npc
            healthSystem.PlayerDamage(damage);

        }
    }

    void npcTurn()
    {
        //randomly generate damage number based on player's damage? or level
        //damage = 1((atk+satk)/((def+sdef)-(healthSystem.playerMaxHealth)));

        damage = Random.Range(1.0f, 5.0f);

        Application.logMessageReceived += HandleLog;

        Debug.Log("Enemy does " + damage + " damage!");

        Application.logMessageReceived -= HandleLog;

        healthSystem.NPCDamage(damage);

        Debug.Log("NPC turn");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Delay1()
    {
        yield return new WaitForSeconds(2);
    }
}
