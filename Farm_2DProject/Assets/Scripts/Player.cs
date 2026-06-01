using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Player_AnimController;

public class Player : MonoBehaviour
{
    [SerializeField] private Player_AnimController Player_animController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private FarmSystem farmSystem;
    public JoyStick joyStick;

    public Button plantBtn;
    public Button harvestBtn;
    public Button waterBtn;

    private bool isHarvest = false;
    private bool isWater= false;
    private bool isPlant = false;

    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조 

    public Vector2Int LookDir { get; private set; } = Vector2Int.down;

    private void OnEnable()
    {
        harvestBtn.onClick.AddListener(Onclick_Harvest);
        waterBtn.onClick.AddListener(Onclick_Water);
        plantBtn.onClick.AddListener(Onclick_Plant);
    }

    private void OnDisable()
    {
        harvestBtn.onClick.RemoveAllListeners();
        waterBtn.onClick.RemoveAllListeners();
        plantBtn.onClick.RemoveAllListeners();
    }
    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isHarvest) return;
        if (isWater) return;
        if (isPlant) return;
        Player_AnimMove();
    }

    private void Player_AnimMove()
    {
        Vector2 direction = joyStick.GetDirection(); // 조이스틱으로 입력 받은 방향 가져옴 
       // Debug.Log(direction); // 조이스틱 값 확인용

        if (direction.magnitude > 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Walk);
            Vector3 move_Trans = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime; // 실제 이동할 거리 계산

            transform.Translate(move_Trans); // 캐릭터 이동 

            if (direction.x > 0.1f)
            {
                spriteRenderer.flipX = false;
                LookDir = Vector2Int.right; //플레이어 방향 저장 
            }
           // 이미지를 좌우 반전 (flipX = true)
            else if (direction.x < -0.1f)
            {
                spriteRenderer.flipX = true;
                LookDir = Vector2Int.left; //플레이어 방향 저장
            }

            else if (direction.y > 0.1f)
            {
                LookDir = Vector2Int.up;
            }
            else if (direction.y < -0.1f)
            {
                LookDir = Vector2Int.down;
            }
        }

        else if( direction.magnitude == 0)
        {
            Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision ) //Is Trigger가 켜진 밭에 진입했을 때 호출
    {
        CropPlot plot = collision.GetComponent<CropPlot>(); // CropPlot 스크립트가 있는지 찾기 
        if (plot != null && farmSystem != null)
        {
            farmSystem.SetTargetPlot(plot); // 주소 보관
            Debug.Log("농사 영역 진입");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // 구역 밖으로 완전히 나왔을 때 호출
    {
        CropPlot plot = collision.GetComponent<CropPlot>();
        if (plot != null && farmSystem != null)
        {
            farmSystem.RemoveTargetPlot(plot); // 주소 삭제 
            Debug.Log("농사 영역 이탈");
        }
    }

    public void Onclick_Harvest()
    {
        if (farmSystem == null || isHarvest || isWater || isPlant) return;

        if (farmSystem.HarvestCrop()) //farmSystem에 수확 실행 지시 
        {
            isHarvest = true;
            Player_animController.SetPlayerAnimState(Player_AnimState.Harvest);
            Invoke(nameof(End_Harvest), 0.7f);
            //nameof(EndHarvest) == "EndHarvest"  -> 나중에 함수 이름을 바꿨을 때 에러날 경우를 대비해서
        }
    }

    private void End_Harvest()
    {
        isHarvest = false;
        Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
    }


    public void Onclick_Water()
    {

        if (farmSystem == null || isHarvest || isWater || isPlant) return;

        if (farmSystem.WaterCrop())
        {
            isWater = true;
            Player_animController.SetPlayerAnimState(Player_AnimState.Water);
            Invoke(nameof(End_Water), 0.7f);
        }
    }

    private void End_Water()
    {
        isWater = false;
        Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
    }

    public void Onclick_Plant()
    {

        if (farmSystem == null || isHarvest || isWater || isPlant) return;

        if (farmSystem.PlantCrop())
        {
            isPlant = true;
            Player_animController.SetPlayerAnimState(Player_AnimState.Plant);
            Invoke(nameof(End_Plant), 0.7f);
        }
    }

    private void End_Plant()
    {
        isPlant = false;
        Player_animController.SetPlayerAnimState(Player_AnimState.Idle);
    }

}