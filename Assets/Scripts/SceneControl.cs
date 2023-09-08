using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneControl : MonoBehaviour
{
    public float popSpeed;
    public GameObject OptionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitOptionPanel()
    {
        OptionPanel.SetActive(false);
    }

    public void ChickButton()
    {
        GameObject chickObj = EventSystem.current.currentSelectedGameObject;
        StartCoroutine(TouchBound(chickObj));
    }

    IEnumerator TouchBound(GameObject boundObj)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(boundObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), popSpeed)).SetEase(Ease.OutElastic);
        seq.Append(boundObj.transform.DOScale(new Vector3(1, 1, 1), 0.2f)).SetEase(Ease.OutQuint);
        var tween = seq.Play();
        yield return tween.WaitForCompletion();
        if (boundObj.transform.CompareTag("GameStart"))
        {
            Debug.Log("Game Start");
        }

        else if (boundObj.transform.CompareTag("GameOption"))
        {
            Debug.Log("Game Option");
            OptionPanel.SetActive(true);
        }

        else if (boundObj.transform.CompareTag("GameTutorial"))
        {
            Debug.Log("Game Tutorial");
        }
    }
}
