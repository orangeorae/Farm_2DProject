using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Player_AnimController;

public class Player : MonoBehaviour
{
    [SerializeField] private Player_AnimController Player_animController;
    [SerializeField] private float moveSpeed = 5f;
    public JoyStick joyStick;
    public Button harvestBtn;
    private bool isHarvest = false;

    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조 

    private void OnEnable()
    {
        harvestBtn.onClick.AddListener(Onclick_Harvest);
    }

    private void OnDisable()
    {
        harvestBtn.onClick.RemoveAllListeners();
    }
    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isHarvest) return;
        Player_AnimMove();
    }

    private void Player_AnimMove()
    {
        Vector2 direction = joyStick.GetDirection(); // 조이스틱으로 입력 받은 방향 가져옴 

        if (direction.magnitude > 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Walk);
            Vector3 move_Trans = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime; // 실제 이동할 거리 계산

            transform.Translate(move_Trans); // 캐릭터 이동 

            if (direction.x > 0f)
            {
                spriteRenderer.flipX = false;
            }
           // 이미지를 좌우 반전 (flipX = true)
            else if (direction.x < 0f)
            {
                spriteRenderer.flipX = true;
            }
        }

        else if( direction.magnitude == 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
        }
    }

    public void Onclick_Harvest()
    {
        isHarvest = true;
        Player_animController.SetPlayerAnimState(Player_AnimState.Harvest);

        Invoke(nameof(End_Harvest), 0.7f); 
        //nameof(EndHarvest) == "EndHarvest"  -> 나중에 함수 이름을 바꿨을 때 에러날 경우를 대비해서
    }

    private void End_Harvest()
    {
        isHarvest = false;
        Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
    }

}