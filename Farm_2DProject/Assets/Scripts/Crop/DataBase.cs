using UnityEngine;

[System.Serializable]
public class DataBase // JSON 기획 데이터 클래스의 최상위 부모 클래스
{
    
    public string Id;
}

[System.Serializable]
public class CropData : DataBase //농작물 세부 기획 데이터 클래스
{
    public string CropName;
    public int SeedPrice;
    public int SellPrice;
    public string SeedSpritePath;
    public string GrownSpritePath;

}

[System.Serializable]
public class CafeData : DataBase //카페 세부 기획 데이터 클래스
{
    public string MenuName;
    public string MenuSpritePath;
    public string RequireCrop;
    public string RequireCount;
    public int RewardGold;

}
