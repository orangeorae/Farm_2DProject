using UnityEngine;
using UnityEngine.UI;

public class SeedBox : MonoBehaviour
{

    [SerializeField] private string seedId;

    public Button seedBtn;

    private void OnEnable()
    {
        seedBtn.onClick.AddListener(Onclick_SeedSelect);

    }

    private void OnDisable()
    {
        seedBtn.onClick.RemoveAllListeners();

    }
    public void Onclick_SeedSelect()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.ChangeSelectSeed(seedId);
        }

        else
        {
            Debug.LogError("[SeedBox] 씬에서 Player를 찾을 수 없습니다");
        }
    }
}
