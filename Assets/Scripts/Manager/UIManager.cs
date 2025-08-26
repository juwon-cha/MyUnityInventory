using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public Transform UICanvasTransform;
    public Transform ClosedUITransform;

    // 가장 상단에 표시되는 UI를 참조하는 변수
    private BaseUI frontUI;

    // 현재 활성화 되어있는 UI를 담는 딕셔너리
    private Dictionary<System.Type, GameObject> openUIPool = new Dictionary<System.Type, GameObject>();
    // 현재 비활성화 되어있는 UI를 담는 딕셔너리
    private Dictionary<System.Type, GameObject> closedUIPool = new Dictionary<System.Type, GameObject>();

    // Constants
    private const string UI_PATH = "Prefabs/UI/";

    protected override void Init()
    {
        base.Init();
    }

    private BaseUI GetUI<T>(out bool isAlreadyOpen)
    {
        Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        // UI가 한 번이라도 생성되었다면 풀에서 가져와서 재활용
        if(openUIPool.ContainsKey(uiType))
        {
            ui = openUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (closedUIPool.ContainsKey(uiType))
        {
            ui = closedUIPool[uiType].GetComponent<BaseUI>();
            closedUIPool.Remove(uiType);
        }
        else // UI가 한 번도 생성된 적이 없다면 새로 생성
        {
            var uiObj = Instantiate(Resources.Load<GameObject>($"{UI_PATH}{uiType}"));
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui;
    }

    public void OpenUI<T>(BaseUIData uiData)
    {
        Type uiType = typeof(T);

        Debug.Log($"{GetType()}::OpenUI({uiType})");

        bool isAlreadyOpen = false;
        var ui = GetUI<T>(out isAlreadyOpen);

        if (isAlreadyOpen)
        {
            Debug.Log($"{uiType} is already opened.");
            return;
        }

        ui.Init(UICanvasTransform);
        ui.gameObject.SetActive(true);
        ui.SetInfo(uiData);
        ui.ShowUI();

        frontUI = ui;
        openUIPool[uiType] = ui.gameObject;
    }

    public void CloseUI(BaseUI ui)
    {
        Type uiType = ui.GetType();

        Debug.Log($"{GetType()}::OpenUI({uiType})");

        ui.gameObject.SetActive(false);
        openUIPool.Remove(uiType);
        closedUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(ClosedUITransform);

        frontUI = null;
    }

    public void CloseAllOpenUI()
    {
        while(frontUI)
        {
            frontUI.CloseUI(true);
        }
    }

    // 특정 UI 화면이 열려있는지 확인. 열려있는 UI화면(객체) 반환
    public BaseUI GetActiveUI<T>()
    {
        var uiType = typeof(T);
        return openUIPool.ContainsKey(uiType) ? openUIPool[uiType].GetComponent<BaseUI>() : null;
    }
}
