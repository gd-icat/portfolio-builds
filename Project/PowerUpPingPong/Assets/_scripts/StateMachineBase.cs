using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StateMachineBase<T> : MonoBehaviour where T : Enum 
{
    [SerializeField] protected List<T> _allStates;
    [SerializeField] private bool _isChangingStates;
    [SerializeField] private int _index;
    [SerializeField] protected T _currentState;
    [Header("Events"), Space(4)]
    public UnityEvent OnStateMachineStart;
    public UnityEvent OnStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        EnterDefaultState();
    }

    // Update is called once PER FRAME
    void Update()
    {
        UpdateState();
    }

    //This can be called from Outside the script, Ideally a Button (UnityEvent)
    public void ChangeState()
    {
        _isChangingStates = true;
        NextState();
    }

    protected virtual void NextState(T state)
    {
        if (_isChangingStates)
        {
            _currentState = state;

            OnStateChanged.Invoke();
            Debug.Log("changed states");

            _isChangingStates = false;
        }
    }

    protected virtual void NextState()
    {
        if (_isChangingStates)
        {
            _index++;
            _currentState = _allStates[_index];

            OnStateChanged.Invoke();
            Debug.Log("changed states");

            _isChangingStates = false;
        }
    }

    protected abstract void UpdateState();

    protected virtual void EnterDefaultState()
    {
        _index = 0;
        //first 'entry' into deafult state
        _currentState = _allStates[_index];

        OnStateMachineStart.Invoke();
    }
}
