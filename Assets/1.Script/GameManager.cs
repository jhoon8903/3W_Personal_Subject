using System;
using System.Collections.Generic;
using _1.Script.InGameUI;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using CharacterTypes = _1.Script.LoginPanel.CharacterCard.Character.CharacterTypes;

namespace _1.Script
{
    public class GameManager : MonoBehaviour
    {
        #region Character Information Variable
        public CharacterTypes CharacterType { get; set; }   // 케릭터 타입
        private Sprite _characterThumbnail; // 이미지 변수
        public event Action<Sprite> OnCharacterThumbnailChanged;    // 이미지 변경 이벤트 
        private AnimatorController _characterController;    // 컨트롤러
        public event Action<AnimatorController> OnCharacterControllerChanged;   // 컨트롤러 변경 이벤트
        #endregion

        #region UserInstance Setting
        private Dictionary<string, Sprite> _user = new Dictionary<string, Sprite>(); //  케릭터 이름 변수
        public event Action<Dictionary<string, Sprite>> OnUser;   //  이름 세팅 이벤트  
        #endregion

        #region Singleton to GameManager
        public static GameManager Instance { get; private set; }

        /// <summary>
        ///  GameManager 싱글턴 인스턴스 화
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
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

        #region Reject Move
        public bool inGame = false;
        #endregion
    }
}