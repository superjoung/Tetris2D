using DG.Tweening;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    //public GameManager Manager;
    public float popSpeed;
    public float destorySpeed;
    public GameObject LSpawnSpot;
    public GameObject RSpawnSpot;
    public GameObject PSpawnSpot; //x�� 4 ������
    public GameObject onPrefab;
    public GameObject[,] enemySpawn;
    public Vector3 startSpawnPoint;
    public SpawnSpot SSp;
    public GameManager GameManager;

    public int count;

    void Start()
    {
        PSpawnSpot = GameObject.Find("SpawnSpot");
        LSpawnSpot = GameObject.Find("LeftSpot");
        RSpawnSpot = GameObject.Find("RightSpot");
    }

    void SprayFrefabs()
    {
        for(int i = 0; i < SSp.selectLevelNum; i++)
        {
            for(int j = 0; j < SSp.selectLevelNum; j++)
            {
                if(enemySpawn[i, j] != null)
                {
                    Vector3 suvPos = enemySpawn[i, j].transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                    enemySpawn[i, j].transform.DOMove(suvPos, destorySpeed).SetEase(Ease.OutQuint);
                }
                Vector3 suvPos2 = SSp.offPrefabs[i, j].transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                SSp.offPrefabs[i, j].transform.DOMove(suvPos2, destorySpeed).SetEase(Ease.OutQuint);
            }
        }
    }

    //player������ ���� ����
    void SpawnEnemy()
    {
        enemySpawn = new GameObject[SSp.selectLevelNum, SSp.selectLevelNum];
        count = SSp.selectLevelNum * SSp.selectLevelNum;
        startSpawnPoint = transform.position;

        if (SSp.selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-0.39 + (-0.78 * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(0.39 + (0.78 * (SSp.selectLevelNum / 2 - 1))), 0);

        else
            startSpawnPoint = new Vector3((float)(-0.78 + (-0.78 * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(0.78 + (0.78 * (SSp.selectLevelNum / 2 - 1))), 0);

        //������ ���� ����
        for (int i = 0; i < SSp.selectLevelNum; i++)
        {
            for (int j = 0; j < SSp.selectLevelNum; j++)
            {
                if (Random.Range(0f, 100f) > (100 / SSp.selectLevelNum) * (j + 1))
                {
                    count--;
                    enemySpawn[i, j] = Instantiate(onPrefab, new Vector3(startSpawnPoint.x + (float)0.78 * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                    enemySpawn[i, j].transform.parent = LSpawnSpot.transform;
                    SSp.offPrefabs[i, j].tag = "onPrefab";
                }
                else
                    break;
            }
            startSpawnPoint += new Vector3(0, (float)-0.78, 0);
        }
        GameManager.gameStart = true; //�������� ���� ���� �κ�
        StartCoroutine(PopSpawnSpot());
    }

    //manager�� speed �� Ŭ���� ������ ���� �Լ� ����
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(Lv1());
        }
    }

    //���� ���� �������� ��� ����
    //���� ������ ����
    IEnumerator Lv1()
    {
        var tween = PSpawnSpot.transform.DOMoveX(4, popSpeed).SetEase(Ease.OutElastic); //PSpawnSpot ���������� �̵�
        yield return tween.WaitForCompletion();
        SpawnEnemy(); //pop �����ϴ��Լ�
    }

    IEnumerator PopSpawnSpot()
    {
        LSpawnSpot.transform.localScale = Vector3.zero;

        var tween = LSpawnSpot.transform.DOScale(new Vector3(1, 1, 1), popSpeed).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(popSpeed);
        StartCoroutine(MoveLSpot());
    }

    IEnumerator MoveLSpot()
    {
        var tween = LSpawnSpot.transform.DOMoveX(transform.position.x + 1, GameManager.gameSpeed).SetEase(Ease.OutElastic);
        Debug.Log("resume Corution");
        yield return tween.WaitForCompletion();
        if (LSpawnSpot.transform.position.x != PSpawnSpot.transform.position.x && count != 0)
        {
            Debug.Log("resume Corution");
            StartCoroutine(MoveLSpot());
        }
        else if(count == 0 && GameManager.gameStart)
        {
            GameManager.gameStart = false;
            LSpawnSpot.transform.DOMoveX(PSpawnSpot.transform.position.x, 0.3f).SetEase(Ease.InCubic);
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(CompleteSpot());
        }
    }

    IEnumerator CompleteSpot()
    {
        Sequence destorySeq = DOTween.Sequence();
        destorySeq.Append(LSpawnSpot.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), destorySpeed)).SetEase(Ease.InSine);
        destorySeq.Join(PSpawnSpot.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), destorySpeed)).SetEase(Ease.InSine);
        destorySeq.Append(LSpawnSpot.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), destorySpeed)).SetEase(Ease.InSine);
        destorySeq.Join(PSpawnSpot.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), destorySpeed)).SetEase(Ease.InSine);
        destorySeq.Append(LSpawnSpot.transform.DOScale(new Vector3(1, 1, 1), destorySpeed)).SetEase(Ease.InSine);
        destorySeq.Join(PSpawnSpot.transform.DOScale(new Vector3(1, 1, 1), destorySpeed)).SetEase(Ease.InSine);
        var tween = destorySeq.Play();
        yield return tween.WaitForCompletion();
        SprayFrefabs();
    }
}
