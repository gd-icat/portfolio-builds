using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameStates
{
    menu,
    gameplay,
    results,
    pause
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameStates _currentState;
    [SerializeField] private bool _isChangingStates;
    [SerializeField] private int _index;
    [Header("Events"), Space(4)]
    public UnityEvent OnStateChanged;
    // Start is called before the first frame update
    void Start()
    {
        //first 'entry' into deafult state
        _currentState = GameStates.menu;
    }

    // Update is called once PER FRAME
    void Update()
    {
        ChangeStateContinous(_currentState);
    }

    private void LateUpdate()
    {
        //State Change Based on Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //start game
            _currentState = GameStates.menu;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //start gameplay
            _currentState = GameStates.gameplay;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //end gameplay
            _currentState = GameStates.results;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //pause game
            _currentState = GameStates.pause;
        }
    }

    //This can be called from Outside the script, Ideally a Button (UnityEvent)
    public void ChangeState()
    {
        _isChangingStates = true;
    }

    private void ChangeStateContinous(GameStates state) 
    {
        if (_isChangingStates)
        {
            switch (state)
            {
                case GameStates.menu:
                    //On Game Start
                    //pop menu UI, disable input, start music
                    //main menu already Loaded
                    _index = 0;
                    break;
                case GameStates.gameplay:
                    //enable input, fade out menu UI, start SFX
                    //load level scenes
                    _index++;
                    LoadSceneNext(_index);
                    break;
                case GameStates.results:
                    //On Game End
                    //disable input, fade in Result UI / Game Over UI, change music
                    //load last scene
                    LoadSceneNext(SceneManager.sceneCountInBuildSettings - 1);
                    break;
                case GameStates.pause:
                    //Disable input, fade in Pause menu UI, pause music
                    break;
            }

            OnStateChanged?.Invoke();
            Debug.Log("changed states");

            _isChangingStates = false;
        }
    }

    private void LoadSceneNext(int sceneId)
    {
        if(SceneManager.sceneCount < 2)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
        }

        else
        {
            Debug.Log("Can't load more than 2 scenes.", this);
            SceneManager.UnloadSceneAsync(sceneId - 1);
            SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        }

        Debug.Log("loading " + SceneManager.GetSceneByBuildIndex(sceneId).name);
    }

    private void LoadScenePrevious(int sceneId)
    {
        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
        }

        else
        {
            Debug.Log("Can't load more than 2 scenes.", this);
            SceneManager.UnloadSceneAsync(sceneId);
            SceneManager.LoadSceneAsync(sceneId - 1, LoadSceneMode.Additive);
        }

        Debug.Log("loading " + SceneManager.GetSceneByBuildIndex(sceneId - 1).name);
    }
}
