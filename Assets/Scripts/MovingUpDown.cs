using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown : MonoBehaviour
{
    public GameManager GM;
    public bool anchor = true;

    private void Update()
    {
        if (GM.talkStart && anchor)
        {
            StartCoroutine(UpAndDown());
        }
    }
    // TalkBox안에 있는 버튼 움직임 시퀀스
    IEnumerator UpAndDown()
    {
        anchor = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.GetComponent<RectTransform>().DOAnchorPosY(-15f, 1f).SetEase(Ease.Linear));
        seq.Append(transform.GetComponent<RectTransform>().DOAnchorPosY(-30f, 0.5f).SetEase(Ease.OutElastic));
        var tween = seq.Play();
        yield return tween.WaitForCompletion();
        anchor = true;
    }
}
