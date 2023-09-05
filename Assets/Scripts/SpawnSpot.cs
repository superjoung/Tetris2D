using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class SpawnSpot : MonoBehaviour
{
    public int selectLevelNum; //������ ������ n x n ��

    public Vector3 startSpawnPoint; //���� ������ ����ϴ� ������ ��ġ

    public GameObject spawnSpotPrefab;
    public GameObject offPrefab;
    public GameObject[,] offPrefabs;

    public RaycastHit2D hit;

    void Start()
    {
        SpotSpawn();
    }

    void Update()
    {
        
    }

    void SpotSpawn()
    {
        spawnSpotPrefab = GameObject.Find("SpawnSpot");
        offPrefabs = new GameObject[selectLevelNum, selectLevelNum];

        //¦�� 0.39 �ʱⰪ 2�������� ���� 0.78 ������
        if (selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-0.39 + (-0.78 * (selectLevelNum / 2 - 1))), (float)(0.39 + (0.78 * (selectLevelNum / 2 - 1))), 0);

        //Ȧ�� 0.78 �ʱⰪ 2�������� ���� 0.78 ������
        else
            startSpawnPoint = new Vector3((float)(-0.78 + (-0.78 * (selectLevelNum / 2 - 1))), (float)(0.78 + (0.78 * (selectLevelNum / 2 - 1))), 0);

        for (int i = 0; i < selectLevelNum; i++)
        {
            for (int j = 0; j < selectLevelNum; j++)
            {
                offPrefabs[i, j] = Instantiate(offPrefab, new Vector3(startSpawnPoint.x + (float)0.78 * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                offPrefabs[i, j].transform.parent = spawnSpotPrefab.transform;
            }
            startSpawnPoint += new Vector3(0, (float)-0.78, 0);
        }
    }

    void HitOnSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            SelectOnCollider();    
        }
    }

    void SelectOnCollider()
    {
        if (hit.collider.CompareTag("offPrefab"))
        {

        }

        else if (hit.collider.CompareTag("onPrefab"))
        {

        }
    }
}