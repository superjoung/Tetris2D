using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 인게임 플레이시 작동하는 변수
    [SerializeField]
    public int gameScore = 0; //사용
    public int gameTime = 0;
    public int gameLife = 3; //사용
    public float gameSpeed; //사용
    public bool gameStart = false; //사용
    public bool talkStart = false;

    // TalkManager에 필요한 변수
    public TalkManager TM;
    public int talkIndex;
    public GameObject firstBossObj;
    public GameObject talkPanel;
    public Text UITalkText;

    // HP 변경시 필요한 변수
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

        if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
        {
            talkStart = false;
            talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화해야함 
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

    // x축 60 +
    public void HpManage()
    {
        hpUIText.text = gameLife + " / " + "3";
    }
}
