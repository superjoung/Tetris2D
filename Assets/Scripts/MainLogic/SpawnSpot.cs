using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;

public class SpawnSpot : MonoBehaviour
{
    public int selectLevelNum; //������ ������ n x n ��
    public float blockWidth;

    public Vector3 startSpawnPoint; //���� ������ ����ϴ� ������ ��ġ

    public GameObject spawnSpotPrefab;
    public GameObject offPrefab;
    [SerializeField]
    public GameObject[,] offPrefabs;
    public List<GameObject> offPrefabObj = new List<GameObject>(9);

    // UI Panel
    public GameObject inGameUIPanel;

    public GameManager GM;
    public EnemySpawn ES;

    // ���� �÷��̾� ������ ���� �Լ�
    public void SpotSpawn()
    {
        spawnSpotPrefab = GameObject.Find("SpawnSpot");
        offPrefabs = new GameObject[selectLevelNum, selectLevelNum];

        //¦�� 0.39 �ʱⰪ 2�������� ���� 0.78 ������
        if (selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-blockWidth/2 + (-blockWidth * (selectLevelNum / 2 - 1))), (float)(blockWidth/2 + (blockWidth * (selectLevelNum / 2 - 1))), 0) + spawnSpotPrefab.transform.position;

        //Ȧ�� 0.78 �ʱⰪ 2�������� ���� 0.78 ������
        else
            startSpawnPoint = new Vector3((float)(-blockWidth + (-blockWidth * (selectLevelNum / 2 - 1))), (float)(blockWidth + (blockWidth * (selectLevelNum / 2 - 1))), 0) + spawnSpotPrefab.transform.position;

        for (int i = 0; i < selectLevelNum; i++)
        {
            for (int j = 0; j < selectLevelNum; j++)
            {
                offPrefabs[i, j] = Instantiate(offPrefabObj[Random.Range(0, 8)], new Vector3(startSpawnPoint.x + (float)blockWidth * j, startSpawnPoint.y, 0), Quaternion.identity);
                //offPrefabs[i, j] = Instantiate(offPrefab, new Vector3(startSpawnPoint.x + (float)0.78 * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                offPrefabs[i, j].transform.parent = spawnSpotPrefab.transform;
            }
            startSpawnPoint += new Vector3(0, (float)-blockWidth, 0);
        }
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, ES.popSpeed).SetEase(Ease.OutElastic);
        inGameUIPanel.SetActive(true);
    }
}
