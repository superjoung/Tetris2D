using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public int                  tutorialNum;    // id ī��Ʈ ��
    public int                  talkIndex;      // �����ִ� sting ���� ī����
    public bool                 talkStart;      // �ؽ�Ʈ�ڽ� ���� ����
    public bool                 tutorialStart;  // Ʃ�丮�� ����
    public GameObject           talkPanel;      // �ؽ�Ʈ �г�
    public GameObject           PSpawnSpot;
    public GameObject           LSpawnSpot;
    public Text                 UITalkText;     // �����ؽ�Ʈ ���

    // ��ũ��Ʈ ����
    public SpawnSpot            spawnSpot;
    public MovingUpDown         movingUpDown;
    public EnemySpawn           enemySpawn;
    public GameManager          gameManager;

    Dictionary<int, string[]> talkDate;

    void Start()
    {
        talkDate = new Dictionary<int, string[]>();
        Generator();
        talkStart = true;
        tutorialNum = 100;
        gameManager.tCount = 0;
        StartTalk(tutorialNum);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && talkStart)
        {
            StartTalk(tutorialNum);
        }
        SuvTutorial_102();
        SuvTutorial_103();
    }

    public void StartTalk(int id)
    {
        if (talkStart)
        {
            Talk(id, false);
        }
        talkPanel.SetActive(talkStart);
    }

    public void Talk(int id, bool isBoss)
    {
        string talkData = GetTalk(id, talkIndex);

        if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
        {
            talkStart = false;
            tutorialNum++;
            talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ�ؾ���
            if(tutorialNum < 102)
                ProgressTutorial();
            if(tutorialNum == 104)
                SceneManager.LoadScene(0);
            return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
        }

        if (isBoss)
        {
            UITalkText.text = talkData;
        }
        else
        {
            UITalkText.text = talkData;
        }

        //���� ������ �������� ���� talkData�� �ε����� �ø�
        talkStart = true; //��簡 ���������Ƿ� ��� ����Ǿ���� 
        talkIndex++;
    }

    void Generator()
    {
        // ��Ʈ��1
        talkDate.Add(100, new string[]
        {"ū���̾� ū���̾�! ���� ������ �غ��ϴ� ������Ʈ�� �ֿ� ������ ��� ��������",
         "�̸��� ������... (��������)���� �̸��� ����?.. ��� �Ǿ��ִ���",
         "�ƹ�ư! ���� �������� ���� �����״ϱ� �տ� �ִ� ���� �μ��� �i�ư�����",
         "�ٵ� ������ �ϳ��־�.. �츮�� ������ �ִ°� ���� ���簢���� �ν��� �����ۿ� ����",
         "���⼭ ���� ������ �ʿ���.. �ʰ� ���� ������ٷ�?"});

        // ó�� Player���� ��ȯ �� ����
        talkDate.Add(101, new string[] 
        {"�ʰ� �ϼ��ؾ� �ϴ� ���� �����̾�",
         "ȭ���� ��ġ�ϰų� ���� ���¿��� �����̸� �ڵ����� ���� ä����",
         "�ѹ� ���� ���� ä������?"});

        talkDate.Add(102, new string[]
        {"��! ���� ����� �ϼ��� ������ �츮�� ������ �̿��� �μ� �� �ְԵ�",
         "�׷� �̹����� �ٸ� �������� ���� �ѹ� ������ �ϼ� ���� ���ھ�?",
         "��κ� �������� ���ʿ��� �ٰ��ðž�",
         "������ �ٰ��ö� ���� �ϼ����� ���ϸ� ���� ���۵����� �ٽ� �������� ���ư�������",
         "��! ��������! �׷� ������ 5���� �������Ѻ���?",
         "�غ� �Ϸ�Ǵ´�� \"W\"�� ������!"});

        talkDate.Add(103, new string[]
        {"�������! 5���� ���� Ŭ�����ϴٴ� ���ϳ��� ���� ���� �� �ְڴ°�?",
         "������ ������ �� ������������ ������ �����ϴ� ������ ���� ��Ĵ��ؼ�",
         "�츮�� �����ҷ��� �Ұž�.. ����������! �� �������� �ɷµ��� �̹� �ľ������ϱ�!",
         "������! ���� �̷��� ���� �ð��� ���� �ٷ� �������������� �����ٰ�",
         "�غ� �����´�� �ٷ� ����غ��ڰ�!"});
    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }

    // id = 102 ���� �Լ�
    public void SuvTutorial_102()
    {
        if (tutorialNum == 102)
        {
            if (gameManager.tCount == 16)
            {
                gameManager.tStart = false;
                talkStart = true;
                gameManager.tCount = 0;
                SuvTutorial_102_2();
                ProgressTutorial();
            }
            else
                gameManager.tStart = true;
        }
    }

    public void SuvTutorial_102_2()
    {
        for (int i = 0; i < PSpawnSpot.transform.childCount; i++)
        {
            Destroy(PSpawnSpot.transform.GetChild(i).gameObject);
        }

        PSpawnSpot.transform.DetachChildren();
    }

    public void SuvTutorial_103()
    {
        if (tutorialNum == 103)
        {
            if (gameManager.gameScore == 5)
            {
                Destroy(PSpawnSpot);
                Destroy(LSpawnSpot);
                gameManager.tStart = false;
                talkStart = true;
                gameManager.gameScore = 0;
                ProgressTutorial();
            }
            else
                gameManager.tStart = true;
        }
    }

    public void ProgressTutorial()
    {
        if(tutorialNum == 101)
        {
            spawnSpot.SpotSpawn();
            movingUpDown.anchor = true;
            StartTalk(tutorialNum);
            talkStart = true;
        }

        if(tutorialNum == 102)
        {
            movingUpDown.anchor = true;
            StartTalk(tutorialNum);
            talkStart = true;
        }

        if (tutorialNum == 103)
        {
            movingUpDown.anchor = true;
            StartTalk(tutorialNum);
            talkStart = true;
        }
    }
}
