using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSedud : MonoBehaviour
{
    // scene to move to 
    public string m_Scene;

    // objects to move
    public GameObject ASedud;
    public GameObject BSedud;

    //get all seduds
    //public GameObject[] sedudList;
    SedudList list;
    GameObject listHolder;

    void Start()
    {
        listHolder = new GameObject();  //create empty gameobject
        listHolder.AddComponent<SedudList>();   //attach SedudList
        list = listHolder.GetComponent<SedudList>();    //list=that SedudList
    }
    void Update()
    {

    }

    public void moveFromStartScene()    //move seduds from start screen to earth. originally named moveScene(). dan didn't want to break zoraida's code.
    {
        Scene thisScene = SceneManager.GetActiveScene();

        // only if in start
        if (thisScene.name == "Start")
        {
            ASedud.SetActive(true);
            ASedud.transform.position = new Vector3(ASedud.transform.position.x + 5, ASedud.transform.position.y, ASedud.transform.position.z);
        }

        // to get all seduds
        list.fillList();
        
        for (int i = 0; i < list.sedudList.Length; i++)
        {
            Debug.Log("sedud to move: " + list.sedudList[i].name);
        }

        StartCoroutine(LoadYourAsyncScene());
    }

    public void moveScene() //new function for more general movement
    {
        Debug.Log("Moving scenes...");
        list.fillList();
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        Debug.Log("Started async");
        PlayerPrefs.SetInt("MoveFinished", 0);

        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

        //prevent the scene from being loaded instantly
        asyncLoad.allowSceneActivation = false;
        //loading for 90% of process
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            if (asyncLoad.progress >= 0.9f && changeSceneNow())
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        /*// to move all seduds
        Debug.Log("Moving seduds...");
        for (int i = 0; i < list.sedudList.Length; i++)
        {
            if (ASedud)
            {
                ASedud.transform.position = new Vector3(ASedud.transform.position.x, ASedud.transform.position.y, ASedud.transform.position.z);
            }
            SceneManager.MoveGameObjectToScene(list.sedudList[i], SceneManager.GetSceneByName(m_Scene));
        }
        Debug.Log("Seduds moved");*/

        //set new scene to the active one
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(m_Scene));

        list.setAllMoved();
        PlayerPrefs.SetInt("MoveFinished", 1);

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
        Debug.Log("Finished moving!");
    }
    bool changeSceneNow()
    {
        Debug.Log("Moving seduds...");
        for (int i = 0; i < list.sedudList.Length; i++)
        {
            if (ASedud)
            {
                ASedud.transform.position = new Vector3(ASedud.transform.position.x, ASedud.transform.position.y, ASedud.transform.position.z);
            }
            SceneManager.MoveGameObjectToScene(list.sedudList[i], SceneManager.GetSceneByName(m_Scene));
        }
        Debug.Log("Seduds moved");

        return true;
    }
}