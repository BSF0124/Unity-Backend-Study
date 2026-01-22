using System.Collections;
using TMPro;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float effectTime;
    private TextMeshProUGUI effectText;

    private void Awake()
    {
        effectText = GetComponent<TextMeshProUGUI>();
    }

    public void Play(float start, float end)
    {
        StartCoroutine(Process(start, end));
    }

    private IEnumerator Process(float start, float end)
    {
        float current = 0;
        float percnet = 0;

        while (percnet < 1)
        {
            current += Time.deltaTime;
            percnet = current / effectTime;

            effectText.fontSize = Mathf.Lerp(start, end, percnet);

            yield return null;
        }
    }
}
