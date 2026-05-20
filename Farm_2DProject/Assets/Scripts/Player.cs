using UnityEngine;
using static Player_AnimController;

public class Player : MonoBehaviour
{
    [SerializeField] private Player_AnimController Player_animController;
    [SerializeField] private float moveSpeed = 5f;


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
        float move_AD = Input.GetAxisRaw("Horizontal");

        float move_WS = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(move_AD, move_WS);

        dir.Normalize(); // 모든 방향의 속도가 같도록 정규화

        dir = transform.TransformDirection(dir);
        if (move_AD != 0 || move_WS != 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Walk);
            transform.position = transform.position + dir *moveSpeed * Time.deltaTime;

            if(move_AD > 0)
            {
                spriteRenderer.flipX = false; // 오른쪽 이동 그대로 
            }

            else if (move_AD < 0)
            {
                spriteRenderer.flipX = true; // 왼쪽 이동시 반전

            }

        }
        else if (move_AD == 0 && move_WS == 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Idle);

            if (Input.GetKey(KeyCode.F))
            {
                Player_animController.SetPlayerAnimState(Player_AnimState.Harvest);
            }
        }
    }
}