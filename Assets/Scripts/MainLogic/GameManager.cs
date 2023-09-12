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

    public int tCount;
    public bool tStart;

    // HP 변경시 필요한 변수
    public Text hpUIText;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameStart)
            HpManage();
    }

    // x축 60 +
    public void HpManage()
    {
        hpUIText.text = gameLife + " / " + "3";
    }
}
