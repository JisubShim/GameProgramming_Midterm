using System.Collections;
using UnityEngine;

public class Boss1DeadState : IBossState
{
    private Boss1StateController _boss1StateSystem;
    private Boss1 _boss1;

    public Boss1DeadState(Boss1StateController boss1StateSystem, Boss1 boss1)
    {
        _boss1StateSystem = boss1StateSystem;
        _boss1 = boss1;
    }

    public void Enter()
    {
        _boss1.Speak("으악! 내가 지다니...");
        _boss1.Boss1Animator.SetTrigger("isDead");
        StartDeadCoroutine();
    }

    public void Exit()
    {

    }

    public void Update()
    {
    }

    public void StartDeadCoroutine()
    {
        _boss1.StartCoroutine(DeadCoroutine());
    }

    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.Instance.LoadScene("EndingScene");
    }
}