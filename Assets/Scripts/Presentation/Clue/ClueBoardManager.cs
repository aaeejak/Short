using UnityEngine;


/// 단서 보드의 열기/닫기를 관리 , 게임 상태(일시정지/재개)를 제어

public class ClueBoardManager : MonoBehaviour
{
    /// 단서 보드가 열려있는지 여부
    public static bool IsBoardOpen { get; private set; } = false;
    
    [Header("단서 데이터")]
    [Tooltip("ClueDatabase ScriptableObject를 드래그")]
    public ClueDatabase clueDatabase;
    
    [Header("UI 커스터마이징")]
    [Tooltip("사용자 지정 보드 배경 이미지 (없으면 기본 크림색 배경 사용)")]
    public Sprite customBackgroundImage;

    [Header("디버그")]
    [Tooltip("체크하면 플레이 시작 시 모든 단서 발견 기록을 초기화합니다")]
    public bool resetOnPlay = false;
    
    private ClueBoardUI boardUI;        
    private ClueService clueService;    

    void Awake()
    {
        // clueDatabase가 Inspector에서 할당되지 않은 경우, Resources에서 자동 로드
        if (clueDatabase == null)
        {
            clueDatabase = Resources.Load<ClueDatabase>("Clues/ClueDatabase");
            if (clueDatabase != null)
            {
                Debug.Log("[ClueSystem] ClueDatabase를 Resources에서 자동 로드했습니다.");
            }
            else
            {
                Debug.LogError("[ClueSystem] ❌ ClueDatabase를 찾을 수 없습니다! Inspector에서 할당하거나 Resources/Clues/ 폴더에 넣어주세요.");
                return;
            }
        }

        // 저장소 생성 
        IClueRepository repository = new PlayerPrefsClueRepository();

        // 디버그: 플레이 시작 시 단서 초기화
        if (resetOnPlay)
        {
            repository.ClearAll();
            Debug.Log("[ClueSystem] ⚠️ 단서 데이터 초기화됨 (resetOnPlay 활성화)");
        }

        // 서비스 생성 
        clueService = new ClueService(clueDatabase, repository);

        // UI 컴포넌트 생성
        boardUI = gameObject.AddComponent<ClueBoardUI>();
        boardUI.Initialize(clueService, customBackgroundImage);

        // 시작 시 보드는 닫힌 상태
        IsBoardOpen = false;
    }

    void Update()
    {
        // M키 입력 감지

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleBoard();
        }
    }

    // 단서 보드 토글 (열기/닫기) 
    private void ToggleBoard()
    {
        if (IsBoardOpen)
        {
            CloseBoard();
        }
        else
        {
            OpenBoard();
        }
    }

    // 단서 보드 열기 
    private void OpenBoard()
    {
        IsBoardOpen = true;

        // 게임 일시정지
        Time.timeScale = 0f;

        // 마우스 커서 표시 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // UI 표시
        boardUI.OpenBoard();

        Debug.Log("[ClueSystem] 📋 단서 보드 열림");
    }

    private void CloseBoard()
    {
        IsBoardOpen = false;

        // 게임 재개
        Time.timeScale = 1f;

        // 마우스 커서 다시 잠금 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // UI 숨기기
        boardUI.CloseBoard();

        Debug.Log("[ClueSystem] 📋 단서 보드 닫힘");
    }

    // 외부에서 호출
    public void DiscoverClue(string clueId)
    {
        bool isNew = clueService.DiscoverClue(clueId);
        if (isNew)
        {
            Debug.Log($"[ClueSystem] 새 단서를 발견했습니다!");

            // 새 단서 발견 시 자동으로 단서 보드 열기
            OpenBoard();
        }
    }
}
