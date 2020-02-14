using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Sedud:MonoBehaviour
{
    public string Name { get; set; }
    private int age;    //backing field for Age. prevents stack overflow errors
    public int Age  //0,1,2 for egg,baby,adult
    { 
        get { return age; }
        set
        { 
            age = value;
            if (age < 0)
                age = 0;
            if (age > 3)
                age = 3;
        }
    }
    private float agetimer=0;

    public GameObject popUI;

    private int aff;
    public int Affection   //(-10,10)
    {
        get { return aff; }
        set
        {
            aff = value;
            if (aff < -10)
                aff = -10;
            if (aff > 10)
                aff = 10;
        }
    }

    public string World;    //world the sedud is in

    private float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hp > 100)
                hp = 100;
        }
    }
    private float atk;
    public float ATK
    {
        get { return atk; }
        set
        {
            atk = value;
            if (atk > 100)
                atk = 100;
        }

    }
    private float def;
    public float DEF
    {
        get { return def; }
        set
        {
            def = value;
            if (def > 100)
                def = 100;
        }
    }
    private float spatk;
    public float SPATK
    {
        get { return spatk; }
        set
        {
            spatk = value;
            if (spatk > 100)
                spatk = 100;
        }
    }
    private float spdef;
    public float SPDEF
    {
        get { return spdef; }
        set
        {
            spdef = value;
            if (spdef > 100)
                spdef = 100;
        }
    }
    private float spe;
    public float SPE
    {
        get { return spe; }
        set
        {
            spe = value;
            if (spe > 100)
                spe = 100;
        }
    }

    private int fruitcount = 0; //how many fruits eaten. max is 5 for life

    //physical parts
    private string snout;
    public string Snout 
    {
        get { return snout; }
        set
        {
            snout = value;
        }
    }
    private string feet;
    public string Feet  
    {
        get { return feet; }
        set
        {
            feet = value;
        }
    }
    private string tail;
    public string Tail 
    {
        get { return tail; }
        set
        {
            tail = value;
        }
    }
    private string ears;
    public string Ears  
    {
        get { return ears; }
        set
        {
            ears = value;
        }
    }
    private string horns;
    public string Horns  
    {
        get { return horns; }
        set
        {
            horns = value;
        }
    }
    private string wings;
    public string Wings  
    {
        get { return wings; }
        set
        {
            wings = value;
        }
    }
    private string special;
    public string Special  
    {
        get { return special; }
        set
        {
            special = value;
        }
    }

    public bool isPet = false;      //player changes this when sedud is pet
    public bool isHit = false;      //UI changes this when pet is hit
    public bool isFed = false;      //UI changes this when pet is fed
    public bool isMain = false;     // makes sedud main

    public bool gameOver = false;     //battle script changes this when sedud dies in battle

    public Transform player;   //keep track of player for AI
    public enum SedudState {Wander,Freeze,Follow,Stuck};
    public SedudState curState;
    private float freezeDist=10;
    public bool whistle;    //toggeled when player presses whistle button (E)
    private Vector3 SedudDir;   //direction to move in
    private Transform[] Waypoints;  //for Wander
    private GameObject wp;   //parent waypoint
    private bool collided = false;  //true if collided with wall/obstacle/etc, not player. chances are the sedud got stuck at a wall.
    Vector3 wayVect;    //for Wander movement
    Vector3 wayDir; //for Wander rotation

    private int curWay = 0;
    private float moveSpeed=.1f;
    private Rigidbody rb;   //sedud rigidbody
    private Collider col;   //sedud collider
    private GameObject egg;    //egg attached to same parent as this sedud
    public Vector3 originalSize;

    public bool Starter;    //whether or not the Sedud was the first 2 starters
    private bool starterDataFilled;

    public string curScene;   //used to keep starters from moving during creation
    public bool isEnemy;
    private bool refindWP = true;
    public bool moved;  //if sedud is moved to a new dome/battle, get setup again

    void Start()
    {
        //originalSize = new Vector3(.2f,.2f,.2f);
        originalSize = Vector3.one;

        //get player,rigidbody,egg
        setup();

        //get waypoints of dome
        findWaypoints();

        if (!Starter && !isEnemy)   //if a bred sedud
        {
            //disable sedud collider & grav. re-enabled when it ages.
            col = GetComponent<Collider>();
            col.enabled = false;
            rb.useGravity = false;
            //hide sedud. scale and move, because parts needed are already active/inactive. don't want to mess that up
            transform.localScale = Vector3.zero;   //make sedud tiny
            transform.position = new Vector3(transform.position.x, -1, transform.position.z);    //hide by moving sedud underground
            egg.transform.localScale = Vector3.Scale(egg.transform.localScale,originalSize);    //scale egg size to dome
        }
        else if (isEnemy)
        {
            Age = 2;

            HP = Random.Range(10, 50);
            ATK = Random.Range(10, 50);
            DEF = Random.Range(10, 50);
            SPATK = Random.Range(10, 50);
            SPDEF = Random.Range(10, 50);
            SPE = Random.Range(10, 50);
        }
        else   //if one of 2 starters
        {
            //hide egg
            egg.SetActive(false);

            //fill in the missing starter data
            HP = Random.Range(10,50);
            ATK = Random.Range(10, 50);
            DEF = Random.Range(10, 50);
            SPATK = Random.Range(10, 50);
            SPDEF = Random.Range(10, 50);
            SPE = Random.Range(10, 50);

            Snout = "AvgSnout";
            Feet = "FeetPaw";
            Tail = "TailFox";
            Ears = "EarsCat";
            Horns = "none";
            Wings = "none";
            Special = "SpecialEarth";
            Name = "Starter";
        }  
    }
    void Update()
    {
    }
    private void FixedUpdate()
    {
        //fix wandering starters after moving to first dome
        curScene = SceneManager.GetActiveScene().name;
        if (Starter && curScene == "DomeEarth" && refindWP)   //rerun start => make this run only once
        {
            setup();
            findWaypoints();
            Age = 2;    //no permababies
            refindWP = false;
        }

        //fix movement to/from all domes
        if (moved)
        {
            Debug.Log("sedud " + Name + " was moved");
            moved = false;

            setup();
            findWaypoints();
        }

        if (gameOver && !isEnemy)
            UponDeath();

        if (isPet)  //sedud gets pet
        {
            Affection++;
            isPet = false;
        }
        if (isHit)  //in menu
        {
            Affection--;
            isHit = false;
        }
        if (isFed)  //in menu
        {
            if (fruitcount < 5)
            {
                fruitcount++;
                if (HP >= 100 && ATK >= 100 && DEF >= 100 && SPATK >= 100 && SPDEF >= 100 && SPE >= 100)
                {
                    Debug.Log("+0 - all stats are capped");
                }
                else
                {
                    HP++;
                    ATK++;
                    DEF++;
                    SPATK++;
                    SPDEF++;
                    SPE++;
                }
            }
            isFed = false;
        }

        //ages
        if (Age==0 && agetimer>=10 && !Starter && !isEnemy)   //adjust # as needed for appropriate age length. keep starters from aging.
        {
            Debug.Log(Name + " is a child");
            Age = 1;

            //get rid of the egg
            egg.SetActive(false);
            //set scale to small
            transform.localScale = .5f * originalSize;
            //move sedud back above ground
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            //reenable collision
            col.enabled = true;
            rb.useGravity = true;

            popUI.SetActive(false);

        }
        else if(agetimer>=20 && !Starter && !isEnemy)   //don't need to scale starter again
        {
            Debug.Log(Name + " is an adult");
            Age = 2;

            //set scale to full size
            //transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localScale = originalSize;
        }
        agetimer += Time.deltaTime;

        //to keep starters from moving during creation
        curScene=SceneManager.GetActiveScene().name;

        //AI
        //change states
        if (Vector3.Distance(player.position,transform.position)<=freezeDist || curScene=="Start" || curScene=="BattleScene")    //if too close to player or creation/battle scenes
        {
            curState = SedudState.Freeze;
        }
        else if(whistle)
        {
            curState = SedudState.Follow;
        }
        else if(collided)
        {
            curState = SedudState.Stuck;
        }
        else if(isEnemy)
        {
            curState = SedudState.Freeze;
        }
        else
        {
            if(Age!=0)
                curState = SedudState.Wander;
        }

        //states
        switch(curState)
        {
            case SedudState.Freeze:
                Debug.Log(name + " is frozen");
                break;
            case SedudState.Follow:
                Debug.Log(name + " is following Player");
                //look at player
                SedudDir = player.position - transform.position;
                SedudDir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(SedudDir),10f*Time.deltaTime);
                //move
                Vector3 p = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
                rb.MovePosition(p);
                break;
            case SedudState.Wander:
                Wander();
                break;
            case SedudState.Stuck:
                Debug.Log(name+" got stuck!");
                collided = false;
                curWay = Random.Range(0, Waypoints.Length);//change waypoint because probably can't find its way to old one
                curState = SedudState.Wander;   //go back to wandering
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag!="Main Camera")  //can add more collisions to ignore if wanted
        {
            collided = true;
        }
    }

    void Wander()
    {
        //ignore Y position of waypoints. Y is simply set to Sedud's current position
        wayVect = Waypoints[curWay].position;
        wayVect.y = transform.position.y;

        if (transform.position!=wayVect && collided==false && Age!=0)   //if not at waypoint, not baby, not collided
        {
            Debug.Log(Name + " is Wandering around the dome");
            turn();
            Vector3 p = Vector3.MoveTowards(transform.position,wayVect,moveSpeed);
            rb.MovePosition(p);
        }
        else //at waypoint, or collided==true (stuck)
        {
            curWay = Random.Range(0, Waypoints.Length); //set next waypoint
            turn();
        }
    }
    
    void turn() //called by Wander() to turn in direction of next waypoint
    {
        //turn towards next waypoint
        wayDir = Waypoints[curWay].position;
        wayDir.y = transform.position.y;    //ignore y value
        wayDir -= transform.position;   //-cur position
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wayDir), 10 * Time.deltaTime);
    }

    void UponDeath()
    {
        Debug.Log(Name + " died");
        if(Affection==10)
        {
            Debug.Log(Name + " reincarnated");
            Affection = 0;
            Age = 0;

            //all stats down by 10%
            int temp = (int)(HP*.1f);
            HP-=temp;
            temp = (int)(ATK * .1f);
            ATK-=temp;
            temp = (int)(DEF * .1f);
            DEF-=temp;
            temp = (int)(SPATK * .1f);
            SPATK-=temp;
            temp = (int)(SPDEF * .1f);
            SPDEF-=temp;
            temp = (int)(SPE * .1f);
            SPE-=temp;
        }
        else
        {
            SendToBoss();

            GameObject parent = transform.parent.gameObject;    //get parent object of sedud
            Destroy(parent);    //delete it, egg object, and self
        }
    }

    void SendToBoss()   //TODO
    {
        Debug.Log("Sent " + Name + " to boss");

        //save name
        //+1 to ghostcount
        //+1 to all of boss stats
    }

    private void findWaypoints()
    {
        wp = null;
        Waypoints = null;
        curWay = 0;

        wp = GameObject.Find("Waypoint");   //parent waypoint
        Waypoints = new Transform[wp.transform.childCount + 1];  //set size to number of waypoints in dome   
        Waypoints[0] = wp.transform;    //first waypoint is the parent
        for (int i = 0; i < wp.transform.childCount; i++)  //child waypoints
        {
            Waypoints[i + 1] = wp.transform.GetChild(i);
        }
    }

    private void setup()
    {
        player = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        egg = transform.parent.GetChild(0).gameObject;
    }
}