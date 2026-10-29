using UnityEngine;

public class CafePopup : MonoBehaviour
{
    [SerializeField] private GameObject CafePopupUI;

    private void Start()
    {
        if(CafePopupUI != null)
        {
            CafePopupUI.SetActive(false);   // (임시) 추후 UIManager를 통할 것 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("카페 영역 진입");

            CafePopupUI.SetActive(true); // (임시) 추후 UIManager를 통할 것
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("카페 영역 진입");

            CafePopupUI.SetActive(false); // (임시) 추후 UIManager를 통할 것
        }
    }
}
