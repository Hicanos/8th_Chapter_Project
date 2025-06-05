# 8th_Chapter_Project
8번째 챕터 개인 프로젝트
3D Auto Play (3D 자동 사냥 게임)

## Scene
### 1. MainScene: 3D 방치형 RPG 게임 메인
### 2. ClassSene: 3D 심화 강의 내용 따라가기

## 필수 구현

- [ ] 기본 UI 구현(UGUI)    
+ 게임 화면에 HP, MP, 경험치 바, 현재 스테이지, 골드 및 재화 정보 표시   
   
- [ ] 플레이어 AI 시스템 (FSM, Coroutine)
+ 플레이어가 직접 조작하지 않아도 전진, 적 발견 시 일정 시간마다 자동으로 적 공격   
   
- [ ] 아이템 및 업그레이드 시스템(강화)   
+ 사용자가 아이템 구매, 업그레이드 할 수 있음   
+ 특정 아이템을 사용하여 플레이어의 스탯을 일시적으로 강화   
   예: 체력 포션 사용시 즉시 회복, 공격력 스크롤 사용 시 30초 동안 공격력 10% 증가   
   
- [ ] 게임 내 통화 시스템   
+ 게임 내에서 사용 가능한 가상의 통화 시스템. 클릭, 혹은 게임 내 활동으로 획득 가능   
    
- [ ] 아이템 및 장비창 UI 구현   
+ 화면의 버튼 클릭 시 인벤토리 창이 열리고, 장비 장착/제거 가능   
   
- [ ] 스테이지 시스템   
+ 다양한 스테이지 구성, 원하는 스테이지 선택하여 입장 가능

- [ ] Scriptable Object를 이용한 데이터 관리   
+ 다양한 데이터를 Scriptable Object로 관리
 1. 아이템 정의
 2. 적의 데이터 정의
 3. 스테이지에 대한 데이터 (몬스터 정보, 보상)을 저장


# 코드 컨벤션 및 커밋 컨벤션 가이드

여기서부터는 프로젝트에서 사용하는 코드 컨벤션과 Git 커밋 컨벤션을 정의합니다. 
일관성 있는 코드 스타일과 명확한 커밋 이력 관리를 통해 이후 팀 프로젝트 등에서도 효율적인 협업 및 유지보수를 목표로 합니다.

## Unity C# 코드 컨벤션

Unity 프로젝트 개발 시 사용하는 C# 코드 스타일 가이드입니다.

### 1. 명명 규칙 (Naming Conventions)

*   **클래스, 메서드, 네임스페이스, 퍼블릭 변수/프로퍼티:** PascalCase (첫 글자 포함 각 단어의 첫 글자를 대문자로 시작)를 사용합니다.
    ```csharp
    public class PlayerMovement
    {
        public int PlayerSpeed;
        public void MoveCharacter() { }
    }
    ```
*   **프라이빗/프로텍티드 변수:** camelCase (첫 단어는 소문자, 이후 각 단어의 첫 글자는 대문자로 시작) 또는 `_` (언더스코어) 접두사를 사용합니다.
    ```csharp
    private float moveSpeed;
    private Rigidbody _playerRigidbody; // 언더스코어 스타일
    ```
*   **상수 (Const):** 일반적으로 PascalCase를 사용하며, 모두 대문자로 쓰는 경우도 있습니다. 여기서는 사용 시 **모두 대문자**로 씁니다.
    ```csharp
    public const float MaxHealth = 100f;
    private const int MIN_DAMAGE = 10; // 모두 대문자 스타일
    ```
*   **매개변수 (Parameters):** camelCase를 사용합니다.
    ```csharp
    void SetPlayerName(string newName) { }
    ```

### 2. 코드 서식 (Code Formatting)

*   **들여쓰기 (Indentation):** 탭 또는 공백 4칸을 일관되게 사용합니다.
*   **중괄호 (Curly Braces `{}`):** 여는 중괄호 `{`는 해당 구문의 첫 줄 끝에 두고, 닫는 중괄호 `}`는 단독으로 새 줄에 둡니다.
    ```csharp
    if (isMoving)
    {
        // 코드 내용
    }
    ```
*   **공백 (Spacing):** 연산자 주변이나 괄호 뒤에 공백을 사용하여 가독성을 높입니다.
    ```csharp
    int totalScore = score1 + score2;
    MyMethod(parameter1, parameter2);
    ```
*   **한 줄에 여러 구문 작성 금지:** 한 줄에는 하나의 구문만 작성합니다.

### 3. 주석 및 문서화 (Comments and Documentation)

*   코드의 의도나 복잡한 로직 설명을 위해 주석 (`//` 또는 `/* */`)을 사용합니다.
*   필요 시 클래스, 메서드, 변수 등에 XML 주석 (`///`)을 사용하여 문서화하는 것을 권장합니다.
    ```csharp
    /// <summary>
    /// 플레이어의 이동을 관리하는 클래스입니다.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// 캐릭터를 특정 방향으로 이동시킵니다.
        /// </summary>
        /// <param name="direction">이동할 방향 벡터</param>
        public void MoveCharacter(Vector3 direction)
        {
            // 이동 로직
        }
    }
    ```

### 4. 기타 Unity 관련 팁

*   `GetComponent` 호출은 `Awake` 또는 `Start`에서 한 번만 수행하고 변수에 저장하여 사용합니다.
*   초기화 순서를 고려하여 `Awake`와 `Start`를 적절히 활용합니다.
*   물리 연산은 `FixedUpdate`, 일반 로직은 `Update`, UI는 `LateUpdate`에서 처리하는 것을 고려합니다.


## GitHub Commit Convention

GitHub Desktop에 Commit 시 사용하는 Commit 스타일 가이드입니다.


### 구성 요소 설명

*   **type:** 커밋의 종류를 나타냅니다.
    *   `Add`: 새로운 파일 추가
    *   `Remove`: 기존 파일 삭제
    *   `Feat`: 새로운 기능 추가
    *   `Fix`: 버그 수정
    *   `Docs`: 문서 수정
    *   `Style`: 코드 포맷, 세미콜론 등 (코드 내용 변경 없음)
    *   `Refactor`: 코드 리팩토링 (기능 변경 없음)
    *   `Test`: 테스트 코드 추가/수정
    *   `Chore`: 빌드, 패키지 설정 등 기타 사항
    *   `Design`: UI 디자인 변경 
    *   `!HOTFIX`: 치명적인 버그 긴급 수정
*   **subject:** 변경 내용을 간결하게 요약합니다 (영문 작성 시 동사 원형 시작, 50자 이내, 마침표 없음 권장).
*   **body (본문):** 커밋 내용을 자세히 설명합니다 ("어떻게"보다는 "무엇을" "왜" 변경했는지 초점). 각 줄은 75자 이내 권장.
*   **footer (꼬리말):** 해당 프로젝트에서는 사용하지 않습니다.

### 커밋 메시지 예시

type: subject

body (optional)

footer (optional) - 현재 프로젝트에서는 없음 처리

```
Feat: Add player health system

플레이어 HP 바와 피해 처리가 구현되었습니다.
피해를 받으면 체력이 감소합니다.
체력이 0이 되면 게임 오버가 됩니다.

