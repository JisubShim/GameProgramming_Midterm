using UnityEngine;

public class Boss1AttackState : IState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private Boss1PatternSystem _patternSystem;

    public Boss1AttackState(Boss1StateController boss1StateSystem, Boss1 boss1, Boss1PatternSystem patternSystem)
    {
        _boss1StateController = boss1StateSystem;
        _boss1 = boss1;
        _patternSystem = patternSystem;
    }

    public void Enter()
    {
        _patternSystem.PlayBigBombPattern(3, 3f);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}