using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button InvButton;
    public GameObject InvPopUp;
    public Button OffButton;

    public GameObject SlotPrefab;
    public Transform SlotContentArea;

    private static Dictionary<string, int> cropItem = new Dictionary<string, int>(); //작물 이름과 수 매칭
    private List<GameObject> spawnSlot = new List<GameObject>();

    private void OnEnable()
    {
        InvButton.onClick.AddListener(Onclick_Inv);
        OffButton.onClick.AddListener(Onclick_OffButton);
    }

    private void OnDisable()
    {
        InvButton.onClick.RemoveAllListeners();
        OffButton.onClick.RemoveAllListeners();
    }

    public void Onclick_Inv()
    {
        InvPopUp.SetActive(true);
        UpdateInventoryUI(); // 인벤토리가 열리는 순간 최신 개수로 글자 갱신
    }

    public void Onclick_OffButton()
    {
        InvPopUp.SetActive(false);
    }

    public void AddItem(string cropName, int count)
    {
        if (cropItem.ContainsKey(cropName))
        {
            cropItem[cropName] += count; // 이미 있는거면 수만 더하기
        }

        else
        {
            cropItem.Add(cropName, count); 
        }

        Debug.Log($"[인벤토리] {cropName} 획득 / 현재 수: {cropItem[cropName]}개");
        UpdateInventoryUI();
    }

    public bool HasItem(string cropName, int requiredCount)
    {
        bool hasEnough = false;

        if (cropItem.ContainsKey(cropName))//인벤토리에서 해당 작물이름 확인 
        {
            int currentCount = cropItem[cropName]; //수량 가져오기 

            if(currentCount >= requiredCount) //수량 비교
            {
                hasEnough = true;
            }
        }

        return hasEnough;
    }


    public void RemoveItem(string cropName, int count)
    {
        if (cropItem.ContainsKey(cropName))
        {
            cropItem[cropName] -= count;

            if (cropItem[cropName] <= 0)
            {
                cropItem[cropName] = 0;
            }

            Debug.Log($"[Inventory] {cropName} {count}개 차감 완료 ");
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI() //당근데이터를 화면의 Text에 그려줌
    {

        foreach(GameObject slot in spawnSlot) //리스트를 돌면서 이미 존재한 오브젝트들을 삭제
        {
            Destroy(slot);
        }

        spawnSlot.Clear(); //리스트를 아예 깨끗하게 비워주기 위함


        foreach (KeyValuePair<string, int> item in cropItem)
        {
            string cropName = item.Key; ; // 작물의 이름/ID 저장
            int cropCount = item.Value; // 개수 저장

            if (cropCount <= 0) continue;

            //작물 프리팹 가져와서 정렬 구역에 자식으로 찍어내기
            GameObject newSlot = Instantiate(SlotPrefab, SlotContentArea);
            spawnSlot.Add(newSlot);  //리스트에 추가 

            //새로 생성된 슬롯 안에서 Image랑 Text 컴포넌트 찾아오기 
            Image iconImage = newSlot.GetComponentInChildren<Image>(); //부모부터 자식까지 탐색
            Text countText = newSlot.GetComponentInChildren<Text>();

            if (countText != null)
            { 
          
                countText.text = $"{cropCount}";

            }

            
            if(iconImage != null)
            {
                //이미지 로드
                Sprite cropSprite = Resources.Load<Sprite>($"Image/Icon_IMG/{cropName}");

                if(cropSprite != null)
                {
                    iconImage.sprite = cropSprite;  
                }
            }
        }

    }
}

