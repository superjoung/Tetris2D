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
         "�ѹ� �غ���?"});

    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }
}
