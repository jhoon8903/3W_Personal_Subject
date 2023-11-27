using System.Collections.Generic;
using _1.Script.InGameUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.LoginPanel
{
    public class LoginPanel : MonoBehaviour
    {
        #region ID Input Variables
        private Image _inputPanel; // 로그인 인풋 패널 (배경)
        [SerializeField] private Button entranceBtn; // 입장 버튼 오브젝트
        [SerializeField] private TextMeshProUGUI characterName; // 케릭터의 이름을 할당할 TMP 오브젝트
        #endregion

        #region Selected Button Variables
        [SerializeField] private Button characterSelectBtn; // 케릭터 선택 패널 실행 버튼 오브젝트
        [SerializeField] private Image characterSelectPanel;    // 케릭터 선택 패널 오브젝트
        [SerializeField] private Image characterThumbnail;  //  케릭터 선택 버튼 위에 보여줄 케릭터 이미지
        #endregion

        #region Initialize Default Value
        /// <summary>
        ///  LoginPanel 초기화 메서드
        ///  인풋 필드 및 로그인 화면 케릭터 기본 값 세팅
        /// </summary>
        private void Start()
        {
            InitializedInPutField();    // 인풋 필드 초기화
            InitializedDefaultCharacter();  // 케릭터 세팅 초기화 (FROG)
            
            // 케릭터가 선택 되면 해당 케릭터 Sprite를 바꿔주는 이벤트
            GameManager.Instance.OnCharacterThumbnailChanged += CharacterSelectPanelClose;
        }
        #endregion

        #region Character Select Method Group
        private void InitializedDefaultCharacter()
        {
            // 케릭터 선택 버튼을 클릭하면 케릭터 선택 패널이 열리도록 이벤트 구독
            characterSelectBtn.onClick.AddListener(CharacterSelectPanelOpen);
            // 케릭터 기본 이미지 설정
            characterThumbnail.sprite = GameManager.Instance.CharacterThumbnail; 
        }

        /// <summary>
        ///  케릭터 선택 패널 오픈 메서드
        /// </summary>
        private void CharacterSelectPanelOpen()
        {
            // 케릭터 선택 패널 오브젝트 활성화
            characterSelectPanel.gameObject.SetActive(true);
        }

        /// <summary>
        ///  선택된 케릭터 Sprite를 받아 선택된 케릭터를 랜더링 후 케릭터 선택 패널 비활성화
        /// </summary>
        /// <param name="sprite">선택한 케릭터 Sprite</param>
        private void CharacterSelectPanelClose(Sprite sprite)
        {
            characterThumbnail.sprite = sprite;   // 케릭터 이미지 교체 
            characterSelectPanel.gameObject.SetActive(false);   // 케릭터 선택화면 비활성화
        }
        #endregion

        #region Input Panel Method Group
        /// <summary>
        ///  인풋 필드 초기화
        /// </summary>
        private void InitializedInPutField()
        {
            _inputPanel = GetComponent<Image>();    // 인풋 패널 할당
            _inputPanel.gameObject.SetActive(true); // 인풋 패널 오브젝트 활성화
            entranceBtn.onClick.AddListener(ClosePanel);    // 입장 버튼 이벤트 리스너 구독
            entranceBtn.interactable = false;   // 버튼의 초기값은 비활성화로 설정
        }

        /// <summary>
        ///  인풋 Text를 이벤트로 받아 글자 수에 따라 검증 조건에 따라 버튼 활성화 여부 확인
        /// </summary>
        /// <param name="value"></param>
        public void OnInputFieldEndEdit(string value)
        {
            characterName.text = value; // 케릭터 이름 할당
            entranceBtn.interactable = value.Length is >= 2 and < 10;   // 2자 이상 10자 미만
            GameManager.Instance.User = new Dictionary<string, Sprite>  // GameManager에 이름과 Sprite를 넘겨 줌
            {
                {value, characterThumbnail.sprite}
            };
        }

        /// <summary>
        ///  입장 버튼을 누르면 인풋 패널 비활성화
        /// </summary>
        private void ClosePanel()
        {
            GameManager.Instance.inGame = true;
            _inputPanel.gameObject.SetActive(false);    // 인풋 패널을 비활성화 하여 게임씬 노출
        }
        #endregion
    }
}
