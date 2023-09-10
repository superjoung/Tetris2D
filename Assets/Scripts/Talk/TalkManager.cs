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
        { "미지에 세계의 온걸 환영한다.", 
          "너는 이 세계를 구하기 위해 온 용사다." });
    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }
}
