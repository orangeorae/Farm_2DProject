using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CropPlot : MonoBehaviour
{
    public enum PlotState
    {
        Planted,
        ReadyToHarvest,
        Empty
    }

    [SerializeField] private Sprite seedSprite; // 씨앗 이미지
    [SerializeField] private Sprite grownSprite; // 완전히 자란 당근 이미지
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

        //DataManager에서 이 작물의 정보를 가져온다.
        CropData data = DataManager.Instance.GetCropData(cropID);
        if (data == null)
        {
            Debug.LogError($"[CropPlot] {cropID}에 해당하는 작물 데이터가 없습니다.");
            return false;
        }
        currentCropID = cropID; //매개변수로 넘겨받은 작물 이름을 저장 

        currentState = PlotState.Planted;

        // Addressable를 이용하여 비동기 로드
        Addressables.LoadAssetAsync<Sprite>(data.SeedSpritePath).Completed += OnCropSpriteLoad;

        Debug.Log(cropID + "씨앗 심기 성공");
        return true;
    }

    
    private void OnCropSpriteLoad(AsyncOperationHandle<Sprite> handle) // 비동기 로드가 완료 되었을 때 실행할 함수 
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = handle.Result; // 로드 완료된 스프라이트 이미지로 변경
            }
        }
        else
        {
            Debug.LogError($"[CropPlot] 작물 이미지 Addressable 로드 실패");
        }
    }



    public bool WaterPlot()
    {
        if (currentState == PlotState.Planted)
        {
            CropData data = DataManager.Instance.GetCropData(currentCropID);
            if (data == null)
            {
                Debug.LogError($"[CropPlot] 물을 주려는데 {currentCropID} 데이터가 없습니다.");
                return false;
            }
            currentState = PlotState.ReadyToHarvest;

      
            Addressables.LoadAssetAsync<Sprite>(data.GrownSpritePath).Completed += OnCropSpriteLoad; 
            Debug.Log($"{currentCropID} 자라남");
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
        currentCropID = "";
        if (spriteRenderer != null && dirtSprite != null)
        {
            spriteRenderer.sprite = dirtSprite;
        }
    }
}
