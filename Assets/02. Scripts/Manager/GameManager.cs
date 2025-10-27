using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float _timer = 0f;
    private bool _isFlowTime = false;
    private float _record = 0f;
    private float _bestRecord = 0f;
    public float Timer => _timer;
    public float Record => _record;
    public float BestRecord => _bestRecord;

    protected override void Awake()
    {
        base.Awake();
        _bestRecord = PlayerPrefs.GetFloat("BestRecord", 0f);
    }
    public void StartTimer()
    {
        _timer = 0;
        _isFlowTime = true;
    }

    public void EndTimer()
    {
        _isFlowTime = false;
        _record = _timer;
        if (_record > _bestRecord)
        {
            _bestRecord = _record;
            PlayerPrefs.SetFloat("BestRecord", _bestRecord);
            PlayerPrefs.Save();
            Debug.Log("신기록 달성! 저장된 기록: " + _bestRecord);
        }
    }

    private void Update()
    {
        if (!_isFlowTime) return;
        _timer += Time.deltaTime;
    }
}
