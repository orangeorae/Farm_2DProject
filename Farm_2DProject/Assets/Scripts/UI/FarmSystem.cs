using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Tilemaps;

public class FarmSystem : MonoBehaviour
{ 

    [SerializeField] private Inventory inventory; // 인벤토리 연걸 

    private CropPlot currentTargetPlot = null;  // 플레이어가 트리거 콜라이더로 밟고 서있는 현재 CropPlot 플랫폼

    public void SetTargetPlot(CropPlot plot) // 플레이어가 영역안에 들어왔을 때 호출 (플레이어 코드에서 호출)
    {
        currentTargetPlot = plot;  //플레이어가 서 있는 밭의 주소 저장 
    }

    public void RemoveTargetPlot(CropPlot plot) // 플레이어가 영역 밖으로 나갔을 때
    {
        if (currentTargetPlot == plot)  // 다른 밭과 꼬이는 것 방지
        { 
            currentTargetPlot = null; 
        }
       
    }

    public bool PlantCrop(string cropID)
    {
        if (currentTargetPlot == null) return false; // 플레이어 발 밑에 밭이 없으면 false 반환 
        return currentTargetPlot.PlantCrop(cropID); 
    }

    public bool WaterCrop()
    {
        if (currentTargetPlot == null) return false;
        return currentTargetPlot.WaterPlot();
    }

    public bool HarvestCrop()
    {
        if (currentTargetPlot == null) return false;

        string resultItem = currentTargetPlot.HarvestPlot();

        if (resultItem != null)
        {
            if(inventory != null)
            {
                inventory.AddItem(resultItem, 1); //인벤토리에 아이템 1개 추가 
            }

            return true; //수확이 성공했을 경우 true 리턴 
        }
        return false; // 수확물이 안나왔을 경우 false 리턴
    }

}



