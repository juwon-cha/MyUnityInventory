using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI levelAmountTxt;
    [SerializeField] private TextMeshProUGUI DescriptionTxt;
    [SerializeField] private Slider expSlider;

    [SerializeField] private Button statusBtn;
    [SerializeField] private Button inventoryBtn;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetMainMenuButtons();
    }

    private void SetMainMenuButtons()
    {
        statusBtn.onClick.AddListener(OpenStatusPopup);

        inventoryBtn.onClick.AddListener(OpenInventoryPopup);
    }

    private void OpenStatusPopup()
    {
        HideMainMenuButtons();

        var uiData = new BaseUIData();
        uiData.OnClose = ShowMainMenuButtons;

        UIManager.Instance.OpenUI<StatusUI>(uiData);
    }

    private void OpenInventoryPopup()
    {
        HideMainMenuButtons();

        var uiData = new BaseUIData();
        uiData.OnClose = ShowMainMenuButtons;

        UIManager.Instance.OpenUI<InventoryUI>(uiData);
    }

    public void ShowMainMenuButtons()
    {
        statusBtn.gameObject.SetActive(true);
        inventoryBtn.gameObject.SetActive(true);
    }

    private void HideMainMenuButtons()
    {
        statusBtn.gameObject.SetActive(false);
        inventoryBtn.gameObject.SetActive(false);
    }
}
