using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool IsGameOver {set; get;} = false;

    public void GameOver()
    {
        if(IsGameOver) return;
        IsGameOver = true;

        BackendGameData.Instance.UserGameData.experience += 25;
        if(BackendGameData.Instance.UserGameData.experience >= 100)
        {
            BackendGameData.Instance.UserGameData.experience = 0;
            BackendGameData.Instance.UserGameData.level++;
        }
        BackendGameData.Instance.GameDataUpdate(AfterGameOver);
    }

    public void AfterGameOver()
    {
        Utils.LoadScene(SceneNames.Lobby);
    }
}
