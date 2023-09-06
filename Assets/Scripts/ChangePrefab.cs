using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
    public RaycastHit2D hit;

    public SpriteRenderer spriteRenderer;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (GM.gameStart)
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
        if (Input.GetMouseButton(0) && GM.gameStart)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            SelectOnCollider();
        }
    }

    void SelectOnCollider()
    {
        //바꿔야 하는것 tag, sprite 전부 체크 됐는지 확인은 모든 tag가 on으로 바뀌었는가 그럼 누르면 안되는 블럭은? 그냥 tag를 on으로 바꾸고 sprite를 안바꾼다.
        //그럼 결국 CompareTag뿐만 아니라 sprite도 확인해줘야 한다. 조건식으로 간단하게 만들면 될듯

        if (hit.collider.CompareTag("offPrefab"))
        {
            //태그 변경
            hit.collider.tag = "onPrefab";
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
