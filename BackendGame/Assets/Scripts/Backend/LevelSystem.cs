using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private readonly int increaseExperience = 25; // 게임 플레이 당 획득 경험치

    public void Process()
    {
        int currentLevel = BackendGameData.Instance.UserGameData.level;

        BackendGameData.Instance.UserGameData.experience += increaseExperience;

        if(BackendGameData.Instance.UserGameData.experience >= BackendChartData.levelChart[currentLevel-1].maxExperience &&
            BackendChartData.levelChart.Count > currentLevel)
        {
            BackendGameData.Instance.UserGameData.gold += BackendChartData.levelChart[currentLevel-1].rewardGold;
            BackendGameData.Instance.UserGameData.experience = 0;
            BackendGameData.Instance.UserGameData.level++;
        }

        BackendGameData.Instance.GameDataUpdate();

        Debug.Log($"현재 레벨 : {BackendGameData.Instance.UserGameData.level}," +
            $"경험치 : {BackendGameData.Instance.UserGameData.experience}/" +
            $"{BackendChartData.levelChart[currentLevel-1].maxExperience}");
    }
}
