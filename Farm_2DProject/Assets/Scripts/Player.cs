using Unity.VisualScripting;
using UnityEngine;
using static Player_AnimController;

public class Player : MonoBehaviour
{
    [SerializeField] private Player_AnimController Player_animController;
    [SerializeField] private float moveSpeed = 5f;
    public JoyStick joyStick;

    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조 

    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Player_AnimMove();
    }

    private void Player_AnimMove()
    {
        Vector2 direction = joyStick.GetDirection(); // 조이스틱으로 입력 빋은 방향 가져옴 

        if (direction.magnitude > 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Walk);
            Vector3 move_Trans = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime; // 실제 이동할 거리 계산

            transform.Translate(move_Trans); // 캐릭터 이동 
        }

        else
        {

            Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
        }
    }
}