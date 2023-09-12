using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    public GameObject TalkPanel;
    public bool talkStart = false;

    public TalkManager TM;
    public int talkIndex;
    public Text UITalkText;

    private void Update()
    {

    }

    public void StartTalk(GameObject talkObj)
    {
        if (talkStart)
        {
            ObjDate objDate = talkObj.GetComponent<ObjDate>();
            Talk(objDate.id, objDate.isBoss);
        }
        TalkPanel.SetActive(talkStart);
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
}
