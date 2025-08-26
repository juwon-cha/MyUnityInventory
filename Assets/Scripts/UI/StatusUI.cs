using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI criticalText;

    [SerializeField] private Button closeBtn;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetStatusInfo();
    }

    private void SetStatusInfo()
    {
        //var player = GameManager.Instance.Player;
        //attackText.text = player.Attack.ToString();
        //defenseText.text = player.Defense.ToString();
        //hpText.text = player.MaxHP.ToString();
        //criticalText.text = player.Critical.ToString();

        closeBtn.onClick.AddListener(() =>
        {
            CloseUI();
        });
    }
}
