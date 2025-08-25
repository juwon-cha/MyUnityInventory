using System;
using UnityEngine;

public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
}

public class BaseUI : MonoBehaviour
{
    private Action onShow;
    private Action onClose;

    public virtual void Init(Transform anchor)
    {
        onShow = null;
        onClose = null;

        transform.SetParent(anchor);
    }

    public virtual void SetInfo(BaseUIData uiData)
    {
        Debug.Log($"{GetType()}::SetInfo");

        onShow = uiData.OnShow;
        onClose = uiData.OnClose;
    }

    public virtual void ShowUI()
    {
        onShow?.Invoke();
        onShow = null;
    }

    public virtual void CloseUI(bool isCloseAll = false)
    {
        if (!isCloseAll)
        {
            onClose?.Invoke();
        }
        onClose = null;

        UIManager.Instance.CloseUI(this);
    }
}
