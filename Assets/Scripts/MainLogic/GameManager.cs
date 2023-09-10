using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �ΰ��� �÷��̽� �۵��ϴ� ����
    [SerializeField]
    public int gameScore = 0; //���
    public int gameTime = 0;
    public int gameLife = 3; //���
    public float gameSpeed; //���
    public bool gameStart = false; //���
    public bool talkStart = false;

    // TalkManager�� �ʿ��� ����
    public TalkManager TM;
    public int talkIndex;
    public GameObject firstBossObj;
    public GameObject talkPanel;
    public Text UITalkText;

    // HP ����� �ʿ��� ����
    public Text hpUIText;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            talkStart = true;
            StartTalk(firstBossObj);
        }

        if (Input.GetMouseButtonDown(0) && talkStart)
            StartTalk(firstBossObj);

        HpManage();
    }

    public void StartTalk(GameObject talkObj)
    {
        if (talkStart)
        {
            ObjDate objDate = talkObj.GetComponent<ObjDate>();
            Talk(objDate.id, objDate.isBoss);
        }
        talkPanel.SetActive(talkStart);
    }

    public void Talk(int id, bool isBoss)
    {
        string talkData = TM.GetTalk(id, talkIndex);

        if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
        {
            talkStart = false;
            talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ�ؾ��� 
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

    // x�� 60 +
    public void HpManage()
    {
        hpUIText.text = gameLife + " / " + "3";
    }
}
