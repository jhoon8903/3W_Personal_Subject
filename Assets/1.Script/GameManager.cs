using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace _1.Script
{
    public class GameManager : MonoBehaviour
    {
        #region Character Information Variable
        public Character.CharacterTypes CharacterType { get; set; }   // 케릭터 타입
        private Sprite _characterThumbnail; // 이미지 변수
        public event Action<Sprite> OnCharacterThumbnailChanged;    // 이미지 변경 이벤트 
        private AnimatorController _characterController;    // 컨트롤러
        public event Action<AnimatorController> OnCharacterControllerChanged;   // 컨트롤러 변경 이벤트
        #endregion

        #region UserInstance Setting
        private Dictionary<string, Sprite> _user = new Dictionary<string, Sprite>(); //  케릭터 이름 변수
        public event Action<Dictionary<string, Sprite>> OnUser;   //  이름 세팅 이벤트  
        #endregion

        #region Character InformationList
        /// <summary>
        ///  Character Class 로 케릭터의 타입(enum), 이미지 (Sprite), 인게임에서 사용할 컨트롤러(AnimatorController) 설정,
        ///  인스펙터 상에서 characters 리스트 형태로 할당하여 정보를 저장  
        /// </summary>
        [Serializable] public class Character
        {
            public enum CharacterTypes { FROG, PINKYMAN, ZEP }   // 케릭터의 유니크 타입 목록
            public CharacterTypes characterType;    // 케릭터의 유니크 타입값
            public Sprite characterImage;   // 케릭터의 기본 Sprite 
            public AnimatorController animator; // 인게임에서 사용할 케릭터 컨트롤러
        }

        [SerializeField] public List<Character> characters;    // Character Class를 리스트 형태로 관리
        #endregion

        #region Service Locator to GameManager
        /// <summary>
        ///  Service Locator Pattern 구현
        /// </summary>
        private void Awake()
        {
            Debug.Log("GameManager.cs - Register Service");
            ServiceLocator.RegisterService(this);
        }
        #endregion

        #region Changed Character Information Event Property
        /// <summary>
        ///  Thumbnail 변경시 이벤트 변경
        /// </summary>
        public Sprite CharacterThumbnail // 케릭터 선택 패널에서 보여 줄 케릭터 스프라이트
        {
            get => _characterThumbnail;
            set
            {
                _characterThumbnail = value;
                OnCharacterThumbnailChanged?.Invoke(_characterThumbnail);
            }
        }

        /// <summary>
        ///  Controller 변경 시 이벤트 변경
        /// </summary>
        public AnimatorController CharacterController
        {
            get => _characterController;
            set
            {
                _characterController = value;
                OnCharacterControllerChanged?.Invoke(_characterController);
            }
        }

        /// <summary>
        ///     케릭터 이름 세팅 프로퍼티 및 이벤트 실행
        /// </summary>
        public Dictionary<string, Sprite> User
        {
            set
            {
                _user = value;
                OnUser?.Invoke(_user);
            }
        }
        #endregion

        public event EventHandler OnClosePanelHandler;
        public void ClosePanel(object sender)
        {
            OnClosePanelHandler?.Invoke(sender, EventArgs.Empty);
        }

        #region Reject Move
        public bool InGame { get; set; } = false;
        #endregion
    }
}