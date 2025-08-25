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
        statusBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<StatusUI>(new BaseUIData());
        });

        inventoryBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<InventoryUI>(new BaseUIData());
        });
    }
}
