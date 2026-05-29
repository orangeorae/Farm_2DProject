using UnityEngine;

public class Player_AnimController : MonoBehaviour
{
    public enum Player_AnimState
    {
        None = 0,
        Idle = 1,
        Walk = 2,
        Harvest = 3,
        Water = 4,
        Plant = 5
    }

    [SerializeField] private Animator Player_Animator;

    private Player_AnimState currentState;

    public void SetPlayerAnimState(Player_AnimState newstate)
    {
        currentState = newstate;

        switch (currentState)
        {
            case Player_AnimState.Idle:
                ResetAllAnimParameters();
                break;
            case Player_AnimState.Walk:
                Player_Animator.SetBool("IsWalk", true);
                break;
            case Player_AnimState.Harvest:
                Player_Animator.SetBool("IsHarvest", true);
                break;
            case Player_AnimState.Water:
                Player_Animator.SetBool("IsWater", true);
                break;
            case Player_AnimState.Plant:
                Player_Animator.SetBool("IsPlant", true);
                break;

            default:
                ResetAllAnimParameters();
                Debug.LogWarning("올바르지 않은 상태입니다.");
                break;
        }
    }

    private void ResetAllAnimParameters()
    {
        Player_Animator.SetBool("IsWalk", false);
        Player_Animator.SetBool("IsHarvest", false);
        Player_Animator.SetBool("IsWater", false);
        Player_Animator.SetBool("IsPlant", false);
    }
}
