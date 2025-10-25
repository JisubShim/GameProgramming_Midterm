using UnityEngine;

public class Boss1HitableState : IState
{
    private Boss1StateSystem _boss1StateSystem;
    private Boss1 _boss1;

    public Boss1HitableState(Boss1StateSystem boss1StateSystem, Boss1 boss1)
    {
        _boss1StateSystem = boss1StateSystem;
        _boss1 = boss1;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
