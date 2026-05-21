using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button InvButton;
    public GameObject InvPopUp;
    public Button OffButton;
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


}

