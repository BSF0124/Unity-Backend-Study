using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnCycleTime;

    private void Awake()
    {
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        int enemyCount = 5;
        float distance = 1.2f;
        float firstX = -2.4f;

        while(true)
        {
            for(int i = 0; i < enemyCount; i++)
            {
                Vector3 position = new Vector3(firstX + distance * i, stageData.LimitMax.y + 1, 0);
                GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.GetComponent<Enemy>().Setup(gameController);
            }

            yield return new WaitForSeconds(spawnCycleTime);
        }
    }
}
