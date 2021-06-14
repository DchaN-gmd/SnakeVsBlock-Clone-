using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBetweenFullLine;
    [SerializeField] private int _distanceBetweenRandomLine;

    [Header("Block")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _blockSpawnChance;

    [Header("Wall")]
    [SerializeField] private Wall _wallTemplate;
    [SerializeField] private int _wallSpawnChance;

    [Header("Bonus")]
    [SerializeField] private Bonus _bonusTemplate;
    [SerializeField] private int _bonusSpawnChance;

    private BlockPoint[] _blockSpawnPoints;
    private WallPoint[] _wallSpawnPoints;
    private BonusPoint[] _bonusSpawnPoints;

    void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallPoint>();
        _bonusSpawnPoints = GetComponentsInChildren<BonusPoint>();

        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < _repeatCount; i++)
        {
            SpawnerMove(_distanceBetweenFullLine);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenFullLine + 30, 0.4f);
            GenerateRandomElements(_bonusSpawnPoints, _bonusTemplate.gameObject, _bonusSpawnChance, 0.43f);
            GenerateFullLine(_blockSpawnPoints, _blockTemplate.gameObject);
            SpawnerMove(_distanceBetweenRandomLine);
            GenerateRandomElements(_bonusSpawnPoints, _bonusTemplate.gameObject, _bonusSpawnChance, 0.43f);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenRandomLine + 20, 0.1f);
            GenerateRandomElements(_blockSpawnPoints, _blockTemplate.gameObject, _blockSpawnChance);
        }
    }

    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i<spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }

    private void GenerateRandomElements(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance, float scaleY = 1, float offsetY = 0)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0,100)<= spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement, offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawnPoint, GameObject generatedElement, float offsetY=0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
    }

    private void SpawnerMove(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
