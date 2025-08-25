using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    protected bool isDestroyOnLoad = false;

    private static T instance;

    public static T Instance
    {
        get
        {
            // instance가 아직 없는 경우
            if (instance == null)
            {
                // 씬에서 먼저 탐색(비활성화된 오브젝트 포함)
                instance = FindObjectOfType<T>(true);

                // 씬에도 없다면 새로 생성
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    // 여기서 AddComponent를 하는 순간 해당 컴포넌트의 Awake() 호출
                    // Awake() 내부의 Init()에서 instance 할당 및 초기화
                    singletonObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        // instance가 아직 할당되지 않았다면 현재 이 컴포넌트를 인스턴스로 설정
        // 이 코드는 씬에 미리 배치했거나 Instance getter를 통해 동적으로 생성되었을 때 모두 실행
        if (instance == null)
        {
            instance = (T)this;

            // 게임 오브젝트의 이름을 싱글톤 타입에 맞게 변경
            gameObject.name = $"{typeof(T)}";

            // isDestroyOnLoad 플래그가 false일 때만 씬 전환 시 파괴되지 않도록 설정
            if (!isDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        // instance가 이미 존재하는데 이 컴포넌트가 아니라면 중복된 것이므로 파괴
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
