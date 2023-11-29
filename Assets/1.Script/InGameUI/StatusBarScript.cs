using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InGameUI
{
    public class StatusBarScript : MonoBehaviour
    {
        private GameManager _gameManager;

        #region UnderBar UI Variables
        [SerializeField] private Button openUserBtn;    // 오픈 버튼 오브젝트
        #endregion

        #region SideBar UI Variables
        [SerializeField] private Image sidebar; // 사이드바 오브젝트
        [SerializeField] private Button closeBtn;   // 닫기 버튼 오브젝트
        [SerializeField] private Users userPrefab;  // 접속 유저마다 1개씩 생성 되는 Users Prefab 오브젝트 
        [SerializeField] private Transform contentTransform;    // 인스턴스가 생성 될 부모 오브젝트 위치
        #endregion

        private string _userName;
        private Sprite _userSprite;
        private GameManager.Character.CharacterTypes _type;
        private Users _userInstance; 

        #region Initilaze UI Instance
        /// <summary>
        ///     인게임 UI Initialize
        ///     버튼 이벤트 설정 및 사이드바 패널 초기상태를 설정 (비활성화)
        /// </summary>
        private void Awake()
        {
            openUserBtn.onClick.AddListener(OpenSideBar);   // 사이드바 onClick Open Event
            closeBtn.onClick.AddListener(CloseSideBar); // 사이드바 onClick Close Event
            sidebar.gameObject.SetActive(false);    // 사이드바 오브젝트 초기 값 (비활성화)
        }
        #endregion

        /// <summary>
        ///  GameManager의 OnUser 이벤트 구독
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _gameManager.OnChangedName += UserNameSetup;
            _gameManager.OnCharacterThumbnailChanged += UserSpriteSetUp;
            _gameManager.OnCharacterTypeChanged += TypeChange;
            _gameManager.OnCreateNpc += CreateNpc;
        }

        private void TypeChange(GameManager.Character.CharacterTypes obj)
        {
            _type = obj;
        }

        private void CreateNpc(object sender, CreateNPCEventArgs e)
        {
            Users npcInstance = Instantiate(userPrefab, contentTransform);
            npcInstance.SetInformation(e.NPCName, e.NPCSprite, _type);
        }

        private void UserSpriteSetUp(Sprite obj)
        {
            _userSprite = obj;
        }

        private void UserNameSetup(object sender, NameChangedEventArgs e)
        {
            _userName = e.NewName;
        }

        /// <summary>
        ///     사이드 바 오픈 메서드 (OnClick)
        /// </summary>
        private void OpenSideBar()
        {
            InstantiateUser();
            sidebar.gameObject.SetActive(true);
        }

        /// <summary>
        ///     사이드 바 클로즈 메서드 (OnClick)
        /// </summary>
        private void CloseSideBar()
        {
            sidebar.gameObject.SetActive(false);
        }

        private void InstantiateUser()
        {
            if (_userInstance != null) return;
            _userInstance = Instantiate(userPrefab, contentTransform);
            _userInstance.SetInformation(_userName, _userSprite, _type);
        }
    }
}
