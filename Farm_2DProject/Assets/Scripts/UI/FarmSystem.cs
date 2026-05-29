using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Tilemaps;

public class FarmSystem : MonoBehaviour
{
    [SerializeField] private Tilemap farmTileMap; // 어떤 칸이 밭인지 검사용

    [SerializeField] private GameObject cropPrefab; // 작물 프리팹 연결

    [SerializeField] private Player player; // 플레이어 위치 가져오기

    private Dictionary<Vector3Int, GameObject> crops = new Dictionary<Vector3Int, GameObject>(); // 같은 간 중복 심기 방지
    //Vector3Int  - 타일 칸 좌표  Ex) Vector3Int cellPos = (2, 3, 0) -> (2,3) 
    // TileMap 함수 기반들이 Vector3Int 기반 그래서 Vector2Int를 써도 되는 데
    // Vector3Int를 사용하면 변환과정없이 바로 사용가능해서 코드가 더 간결해진다


    public void PlantCrop()
    {

        Vector3 playerPos =player.transform.position +  new Vector3(0, -0.6f, 0); // 캐릭터 중심위치에서 Y축을 아래로 내려 안전하게 땅의 좌표가 담기게 한다

        Vector3Int playerCell =farmTileMap.WorldToCell(playerPos); // 월드좌표를 타일맵 칸 좌표로 변환

        Vector3Int targetCell = playerCell; // 밭을 찾으면 좌표 저장

        bool foundFarm = false; //밭을 찾았는지 여부 기록



        // 주변 5x5 탐색  Grid cell size 를 0.25 * 0.25로 해놔서 좀 더 넓게 탐색
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                Vector3Int checkCell = playerCell + new Vector3Int(x, y, 0);  // 오차 더해주기 

                if (farmTileMap.HasTile(checkCell)) //좌표 안에 타일이 있으면 
                {
                    targetCell = checkCell; // 좌표 넘겨주기 
                    foundFarm = true;
                    break;
                }
            }
            if (foundFarm) break; // 타일을 찾았으니 바깥쪽 for문도 완전히 탈출 

        }

        // 주변에서 밭을 못 찾았으면 종료
        if (foundFarm == false)
        {
            Debug.Log($"근처에 밭이 없습니다. (플레이어 발밑 월드좌표: {playerPos}, 인식된 타일좌표: {playerCell})");
            return;
        }


        if (crops.ContainsKey(targetCell))
        {
            Debug.Log("이미 심어져 있습니다.");
            return;
        }

        Vector3 spawnPos = farmTileMap .GetCellCenterWorld(targetCell); //타일 중앙 좌표 반환

        GameObject crop = Instantiate(cropPrefab, spawnPos, Quaternion.identity); // 작물 생성,회전없이

        crops.Add(targetCell, crop); 

        Debug.Log("심기 성공");
    }

    public void WaterCrop()
    {
        Vector3 playerPos = player.transform.position + new Vector3(0, -0.6f, 0);
        Vector3Int playerCell = farmTileMap.WorldToCell(playerPos);
        Vector3Int targetCell = playerCell;
        bool foundFarm = false;

        // 심을 때와 동일한 로직
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                Vector3Int checkCell = playerCell + new Vector3Int(x, y, 0);

                if (farmTileMap.HasTile(checkCell))
                {
                    targetCell = checkCell;
                    foundFarm = true;
                    break;
                }
            }
            if (foundFarm) break;
        }

        if (foundFarm == false)
        {
            Debug.Log("물 줄 밭이 근처에 없습니다.");
            return;
        }

        // 해당 좌표에 심어진 작물이 있다면 상태 변경 요청
        if (crops.ContainsKey(targetCell))
        {
            GameObject currentCropObj = crops[targetCell]; // 해당 좌표에 매칭되어 있는 작물 가져오기
            CropPlot plotScript = currentCropObj.GetComponent<CropPlot>(); //작물 상태 가져오기

            if (plotScript != null)
            {
                // 현재 상태가 씨앗상태일 때만 작동
                if (plotScript.currentState == CropPlot.PlotState.Planted)
                {
                    plotScript.WaterPlot();
                    Debug.Log("물주기 최종 성공 -> 당근 완성!");
                }
            }
        }
    }


  
}
