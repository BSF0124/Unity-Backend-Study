using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onGameOver;
    [SerializeField]
    private DailyRankRegister dailyRank;

    private int score = 0;

    public bool IsGameOver {set; get;} = false;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    public void GameOver()
    {
        if(IsGameOver) return;
        IsGameOver = true;

        onGameOver.Invoke();

        dailyRank.Process(score);

        BackendGameData.Instance.UserGameData.experience += 25;
        if(BackendGameData.Instance.UserGameData.experience >= 100)
        {
            BackendGameData.Instance.UserGameData.experience = 0;
            BackendGameData.Instance.UserGameData.level++;
        }
        BackendGameData.Instance.GameDataUpdate();
    }
}
