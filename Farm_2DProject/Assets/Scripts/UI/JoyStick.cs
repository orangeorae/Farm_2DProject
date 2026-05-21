using UnityEngine;
using UnityEngine.EventSystems; //터치 이벤트 

//유니티에서 제공하는 모바일 터치 이벤트 감지 인터페이스
public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

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
        handle.position = eventData.position; // 손잡이를 터치한 위치로 이동

        //조이스틱 범위를 벗어나지 않게 반지름을 90으로 제한
        handle.anchoredPosition = Vector2.ClampMagnitude(handle.anchoredPosition, radius);

        //입력 방향 계산(-1 ~ 1 사이의 값)
        inputDirection = handle.anchoredPosition / radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //손을 떼면 중앙으로 복귀
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return inputDirection;
    }

}
