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
        // 인트로1
        talkDate.Add(100, new string[]
        {"큰일이야 큰일이야! 지금 게임을 준비하던 오브젝트가 주요 문서를 들고 도망갔어",
         "이름이 뭐더라... (뒤적뒤적)폴더 이름이 과제?.. 라고 되어있던데",
         "아무튼! 아직 멀지않은 곳에 있을테니까 앞에 있는 벽을 부수고 쫒아가야해",
         "근데 문제가 하나있어.. 우리가 가지고 있는건 예쁜 정사각형을 부쉬는 도구밖에 없어",
         "여기서 너의 도움이 필요해.. 너가 벽을 만들어줄래?"});

        // 처음 Player보드 소환 후 설명
        talkDate.Add(101, new string[] 
        {"너가 완성해야 하는 벽의 도안이야",
         "화면을 터치하거나 누른 상태에서 움직이면 자동으로 블럭이 채워져",
         "한번 해볼래?"});

    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }
}
