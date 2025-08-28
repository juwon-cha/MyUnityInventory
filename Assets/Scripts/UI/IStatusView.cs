using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusView
{
    public event Action OnCloseButtonClicked;

    // Presenter가 스탯 정보를 전달하여 UI 업데이트를 지시
    public void UpdateStats(UserItemStats totalStats);
    public void Close();
}
