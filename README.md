# MyUnityInventory

`MyUnityInventory`는 Unity 엔진으로 개발된 인벤토리 시스템 구현 프로젝트이다. 이 프로젝트는 유지보수와 확장이 용이한 소프트웨어 아키텍처에 중점을 두고 있으며 특히 **MVP(Model-View-Presenter) 패턴**을 적용하여 UI와 데이터 로직을 분리하고 **Excel 기반의 데이터 관리 파이프라인**을 구축하여 게임 데이터를 효율적으로 관리하는 방법을 보여준다.

## 🌟 주요 특징

* **아키텍처**: UI와 데이터 로직의 의존성을 분리하기 위한 **MVP(Model-View-Presenter) 패턴** 적용.
* **데이터 관리**: 기획자가 관리하기 쉬운 **Excel(.xlsx) 파일**을 통해 아이템 데이터를 관리.
* **에디터 툴**: Unity 에디터 메뉴를 통해 Excel 파일을 **JSON 형식으로 자동 변환**하는 커스텀 툴 제공.
* **데이터 파싱**: 게임 실행 시 JSON 데이터를 파싱하여 **데이터 테이블로 관리**하고 게임 내에서 활용.
* **UI 시스템**: 인벤토리 및 캐릭터 스탯 UI 구현.

## 🏗️ 아키텍처: MVP(Model-View-Presenter) 패턴

이 프로젝트의 핵심 아키텍처는 MVP 패턴이다. Unity에서 흔히 발생하는 `Massive ViewController`(하나의 스크립트가 너무 많은 역할을 담당하는 문제)를 방지하고 코드의 재사용성과 테스트 용이성을 높이기 위해 UI 시스템 전반에 MVP 패턴을 적용했다.

### MVP 패턴 구조

* **Model**: 순수한 데이터와 데이터 처리 로직을 담당한다. View나 Presenter에 대해 알지 못하는 독립적인 계층이다.
    * `InventoryData.cs`: 인벤토리에 소지한 아이템 목록, 장착 아이템 정보 등 실제 인벤토리 데이터를 관리한다. 데이터 변경 시 `OnInventoryChanged` 이벤트를 통해 외부로 알립니다.
    * `Character.cs`: 캐릭터의 스탯(공격력, 방어력 등) 데이터를 관리하며 장비 장착/해제에 따라 스탯을 업데이트한다.

* **View**: 사용자에게 보여지는 UI 요소들을 담당하며 사용자 입력을 받아 Presenter에게 전달하는 역할만 수행한다.
    * `InventoryUI.cs`, `StatusUI.cs`: 실제 UI 프리팹에 붙는 컴포넌트로 Presenter의 지시에 따라 화면을 갱신한다. 아이템 슬롯 클릭과 같은 사용자 입력은 `Action` 이벤트를 통해 Presenter로 전달한다.
    * `IInventoryView.cs`, `IStatusView.cs`: View가 구현해야 하는 인터페이스이다. Presenter는 이 인터페이스를 통해 View와 상호작용하므로 구체적인 View 클래스(MonoBehaviour)에 대한 의존성이 없다.

* **Presenter**: Model과 View 사이의 중재자 역할을 한다. View로부터 사용자 입력을 전달받아 Model의 데이터를 갱신하고 Model의 데이터 변경을 감지하여 View에게 UI 업데이트를 지시하는 등 애플리케이션의 핵심 로직을 담당한다.
    * `InventoryPresenter.cs`, `StatusPresenter.cs`: View와 Model을 생성자에서 주입받아 양쪽의 이벤트를 구독한다. Model의 데이터를 가공하여 View가 표시하기 좋은 형태(`InventoryItemSlotData`)로 만들어 전달하는 '프레젠테이션 로직'을 수행한다.

### 심화: 중첩된 MVP 패턴

`InventoryUI`는 여러 개의 `InventoryItemSlot`으로 구성된다. 이 프로젝트는 개별 `InventoryItemSlot`에도 MVP 패턴을 적용하여 일관성과 확장성을 높였다.

* **Model**: `InventoryItemSlotData`
* **View**: `InventoryItemSlot.cs`, `IInventoryItemSlotView.cs`
* **Presenter**: `InventoryItemSlotPresenter.cs`

이를 통해 각 아이템 슬롯은 독립적인 컴포넌트로 기능하며 아이콘 로딩, 장착 표시 등 자신과 관련된 로직을 스스로 처리한다.

## ⚙️ 데이터 관리 파이프라인: Excel에서 게임 데이터까지

게임에 사용되는 수많은 아이템 데이터를 코드에 직접 작성(하드코딩)하는 대신, 외부 데이터 파일을 이용하는 효율적인 파이프라인을 구축했다.

### 1. Excel 데이터 작성

모든 아이템의 기본 정보(ID, 이름, 타입, 스탯 등)는 기획자가 다루기 쉬운 Excel 파일(`Assets/Data/ItemDataTable.xlsx`)에 정의된다.

| ItemID | ItemName      | ItemType | ATK | DEF |
| :----- | :------------ | :------- | :-: | :-: |
| 11001  | Wooden Sword  | Weapon   |  5  |  0  |
| 21001  | Leather Armor | Armor    |  0  |  3  |
| ...    | ...           | ...      | ... | ... |

### 2. Excel to JSON 변환기

Unity 에디터 상단 메뉴의 **[Tools > Convert Excel to Json]** 를 통해 Excel 파일을 Json으로 변환할 수 있다.

* **`XlsxToJsonConverter.cs`**: 이 에디터 스크립트는 `Assets/Data` 폴더 내의 모든 `.xlsx` 파일을 찾아 읽는다.
* **`ExcelDataReader`**: 외부 라이브러리를 사용하여 Excel 파일의 데이터를 읽어들인다.
* **JSON 생성**: 읽어들인 데이터를 Unity에서 사용하기 쉬운 JSON 형식으로 변환하여 `Assets/Resources/ItemData/ItemData.json` 경로에 저장한다.

### 3. JSON 데이터 파싱 및 게임 적용

* **`DataTableManager.cs`**: 게임이 시작될 때 `Resources` 폴더에서 `ItemData.json` 파일을 로드한다.
* **데이터 테이블 생성**: 로드한 JSON 텍스트를 파싱하여 각 아이템 데이터를 `ItemData` 클래스 객체로 변환한다. 이 객체들을 `ItemID`를 키로 하는 `Dictionary`에 저장하여 게임 내에서 빠르게 아이템 정보를 참조할 수 있도록 한다.
* **게임 내 활용**: `GameManager`를 통해 `DataTableManager`에 접근하여 아이템의 스탯, 이름 등 필요한 정보를 언제든지 가져와 사용할 수 있다.

## 🚀 시작하기

1.  이 저장소를 클론(`git clone`)한다.
2.  Unity Hub에서 프로젝트를 열어준다. (Unity 2021.3.17f1 버전에서 제작되었습니다.)
3.  `Assets/Scenes/MyUnityInventory.unity` 씬을 연다.
4.  Play 버튼을 눌러 게임을 실행한다.

## 📂 프로젝트 구조
```
MyUnityInventory/
├── Assets/
│   ├── Data/                 # Excel 원본 데이터 파일
│   ├── Editor/               # Unity 에디터 확장 스크립트 (XlsxToJsonConverter)
│   ├── Externals/            # 외부 에셋 (이미지, 폰트 등)
│   ├── Plugins/              # 외부 라이브러리 (ExcelDataReader)
│   ├── Prefabs/              # UI 및 시스템 프리팹
│   ├── Resources/
│   │   └── ItemData/         # 변환된 JSON 데이터 파일
│   ├── Scenes/               # 게임 씬
│   └── Scripts/
│       ├── Manager/          # 게임의 전반적인 관리자 클래스 (GameManager, UIManager 등)
│       ├── Item/             # 아이템 데이터 클래스
│       └── UI/               # MVP 패턴이 적용된 UI 관련 스크립트
└── ProjectSettings/          # Unity 프로젝트 설정 파일
```

## 📜 사용된 에셋

* **Font**: DNFBitBitv2
* **UI Button Set**: FreeButtonSet by Cainos
* **Item Icons**: Pixel Art Icon Pack - RPG by Cainos