using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class CafePopup : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject Prefab_CafePopup;
    [SerializeField] private TextMeshProUGUI Text_menuName;
    [SerializeField] private TextMeshProUGUI Text_Ingredient; //필요 작물
    [SerializeField] private TextMeshProUGUI Text_reward; // 보상
    [SerializeField] private Image Image_Menu;

    [SerializeField] private Inventory inventory;

    private List<CafeData> _allQuest  = new List<CafeData>(); // 카페 퀘스트 리스트 
    private List<CafeData> _remainQuest = new List<CafeData>(); // 남은 퀘스트 리스트

    private CafeData _currentQuest; //현재 창에 고정되어 진행 중인 퀘스트 


    public CafeData CurrentQuest //현재 퀘스트를 안전하게 가져오기 위한 프로퍼티 
    {
        get => _currentQuest;
        set
        {
            if(_currentQuest != value)
            {
                _currentQuest = value;
            }
        }
    }
    private void Start()
    {
        InitCafeQuest();
    }

    public void OpenPopup()
    {
        Prefab_CafePopup.SetActive(true);

        if (_allQuest == null || _allQuest.Count == 0)  // 데이터가 아직 안불러졌을 경우 대비 
        {
            InitCafeQuest();
        }
        if(_remainQuest.Count == 0) //남은 퀘스트가 없으면 다시 리셋
        {
            ResetQuest();
        }
       /* if (_remainQuest.Count > 0) // 테스트용 
        {
            int randomIndex = Random.Range(0, _remainQuest.Count);

            _currentQuest = _remainQuest[randomIndex];

            DisplayPopupData();

            Debug.Log($"[CafePopup] 랜덤 선택된 메뉴: {_currentQuest.MenuName}");
        }*/

        if(_currentQuest == null && _remainQuest.Count > 0) //진행 중인 퀘스트가 없을 경우에만 다시 랜덤 퀘스트 
        {
            int randomIndex = Random.Range(0, _remainQuest.Count);
            _currentQuest = _remainQuest[randomIndex];
            Debug.Log($"[CafePopup] 새로운 퀘스트가 발행 되었습니다.");
        }

        DisplayPopupData();


    }

    public void ClosePopup()
    {
        Prefab_CafePopup.SetActive(false);
    }

    private void InitCafeQuest()
    {
        if (DataManager.Instance != null)
        {
            _allQuest = new List<CafeData>(DataManager.Instance.CafeDataList.Values); //딕셔너리인 카페데이터를 리스트 형태로 변환하여 퀘스트 목록에 저장 

            if (_allQuest.Count > 0) //퀘스트 데이터가 1개라도 존재하면 
            {
                ResetQuest();
            }

            else
            {
                Debug.Log("[CafePopup] CafeDataList에 데이터가 비어있습니다.");
            }
        }
    }

    private void ResetQuest()
    {
        _remainQuest = new List<CafeData>(_allQuest);
        Debug.Log("모든 카페 퀘스트 완료 / 퀘스트 목록이 초기화 되었습니다.");
    }

    private void DisplayPopupData()
    {
        if(_currentQuest == null)
        {
            return;
        }

        if (Text_menuName != null)
        {
            Text_menuName.text = _currentQuest.MenuName;
        }

        if(Text_Ingredient != null) 
        {
            //쉼표를 기준으로 여러 개로 묶인 작물 이름과 수량을 각각 문자열 배열로 쪼개기 
            string[] crops = _currentQuest.RequireCrop.Split(','); 
            string[] counts = _currentQuest.RequireCount .Split(",");

            string requireText = "";

            for (int i = 0; i < crops.Length; i++) // 쪼갠 데이터 개수만큼 반복문을 돌며 한줄씩 맞추기 
            {
                string cropName = crops[i].Trim(); // 앞뒤 불필요한 공백 제거 
                string cropCount = counts[i].Trim();

                requireText += $"{cropName}: {cropCount}개 필요";

                if (i < crops.Length - 1) // 마지막 재료가 아니라면 줄바꿈
                {
                    requireText += "\n";
                }
            }
                Text_Ingredient.text = requireText;
        }

        if(Text_reward != null)
        {
            Text_reward.text = $"{_currentQuest.RewardGold} 골드(Gold)";
        }

        // Addressable를 이용하여 비동기 로드
        Addressables.LoadAssetAsync<Sprite>(_currentQuest.MenuSpritePath).Completed += OnCafeSpriteLoad;
    }

    private void OnCafeSpriteLoad(AsyncOperationHandle<Sprite> handle) // 비동기 로드가 완료 되었을 때 실행할 함수 
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (Image_Menu != null)
            {
                Image_Menu.sprite = handle.Result; // 로드 완료된 스프라이트 이미지로 변경
            }
        }
        else
        {
            Debug.LogError($"[CafePopup] 카페 메뉴 이미지 Addressable 로드 실패");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("카페 영역 진입");

            OpenPopup();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("카페 영역 진입");

            ClosePopup();
        }
    }
}
