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
    public GameObject LSpawnSpot;
    public GameObject RSpawnSpot;
    public GameObject PSpawnSpot; //x축 4 차증감
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

    //player보드판 생성 로직
    void SpawnEnemy()
    {
        enemySpawn = new GameObject[SSp.selectLevelNum, SSp.selectLevelNum];
        count = SSp.selectLevelNum * SSp.selectLevelNum;
        startSpawnPoint = transform.position;

        if (SSp.selectLevelNum % 2 == 0)
            startSpawnPoint = new Vector3((float)(-0.39 + (-0.78 * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(0.39 + (0.78 * (SSp.selectLevelNum / 2 - 1))), 0);

        else
            startSpawnPoint = new Vector3((float)(-0.78 + (-0.78 * (SSp.selectLevelNum / 2 - 1)) + LSpawnSpot.transform.position.x), (float)(0.78 + (0.78 * (SSp.selectLevelNum / 2 - 1))), 0);

        //랜덤블럭 생성 구간
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
        GameManager.gameStart = true; //본격적인 게임 시작 부분
        StartCoroutine(PopSpawnSpot());
    }

    //manager에 speed 및 클리어 갯수에 따라 함수 실행
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(Lv1());
        }
    }

    //왼쪽 스폰 지역에만 블록 생성
    //게임 시작의 발포
    IEnumerator Lv1()
    {
        var tween = PSpawnSpot.transform.DOMoveX(4, popSpeed).SetEase(Ease.OutElastic);
        yield return tween.WaitForCompletion();
        SpawnEnemy();
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
        }
    }
}
