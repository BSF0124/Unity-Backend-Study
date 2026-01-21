using System.Collections;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject meteoritePrefab;
    [SerializeField]
    private float minSpawnCycleTime = 1;
    [SerializeField]
    private float maxSpawnCycleTime = 4;

    private void Awake()
    {
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        while(true)
        {
            float spawnCycleTime = Random.Range(minSpawnCycleTime, maxSpawnCycleTime);
            yield return new WaitForSeconds(spawnCycleTime);

            float x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(x,0,0), Quaternion.identity);
            yield return new WaitForSeconds(1);

            Destroy(alertLineClone);

            GameObject meteorite = Instantiate(meteoritePrefab, new Vector3(x, stageData.LimitMax.y+1, 0), Quaternion.identity);
            meteorite.GetComponent<Meteorite>().Setup(gameController);
        }
    }
}
