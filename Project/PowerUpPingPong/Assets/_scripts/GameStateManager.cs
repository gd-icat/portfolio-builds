using System.Collections;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        //first 'entry' into deafult state
        _currentState = GameStates.menu;
    }

    // Update is called once PER FRAME
    void Update()
    {
        switch (_currentState)
        {
            case GameStates.menu:
                //pop menu UI, disable input, start music
                break;
            case GameStates.gameplay:
                //enable input, fade out menu UI, start SFX
                break;
            case GameStates.results:
                //disable input, fade in Result UI / Game Over UI, change music
                break;
            case GameStates.pause:
                //Disable input, fade in Pause menu UI, pause music
                break;
        }
    }

    private void LateUpdate()
    {
        //State Change Based on Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentState = GameStates.menu;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentState = GameStates.gameplay;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentState = GameStates.results;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _currentState = GameStates.pause;
        }
    }

    //This can be called from Outside the script, Ideally a Button (UnityEvent)
    public void ChangeState(GameStates state)
    {
        _currentState = state;
    }
}
