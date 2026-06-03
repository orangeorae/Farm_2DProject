using UnityEngine;

public class CropPlot : MonoBehaviour
{
    public enum PlotState
    {
        Planted,
        ReadyToHarvest,
        Empty
    }

    [SerializeField] private Sprite seedSprite; // 씨앗 이미지
    [SerializeField] private Sprite carrotSprite; // 완전히 자란 당근 이미지
    [SerializeField] private Sprite dirtSprite; // 흙 이미지


    public PlotState currentState = PlotState.Empty;  // 처음에는 빈땅 상태
    public string currentCropID; // 어떤 작물이 심겼는지 데이터 ID 저장용

    private SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPlot();
    }

    public bool PlantCrop(string cropID) 
    {
        if (currentState != PlotState.Empty)
        {
            return false;
        }

        currentCropID = cropID; //매개변수로 넘겨받은 작물 이름을 저장 

        currentState = PlotState.Planted;

        if (seedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = seedSprite; //씨앗 이미지로 변경
        }

        Debug.Log(cropID + "씨앗 심기 성공");
        return true;
    }
    public bool WaterPlot()
    {
        if (currentState == PlotState.Planted)
        {
            currentState = PlotState.ReadyToHarvest;

            if (carrotSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = carrotSprite; // 당근 이미지로 변경
            }
            Debug.Log("당근 자라남");
            return true;
        }

        return false;
    }

    public string HarvestPlot()
    {
        if (currentState != PlotState.ReadyToHarvest) return null;

        string harvestItem = currentCropID;

        ResetPlot();

        return harvestItem;
    }

    private void ResetPlot()
    {
        currentState = PlotState.Empty;
        currentCropID = " ";
        if (spriteRenderer != null && dirtSprite != null)
        {
            spriteRenderer.sprite = dirtSprite;
        }
    }
}
