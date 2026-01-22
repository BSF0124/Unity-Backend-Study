using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [Header("InGame")]
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("Game Over")]
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private TextMeshProUGUI textResultScore;

    [Header("Game Over UI Animation")]
    [SerializeField]
    private ScaleEffect effectGameOver; 
    [SerializeField] 
    private CountingEffect effectResultScore;

    private void Update()
    {
        textScore.text = $"SCORE {gameController.Score}";
    }

    public void OnGameOver()
    {
        panelGameOver.SetActive(true);
        textResultScore.text = gameController.Score.ToString();
        effectGameOver.Play(200, 100);
        effectResultScore.Play(0, gameController.Score);
    }

    public void BtnClickGoToLobby()
    {
        Utils.LoadScene(SceneNames.Lobby);
    }
}
