using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public Button SettingButton;

    public void OnEnable()
    {
        StartButton.onClick.AddListener(OnClick_StartBtn);
        SettingButton.onClick.AddListener(OnClick_SettingBtn);
    }

    public void OnDisable()
    {
        StartButton.onClick.RemoveAllListeners();
        SettingButton.onClick.RemoveAllListeners();
    }
    public void OnClick_StartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClick_SettingBtn()
    {

    }
}
