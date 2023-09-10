using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkDate;

    void Start()
    {
        talkDate = new Dictionary<int, string[]>();
        Generator();
    }

    void Generator()
    {
        // TestCase
        talkDate.Add(1000, new string[]
        {"ū���̾� ū���̾�! ���� ������ �غ��ϴ� ������Ʈ�� �ֿ� ������ ��� ��������",
         "�̸��� ������... (��������)���� �̸��� ����?.. ��� �Ǿ��ִ���",
         "�ƹ�ư! ���� �������� ���� �����״ϱ� �տ� �ִ� ���� �μ��� �i�ư�����",
         "�ٵ� ������ �ϳ��־�.. �츮�� ������ �ִ°� ���� ���簢���� �ν��� �����ۿ� ����",
         "���⼭ ���� ������ �ʿ���.. �ʰ� ���� ������ٷ�?"});
    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }
}
