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

    public int tCount;
    public bool tStart;

    // HP ����� �ʿ��� ����
    public Text hpUIText;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameStart)
            HpManage();
    }

    // x�� 60 +
    public void HpManage()
    {
        hpUIText.text = gameLife + " / " + "3";
    }
}
