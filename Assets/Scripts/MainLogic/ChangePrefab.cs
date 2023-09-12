using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
    public RaycastHit2D hit;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer offSpriteRender;
    public Sprite offPrefabSprite;
    public Sprite onPrefabSprite;

    [SerializeField]
    public EnemySpawn ES;
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        ES = GameObject.Find("LeftSpot").GetComponent<EnemySpawn>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        offSpriteRender = gameObject.transform.GetComponent<SpriteRenderer>();
        offPrefabSprite = offSpriteRender.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (GM.gameStart || GM.tStart)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            SelectOnCollider();
        }
    }

    private void OnMouseEnter()
    {
        HitOnSelected();
    }

    void HitOnSelected()
    {
        if (Input.GetMouseButton(0) && (GM.gameStart || GM.tStart))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            SelectOnCollider();
        }
    }

    void SelectOnCollider()
    {
        //�ٲ�� �ϴ°� tag, sprite ���� üũ �ƴ��� Ȯ���� ��� tag�� on���� �ٲ���°� �׷� ������ �ȵǴ� ����? �׳� tag�� on���� �ٲٰ� sprite�� �ȹٲ۴�.
        //�׷� �ᱹ CompareTag�Ӹ� �ƴ϶� sprite�� Ȯ������� �Ѵ�. ���ǽ����� �����ϰ� ����� �ɵ�

        if (hit.collider.CompareTag("offPrefab"))
        {
            //�±� ����
            hit.collider.tag = "onPrefab";
            GM.tCount++;
            ES.count--;
            spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite.name == onPrefabSprite.name)
            {
                spriteRenderer.sprite = offPrefabSprite;
            }

            else
            {
                spriteRenderer.sprite = onPrefabSprite;
            }
        }

        else if (hit.collider.CompareTag("onPrefab"))
        {
            hit.collider.tag = "offPrefab";
            GM.tCount--;
            ES.count++;
            spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite.name == onPrefabSprite.name)
            {
                spriteRenderer.sprite = offPrefabSprite;
            }

            else
            {
                spriteRenderer.sprite = onPrefabSprite;
            }
        }
    }
}
