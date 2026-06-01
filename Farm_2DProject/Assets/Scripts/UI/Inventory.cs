using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button InvButton;
    public GameObject InvPopUp;
    public Button OffButton;

    private Dictionary<string, int> cropItem = new Dictionary<string, int>(); //작물 이름과 수 매칭

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
    }

    public int GetCropItemCount(string cropName)  // 농작물 개수 확인하는 함수 
    {
        if (cropItem.ContainsKey(cropName))
        {
            return cropItem[cropName];
        }
        return 0;
    }
}

