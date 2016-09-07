using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField]
    Canvas hud, menu, move;
    
    //hideininspector keeps public values from being tampered with in the inspector by hiding them
    [HideInInspector]
    public bool canvasSwitch = false;

    int culling;
    Camera cam;

    void Awake()
    {
        if(Instance == null) Instance = this; else Destroy(this);

        cam = Camera.main;
    }

    public void ExitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void MainMenu()
    {
        hud.gameObject.SetActive(canvasSwitch);
        cam.cullingMask = (!canvasSwitch) ? LayerMask.NameToLayer("UI") : LayerMask.NameToLayer("Everything");

        menu.gameObject.SetActive(!canvasSwitch);

        canvasSwitch = !canvasSwitch;
    }

    public void MoveMenu()
    {
        hud.gameObject.SetActive(canvasSwitch);
        move.gameObject.SetActive(!canvasSwitch);

        canvasSwitch = !canvasSwitch;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(SceneLoading());
    }


    IEnumerator SceneLoading()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
