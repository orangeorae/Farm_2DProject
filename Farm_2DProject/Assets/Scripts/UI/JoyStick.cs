using UnityEngine;
using UnityEngine.EventSystems; //터치 이벤트 

//유니티에서 제공하는 모바일 터치 이벤트 감지 인터페이스
public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private RectTransform background;

    public RectTransform handle;
    private Vector2 inputDirection;

    private float radius = 90f; // 내 조이스틱의 백그라운드의 width가 180이므로 90 
    // 누르는 순간 실행 될 함수 
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // 드래그하는 동안 매 프레임 실행 될 함수
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 pos; // 최종 위치 담아둘 변수 

        // 화면 좌표를 조이스틱 내부 좌표로 변환하기 위함 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, // 배경의 정중앙을 (0,0)으로 하기 위함
            eventData.position, // 손가락이 누르고 있는 화면 좌표
            eventData.pressEventCamera, // 캔버스 카메라 정보
            out pos //변환이 완료된 조이스틱 기준 좌표를 pos 변수에 담는다
        );

        // 조이스틱 범위 제한
        pos = Vector2.ClampMagnitude(pos, radius);

        // 손잡이 이동
        handle.anchoredPosition = pos;

        // 방향값 계산
        inputDirection = pos / radius;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //손을 떼면 중앙으로 복귀
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return inputDirection; // 계산된 값 반환 
    }

}
