using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static _1.Script.LoginPanel.CharacterCard.Character;

namespace _1.Script.LoginPanel
{
    public class SelectCard : MonoBehaviour
    {
        private GameManager _gameManager;

        #region Character Card Variables
        // 해당 케릭터가 인스턴화 될 때 설정되는 케릭터 정보
        public CharacterTypes CharacterType { get; set; }
        public Sprite CharacterSprite { get; set; }
        public AnimatorController Controller { get; set; }
        #endregion

        #region Object Variables
        private Button _selectBtn;
        private Image _characterImage;
        #endregion

        #region Initliaze Component
        /// <summary>
        ///  각 컴포넌트 및 이벤트 초기화
        /// </summary>
        private void Awake()
        {
            _selectBtn = GetComponent<Button>();    // 버튼 컴포넌트 할당
            _selectBtn.onClick.AddListener(Selected);   // 버튼 클릭 이벤트 준비 (Callback => Selected)
            _characterImage = GetComponent<Image>();    // 케릭터 컴포넌트 할당
        }

        /// <summary>
        ///  기본 인스턴스의 케릭터 이미지 세팅
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _characterImage.sprite = CharacterSprite;
        }
        #endregion

        #region Callback Event
        /// <summary>
        ///  버튼 클릭 이벤트 발생시 Callback으로 실행되는 메서드
        ///  GameManager에 해당 케릭터 정보를 전달 합니다.
        /// </summary>
        private void Selected()
        {
            _gameManager.CharacterType = CharacterType;
            _gameManager.CharacterController = Controller;                 
            _gameManager.CharacterThumbnail = CharacterSprite;
        }
        #endregion
    }
}