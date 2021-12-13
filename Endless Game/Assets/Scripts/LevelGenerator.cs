using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float movingSpeed = 12;
    public int sectionsToPreSpawn = 15;
    public List<GameObject> spawnedSections;

    private void Start()
    {
        spawnedSections = new List<GameObject>();

        GameObject prevSection = ObjectPooler.SharedInstance.GetPooledObject();
        prevSection.SetActive(true);

        GameObject nextSection = ObjectPooler.SharedInstance.GetPooledObject();
        nextSection.SetActive(true);

        nextSection.transform.position = prevSection.GetComponent<Section>().endPoint[0].position - nextSection.GetComponent<Section>().startPoint.position;

        spawnedSections.Add(prevSection);
        spawnedSections.Add(nextSection);
    }

    private void Update()
    {
        if (Camera.main.WorldToViewportPoint(spawnedSections[0].GetComponent<Section>().endPoint[0].position).z < 0)
        {
            GameObject nextSection = ObjectPooler.SharedInstance.GetRandomPooledObject();
            spawnedSections[0].SetActive(false);
            spawnedSections.RemoveAt(0);
            print(spawnedSections[0].name);
            nextSection.transform.position = spawnedSections[0].GetComponent<Section>().endPoint[0].position;
            nextSection.transform.rotation = spawnedSections[0].GetComponent<Section>().endPoint[0].rotation;
            float distance = (nextSection.GetComponent<Section>().startPoint.position - nextSection.transform.position).magnitude;
            nextSection.transform.position += nextSection.transform.forward * distance;
            nextSection.SetActive(true);
            spawnedSections.Add(nextSection);
        }
    }
}
