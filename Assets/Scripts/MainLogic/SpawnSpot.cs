using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class SpawnSpot : MonoBehaviour
{
    public int selectLevelNum; //������ ������ n x n ��

    public Vector3 startSpawnPoint; //���� ������ ����ϴ� ������ ��ġ

    public GameObject spawnSpotPrefab;
    public GameObject offPrefab;
    [SerializeField]
    public GameObject[,] offPrefabs;
    public List<GameObject> offPrefabObj = new List<GameObject>(9);

    void Start()
    {
        SpotSpawn();
    }

    void Update()
    {
        
    }

    public void SpotSpawn()
    {
        spawnSpotPrefab = GameObject.Find("SpawnSpot");
        offPrefabs = new GameObject[selectLevelNum, selectLevelNum];

        //¦�� 0.39 �ʱⰪ 2�������� ���� 0.78 ������
        if (selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-0.39 + (-0.78 * (selectLevelNum / 2 - 1))), (float)(0.39 + (0.78 * (selectLevelNum / 2 - 1))), 0) + spawnSpotPrefab.transform.position;

        //Ȧ�� 0.78 �ʱⰪ 2�������� ���� 0.78 ������
        else
            startSpawnPoint = new Vector3((float)(-0.78 + (-0.78 * (selectLevelNum / 2 - 1))), (float)(0.78 + (0.78 * (selectLevelNum / 2 - 1))), 0) + spawnSpotPrefab.transform.position;

        for (int i = 0; i < selectLevelNum; i++)
        {
            for (int j = 0; j < selectLevelNum; j++)
            {
                //offPrefabs[i, j] = Instantiate(offPrefabObj[Random.Range(0, 8)], new Vector3(startSpawnPoint.x + (float)0.78 * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                offPrefabs[i, j] = Instantiate(offPrefab, new Vector3(startSpawnPoint.x + (float)0.78 * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                offPrefabs[i, j].transform.parent = spawnSpotPrefab.transform;
            }
            startSpawnPoint += new Vector3(0, (float)-0.78, 0);
        }
    }
}
