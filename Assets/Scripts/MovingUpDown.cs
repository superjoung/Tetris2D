using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown : MonoBehaviour
{
    public TalkManager TM;
    public bool anchor = true;

    private void Update()
    {
        if (TM.talkStart && anchor)
        {
            StartCoroutine(UpAndDown());
        }
    }
    // TalkBox�ȿ� �ִ� ��ư ������ ������
    IEnumerator UpAndDown()
    {
        anchor = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.GetComponent<RectTransform>().DOAnchorPosY(30f, 1f).SetEase(Ease.Linear));
        seq.Append(transform.GetComponent<RectTransform>().DOAnchorPosY(15f, 0.5f).SetEase(Ease.OutElastic));
        var tween = seq.Play();
        yield return tween.WaitForCompletion();
        anchor = true;
    }
}
