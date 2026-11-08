using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set; }

    [SerializeField] private int playerGold = 0;

    public int PlayerGold
    {
        get
        {
            return playerGold;
        }
    }

    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        playerGold += amount;

        //숫자 포멧팅 문접  -> 세미콜론을 기준으로 총 3가지 구간으로 나누어서 규칙을 정해주는 방식 
        //{amount:+#;-#;0} -> 양수, 음수일 때는 앞에 무조건 +, -기호 붙여서 출력/ 값이 0일 때는 그냥 0으로 출력  
        Debug.Log($"골드 변동: {amount:+#;-#;0}| 현재 소지금: {playerGold}");

        OnGoldChanged.Invoke(playerGold);

    }
}
