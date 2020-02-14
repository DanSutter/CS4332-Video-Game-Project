/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    int previousLevel;

    [SerializeField] private Transform sedud;
    public HealthSystem healthSystem;
    public HealthBar healthBar;
    public HealthBarEnemy healthBarEnemy;

    //public Sedud playerSedud;

    public Button AttackButton;
    public Button SAttackButton;

    public Text gameOver;
    public Text victory;

    bool buttonPressed = false;
    private int number;

    float damage;

    /*private Sedud enemy;
    //for spawning random enemy
    private GameObject parentE;
    private GameObject Egg;
    public GameObject ChildE;
    public GameObject template; //sedud body prefab
    private int whichAtt = 0;
    private GameObject model;

    //for player
    private GameObject playerSedudParent;

    SedudList list;
    GameObject listHolder;*/

    //keep realstart from rerunning
    //bool setupDone;

    // Start is called before the first frame update
    //void Start()
    //{
       
    //}
    /*void Update()
    {
        //THIS IS WHY THE RANDOM ENEMY WOULD NEVER SPAWN UPON SCENE CHANGE!!!!!!
        //Start was trying to run before the previous scene ever unloaded. I don't know exactly
        //why that broke the spawning, but I assume it was trying to spawn in the previous scene, 
        //then immediately gets deleted

        // Umm no, changing this just messed up what was working code so I don't know why this is here???

        if(PlayerPrefs.GetInt("MoveFinished")==1 && !setupDone) 
        {
            setupDone = true;
            realStart();
        }
    }*/


    /*private void SpawnSedud(bool isPlayer)
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
        //Instantiate(sedud, position, Quaternion.Euler(0, rotation, 0));
    //}

   /* IEnumerator Start()
    {
        victory.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);

        previousLevel = PlayerPrefs.GetInt("previousLevel");

        //set up list of all seduds in scene
        //listHolder = new GameObject();  //create empty gameobject
        //listHolder.AddComponent<SedudList>();   //attach SedudList
        //list = listHolder.GetComponent<SedudList>();    //list=that SedudList

        //spawn enemy
        //spawnEnemy();
        //set up player's active battler on the scene
        //setPlayer();


        SpawnSedud(true);
        SpawnSedud(false);


        healthBar.setup(healthSystem);
        healthBarEnemy.setup(healthSystem);

        //healthSystem = new HealthSystem();
        healthSystem.isDead = false;
        healthSystem.gameOver = false;

        //Calculate Player max health
        healthSystem.playerMaxHealth = Random.Range(20.0f, 30.0f);
        healthSystem.playerHealth = healthSystem.playerMaxHealth;

        // Assign NPC Max Health
        healthSystem.npcMaxHealth = Random.Range(20.0f, 30.0f);
        healthSystem.npcHealth = healthSystem.npcMaxHealth;

        int counter = 2;

        //Infinite loop
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

                    //+1 to boss stats


                    PlayerPrefs.SetInt("BossHP", 100);
                    PlayerPrefs.SetInt("BossATK", 100);

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

    //spawn a random enemy
    /*private void spawnEnemy()
    {
        //new empty gameobject to be parent. will hold the Sedud class information
        parentE = new GameObject("Enemy");
        parentE.transform.position = new Vector3(6.29f,0,1);
        parentE.transform.rotation = Quaternion.Euler(0, -90, 0);

        //make an empty gameobject as a placeholder for the egg. we don't actually need an egg for these guys.
        Egg = new GameObject("eggPlaceholder");
        Egg.transform.position = parentE.transform.position;
        Egg.transform.parent = parentE.transform;

        //sedud, child
        ChildE = Instantiate(template, parentE.transform);  //create new Sedud

        enemy = ChildE.GetComponent<Sedud>();

        //set to battle mode so that it doesn't age. it won't move either, since the camera is so close.
        //keep untagged so it doesn't come with us later
        enemy.isEnemy = true;

        //fill out sedud information in script's variables
        enemy.Name = "Enemy";
        enemy.Snout = selectChance("snout");
        enemy.Feet = selectChance("feet");
        enemy.Tail = selectChance("tail");
        enemy.Ears = selectChance("ears");
        enemy.Horns = selectChance("horns");
        enemy.Wings = selectChance("wings");
        enemy.Special = selectChance("special");

        //turn on/off /color parts
        for (int i = 0; i < ChildE.transform.childCount; i++)
        {
            model = ChildE.transform.GetChild(i).gameObject;
            //Debug.Log("model name: " + model.name);
            //Debug.Log("child snout: " + child.Snout);
            if (model.name != enemy.Snout && model.name != enemy.Feet && model.name != enemy.Tail &&
                model.name != enemy.Ears && model.name != enemy.Horns && model.name != enemy.Wings &&
                model.name != enemy.Special)  //if not one of chosen parts
            {
                model.SetActive(false); //turn off part
            }
            else if (model.name != "SpecialSpace") //if chosen part, and chosen part is not SpecialSpace, give it a random color
            {
                int color = Random.Range(1, 5);
                switch (color)
                {
                    case 1: //blue
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudBlue") as Material;
                        break;
                    case 2: //green
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudGreen") as Material;
                        break;
                    case 3: //red
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudRed") as Material;
                        break;
                    case 4: //yellow
                        model.GetComponent<Renderer>().material = Resources.Load("Sedud Colors/SedudYellow") as Material;
                        break;
                }
            }
        }
        Debug.Log("Spawned enemy");
    }*/
    /*private string selectChance(string part)
    {

        if (part == "snout")
            whichAtt = Random.Range(0, 3);
        else if (part == "feet")
            whichAtt = Random.Range(12, 19);
        else if (part == "tail")
            whichAtt = Random.Range(26, 33);
        else if (part == "ears")
            whichAtt = Random.Range(3, 12);
        else if (part == "horns")
            whichAtt = Random.Range(19, 26);
        else if (part == "wings")
            whichAtt = Random.Range(33, 38);
        else if (part == "special")
            whichAtt = Random.Range(38, 42);

        switch (whichAtt)
        {
            //noses
            case 0:
                return "ShortSnout";
            case 1:
                return "AvgSnout";
            case 2:
                return "LongSnout";
            //ears
            case 3:
                return "EarsBat";
            case 4:
                return "EarsCat";
            case 5:
                return "EarsSheep";
            case 6:
                return "EarsDog";
            case 7:
                return "EarsBunnyUp";
            case 8:
                return "EarsBunnyDown";
            case 9:
                return "EarsHuman";
            case 10:
                return "EarsWolf";
            case 11:
                return "EarsLong";
            //feet
            case 12:
                return "FeetNone";
            case 13:
                return "FeetNone";
            case 14:
                return "FeetNone";
            case 15:
                return "FeetBird";
            case 16:
                return "FeetPaw";
            case 17:
                return "FeetHoof";
            case 18:
                return "FeetWebbed";
            //horns
            case 19:
                return "HornsNone";
            case 20:
                return "HornsAntennae";
            case 21:
                return "HornsBack";
            case 22:
                return "HornsForward";
            case 23:
                return "HornsPointy";
            case 24:
                return "HornsRound";
            case 25:
                return "HornsTall";
            //tail
            case 26:
                return "TailCat";
            case 27:
                return "TailDog";
            case 28:
                return "TailFox";
            case 29:
                return "TailWolf";
            case 30:
                return "TailDragon";
            case 31:
                return "TailFish";
            case 32:
                return "TailDevil";
            //wings
            case 33:
                return "WingsNone";
            case 34:
                return "WingsButterfly";
            case 35:
                return "WingsBat";
            case 36:
                return "WingsSwirl";
            case 37:
                return "WingsAngel";
            //specials
            case 38:
                return "SpecialEarth";
            case 39:
                return "SpecialSnow";
            case 40:
                return "SpecialSpace";
            case 41:
                return "SpecialWater";
            default:
                return ("None");
        }
    }

    private void setPlayer()
    {
        //set up player's active battler
        for (int i = 0; i < list.sedudList.Length; i++)
        {
            if (list.sedudList[i].transform.GetChild(1).GetComponent<Sedud>().isMain)
            {
                playerSedudParent = list.sedudList[i];
                Debug.Log("main: " + list.sedudList[i]);
                playerSedud = list.sedudList[i].transform.GetChild(1).GetComponent<Sedud>();
                Debug.Log("main name: " + playerSedud.Name);
            }
        }
        playerSedudParent.transform.position = new Vector3(0, 0, 0);
        playerSedudParent.transform.GetChild(1).position = new Vector3(-6.29f, 0, -10);
        playerSedudParent.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0);
        playerSedudParent.transform.rotation = Quaternion.Euler(0, 90, 0);
    }*/

    // Coroutine
    /*IEnumerator WaitForClick()
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
    void attack(int number) //player's turn
    {
        buttonPressed = true;

        if(number == 1)
        {
            //regular attack
            Debug.Log("attack");

            //calculate attack damage
            //damage = Random.Range(playerSedud.ATK/2, playerSedud.ATK * 2);
            //damage = Random.Range(2*playerSedud.ATK/enemy.DEF-2, 2*playerSedud.ATK/ enemy.DEF+2);

            damage = Random.Range(1.0f, 5.0f);

            //call player damage function to deal damage to npc
            healthSystem.PlayerDamage(damage);
        }

        if(number == 2)
        {
            //special attack
            Debug.Log("special attack");
            //calculate special attack damage
            //damage = satk * 2;
            //damage = Random.Range(satk/2, satk*2);
            //damage = Random.Range(2 * playerSedud.SPATK / enemy.SPDEF - 2, 2 * playerSedud.SPATK / enemy.SPDEF + 2);

            damage = Random.Range(2.0f, 6.0f);

            //call player damage function to deal damage to npc
            healthSystem.PlayerDamage(damage);

        }
    }

    void npcTurn()  //npc's turn
    {
        //randomly decide physical or special attack
        int type = Random.Range(0,2);
        switch(type)
        {
            case 0:
                //damage=Random.Range(2*enemy.ATK/playerSedud.DEF-2, 2*enemy.ATK/playerSedud.DEF+2);
                Random.Range(1.0f, 5.0f);
                break;
            case 1:
                //damage = Random.Range(2 * enemy.SPATK / playerSedud.SPDEF - 2, 2 * enemy.SPATK / playerSedud.SPDEF + 2);
                Random.Range(1.0f, 3.0f);
                break;
        }

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
}*/
