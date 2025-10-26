using System.Collections.Generic;
using UnityEngine;

public enum Boss1State
{
    None, Idle, Attack, Hitable, Dead
}

public class Boss1StateController : MonoBehaviour
{
    [SerializeField] private Boss1State _currentState;
    [SerializeField] private Boss1PatternSystem _patternSystem;
    [SerializeField] private GroundController[] _groundControllers;
    private Boss1State _previousState;

    private Dictionary<Boss1State, IState> _stateDictionary;
    private Boss1Phase2AttackState _phase2AttackState;
    private Boss1Phase3AttackState _phase3AttackState;

    public Boss1State CurrentState => _currentState;
    private int _phase = 1;

    private void Awake()
    {
        Boss1 boss1 = GetComponent<Boss1>();

        _phase2AttackState = new Boss1Phase2AttackState(this, boss1, _patternSystem);
        _phase3AttackState = new Boss1Phase3AttackState(this, boss1, _patternSystem);

        _stateDictionary = new Dictionary<Boss1State, IState>
        {
            { Boss1State.Idle,          new Boss1IdleState(this, boss1) },
            { Boss1State.Attack,       new Boss1Phase1AttackState(this, boss1, _patternSystem) },
            { Boss1State.Hitable,       new Boss1HitableState(this, boss1) }
        };
    }

    private void Start()
    {
        Initialize(Boss1State.Idle);
    }

    private void Initialize(Boss1State startingState)
    {
        SetActiveObstacle(true);
        _previousState = startingState;
        _currentState = startingState;
        _stateDictionary[_currentState].Enter();
    }

    public void TransitionTo(Boss1State nextState)
    {
        _previousState = _currentState;
        _stateDictionary[_currentState].Exit();
        _currentState = nextState;
        _stateDictionary[_currentState].Enter();
    }

    public void RevertPreviousState()
    {
        Boss1State temp = _previousState;
        _stateDictionary[_previousState].Enter();
        _previousState = _currentState;
        _currentState = temp;
    }

    public void SetActiveObstacle(bool isActive)
    {
        _groundControllers[0].SetActiveObstacle(isActive);
        _groundControllers[1].SetActiveObstacle(isActive);
    }

    public void GoNextPhase()
    {
        _phase++;
        if (_phase == 2)
        {
            _stateDictionary[Boss1State.Attack] = _phase2AttackState;
        }
        else if (_phase == 3)
        {
            _stateDictionary[Boss1State.Attack] = _phase3AttackState;
        }
    }

    private void Update()
    {
        _stateDictionary[_currentState].Update();
    }
}
