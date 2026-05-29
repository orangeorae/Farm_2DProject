using UnityEngine;

public class CropPlot : MonoBehaviour
{
    public enum PlotState
    {
        Planted,
        ReadyToHarvest
    }

    [SerializeField] private Sprite seedSprite; // 씨앗이 심긴 이미지
    [SerializeField] private Sprite carrotSprite; // 완전히 자란 당근 이미지


    public PlotState currentState = PlotState.Planted; // 생성되자마자 씨앗 상태
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentState = PlotState.Planted;
        if (seedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = seedSprite;
        }
    }

    public void WaterPlot()
    {
        if (currentState == PlotState.Planted)
        {
            currentState = PlotState.ReadyToHarvest;
            spriteRenderer.sprite = carrotSprite; // 당근 이미지로 변경
            Debug.Log("당근 자라남");
        }
    }


    
}
