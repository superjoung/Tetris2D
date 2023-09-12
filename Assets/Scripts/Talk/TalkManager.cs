using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public int                  tutorialNum;    // id 카운트 업
    public int                  talkIndex;      // 남아있는 sting 갯수 카운팅
    public bool                 talkStart;      // 텍스트박스 시작 여부
    public bool                 tutorialStart;  // 튜토리얼 시작
    public GameObject           talkPanel;      // 텍스트 패널
    public GameObject           PSpawnSpot;
    public GameObject           LSpawnSpot;
    public Text                 UITalkText;     // 메인텍스트 출력

    // 스크립트 선언
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

        if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
        {
            talkStart = false;
            tutorialNum++;
            talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화해야함
            if(tutorialNum < 102)
                ProgressTutorial();
            if(tutorialNum == 104)
                SceneManager.LoadScene(0);
            return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
        }

        if (isBoss)
        {
            UITalkText.text = talkData;
        }
        else
        {
            UITalkText.text = talkData;
        }

        //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
        talkStart = true; //대사가 남아있으므로 계속 진행되어야함 
        talkIndex++;
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
         "한번 블럭을 전부 채워볼래?"});

        talkDate.Add(102, new string[]
        {"와! 정말 대단해 완성된 벽들은 우리가 도구를 이용해 부술 수 있게돼",
         "그럼 이번에는 다른 조각들을 보고 한번 퍼즐을 완성 시켜 보겠어?",
         "대부분 조각들은 왼쪽에서 다가올거야",
         "조각이 다가올때 까지 완성하지 못하면 도구 오작동으로 다시 조각으로 돌아가버리니",
         "꼭! 조심해줘! 그럼 가볍게 5개만 성공시켜볼까?",
         "준비가 완료되는대로 \"W\"를 눌러봐!"});

        talkDate.Add(103, new string[]
        {"언블리버블! 5개를 전부 클리어하다니 도둑놈을 빨리 잡을 수 있겠는걸?",
         "하지만 조심해 각 스테이지마다 기존에 존재하는 보스들 마저 잠식당해서",
         "우리를 방해할려고 할거야.. 걱정하지마! 각 보스들의 능력들은 이미 파악했으니까!",
         "아차차! 지금 이러고 있을 시간이 없어 바로 스테이지룸으로 보내줄게",
         "준비가 끝나는대로 바로 출발해보자고!"});
    }

    public string GetTalk(int date, int talkIndex)
    {
        if (talkIndex == talkDate[date].Length)
            return null;
        else
            return talkDate[date][talkIndex];
    }

    // id = 102 서브 함수
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
