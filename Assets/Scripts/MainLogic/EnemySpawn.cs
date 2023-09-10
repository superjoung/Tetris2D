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
    public float moveSpeed;
    public float blockWdith;
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

    void SprayFrefabs() // 7
    {
        for(int i = 0; i < SSp.selectLevelNum; i++)
        {
            for(int j = 0; j < SSp.selectLevelNum; j++)
            {
                if(enemySpawn[i, j] != null)
                {
                    Vector3 suvPos = enemySpawn[i, j].transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                    enemySpawn[i, j].transform.DOMove(suvPos, destorySpeed).SetEase(Ease.OutQuint);
                    enemySpawn[i, j].transform.DOScale(Vector3.zero, destorySpeed).SetEase(Ease.OutQuint);
                }
                Vector3 suvPos2 = SSp.offPrefabs[i, j].transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                SSp.offPrefabs[i, j].transform.DOMove(suvPos2, destorySpeed).SetEase(Ease.OutQuint);
                SSp.offPrefabs[i, j].transform.DOScale(Vector3.zero, destorySpeed).SetEase(Ease.OutQuint);
            }
        }

        for(int i = 0; i < SSp.selectLevelNum; i++)
        {
            for(int j = 0; j < SSp.selectLevelNum; j++)
            {
                enemySpawn[i, j] = null;
                SSp.offPrefabs[i, j] = null;
            }
        }

        StartCoroutine(OnWaitForSecond(destorySpeed));
    }

    void DestroyChildObj() // 9
    {
        for (int i = 0; i < LSpawnSpot.transform.childCount; i++)
        {
            Destroy(LSpawnSpot.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PSpawnSpot.transform.childCount; i++)
        {
            Destroy(PSpawnSpot.transform.GetChild(i).gameObject);
        }

        PSpawnSpot.transform.DetachChildren();
        LSpawnSpot.transform.DetachChildren();
        GameManager.gameScore++;
        StartCoroutine(Lv1());
    }

    //player������ ���� ����
    void SpawnEnemy() // 3
    {
        enemySpawn = new GameObject[SSp.selectLevelNum, SSp.selectLevelNum];
        count = SSp.selectLevelNum * SSp.selectLevelNum;
        startSpawnPoint = transform.position;

        if (SSp.selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-blockWdith/2 + (-blockWdith * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(blockWdith/2 + (blockWdith * (SSp.selectLevelNum / 2 - 1))), 0);

        else
            startSpawnPoint = new Vector3((float)(-blockWdith + (-blockWdith * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(blockWdith + (blockWdith * (SSp.selectLevelNum / 2 - 1))), 0);

        //������ ���� ����
        for (int i = 0; i < SSp.selectLevelNum; i++)
        {
            for (int j = 0; j < SSp.selectLevelNum; j++)
            {
                if (Random.Range(0f, 100f) > (100 / SSp.selectLevelNum) * (j + 1))
                {
                    count--;
                    enemySpawn[i, j] = Instantiate(onPrefab, new Vector3(startSpawnPoint.x + (float)blockWdith * j, startSpawnPoint.y, 0), new Quaternion(0, 0, 0, 0));
                    enemySpawn[i, j].transform.parent = LSpawnSpot.transform;
                    SSp.offPrefabs[i, j].tag = "onPrefab";
                }
                else
                    break;
            }
            startSpawnPoint += new Vector3(0, (float)-blockWdith, 0);
        }
        GameManager.gameStart = true; //�������� ���� ���� �κ�
        StartCoroutine(PopSpawnSpot());
    }

    //manager�� speed �� Ŭ���� ������ ���� �Լ� ����
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W)) // 1
        {
            Debug.Log("Start");
            StartCoroutine(Lv1());
        }
    }

    //���� ���� �������� ��� ����
    //���� ������ ����
    IEnumerator Lv1() // 2
    {
        if (GameManager.gameScore == 0)
        {
            SSp.SpotSpawn();
            var tween = PSpawnSpot.transform.DOMoveX(4, popSpeed).SetEase(Ease.OutElastic); //PSpawnSpot ���������� �̵�
            yield return tween.WaitForCompletion();
        }
        else
        {
            LSpawnSpot.transform.position = new Vector3(-5, 0, 0);
            PSpawnSpot.transform.position = new Vector3(4, 0, 0);
            SSp.SpotSpawn();
            yield return null;
        }
        SpawnEnemy(); //pop �����ϴ��Լ�
    }

    IEnumerator PopSpawnSpot() // 4
    {
        LSpawnSpot.transform.localScale = Vector3.zero;

        var tween = LSpawnSpot.transform.DOScale(new Vector3(1, 1, 1), popSpeed).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(popSpeed);
        StartCoroutine(MoveLSpot());
    }

    IEnumerator MoveLSpot() // 5
    {
        var tween = LSpawnSpot.transform.DOMoveX(transform.position.x + 1, GameManager.gameSpeed).SetEase(Ease.OutElastic);
        Debug.Log("resume Corution");
        yield return tween.WaitForCompletion();
        if (LSpawnSpot.transform.position.x != PSpawnSpot.transform.position.x && count != 0) //������
        {
            Debug.Log("resume Corution");
            StartCoroutine(MoveLSpot());
        }
        else if(LSpawnSpot.transform.position.x == PSpawnSpot.transform.position.x) //�����߱� ����
        {
            GameManager.gameLife--;
            GameManager.gameStart = false;
            StartCoroutine(CompleteSpot());
            StartCoroutine(CameraMoving());
        }
        else if(count == 0 && GameManager.gameStart) //�����߱� ����
        {
            GameManager.gameStart = false;
            LSpawnSpot.transform.DOMoveX(PSpawnSpot.transform.position.x, moveSpeed).SetEase(Ease.InCubic);
            yield return new WaitForSeconds(moveSpeed);
            StartCoroutine(CompleteSpot());
        }
    }

    IEnumerator CompleteSpot() // 6
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

    IEnumerator OnWaitForSecond(float timeSet) // 8
    {
        yield return new WaitForSeconds(timeSet);
        DestroyChildObj();
    }

    IEnumerator CameraMoving() // �� ������ ������ �� ī�޶� ������
    {
        Sequence Seq = DOTween.Sequence();
        Seq.Append(Camera.main.transform.DOMoveX(Random.Range(-0.5f, 0.5f), 0.1f).SetEase(Ease.OutElastic));
        Seq.Join(Camera.main.transform.DOMoveY(Random.Range(-0.5f, 0.5f), 0.1f).SetEase(Ease.OutElastic));
        Seq.Append(Camera.main.transform.DOMoveX(Random.Range(-0.5f, 0.5f), 0.1f).SetEase(Ease.OutElastic));
        Seq.Join(Camera.main.transform.DOMoveY(Random.Range(-0.5f, 0.5f), 0.1f).SetEase(Ease.OutElastic));
        Seq.Append(Camera.main.transform.DOMoveX(0, 0.1f).SetEase(Ease.OutElastic));
        Seq.Join(Camera.main.transform.DOMoveY(0, 0.1f).SetEase(Ease.OutElastic));
        var tween = Seq.Play();
        yield return tween.WaitForCompletion();
    }
}
