using UnityEngine;

public class LaserPatterns : MonoBehaviour
{
    public GameObject[] patterns;

    public float speed = 5f;

    public float startZ = -55.55f;

    public float stopZ = 8f;

    private GameObject currentPattern;

    private Vector3[] startingPositions;

    void Start()
    {
        startingPositions = new Vector3[patterns.Length];

        for (int i = 0; i < patterns.Length; i++)
        {
            startingPositions[i] = patterns[i].transform.position;

            patterns[i].SetActive(false);
        }

        ChooseRandomPattern();
    }

    void Update()
    {
        if (currentPattern == null)
            return;

        currentPattern.transform.position +=
            Vector3.back * speed * Time.deltaTime;

        if (currentPattern.transform.GetChild(0).position.z <= stopZ)
        {
            ChooseRandomPattern();
        }
    }

    void ChooseRandomPattern()
    {
        if (patterns == null || patterns.Length == 0)
        {
            return;
        }

        if (currentPattern != null)
        {
            currentPattern.SetActive(false);
        }

        int randomIndex = Random.Range(0, patterns.Length);

        currentPattern = patterns[randomIndex];

        currentPattern.transform.position = startingPositions[randomIndex];

        currentPattern.SetActive(true);
    }
}