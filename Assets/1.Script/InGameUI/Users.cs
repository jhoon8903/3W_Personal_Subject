using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static _1.Script.GameManager.Character;

namespace _1.Script.InGameUI
{
    public class Users : MonoBehaviour
    {
        [SerializeField] private Image userAvatar;  // Image 컴포넌트
        [SerializeField] private TextMeshProUGUI userName;  // Text Mesh Pro 컴포넌트


        private GameManager _gameManager;
        private CharacterTypes _characterType;
        private string _currentName;    // 현재 이름

        /// 이벤트로 들어온 Name과 Sprite 를 저장 해둡니다.
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _gameManager.OnChangedName += UpdateName;
            _gameManager.OnCharacterThumbnailChanged += UpdateSprite;
        }

        /// <summary>
        ///  이벤트를 호출 받으면 해당 이름을 비교하여 _currentName과 e.OldName이 일치하는 객체만 변경합니다.
        /// </summary>
        /// <param name="sender"> 발신자 </param>
        /// <param name="e"> e.OldName, e.NewName </param>
        private void UpdateName(object sender, NameChangedEventArgs e)
        {
            if (e.OldName != _currentName) return;
            userName.text = e.NewName;
            _currentName = userName.text;
        }

        /// <summary>
        ///  스프라이트 변경 메서드가 들어오면 
        /// </summary>
        /// <param name="sprite"></param>
        private void UpdateSprite(Sprite sprite)
        {
            Debug.Log($"type : {_characterType}");
            if (_characterType != CharacterTypes.NPC)
            {
                userAvatar.sprite = sprite;
            }
        }

        public void SetInformation(string userNameText, Sprite userAvatarSprite, CharacterTypes type)
        {
            userName.text = userNameText;
            _currentName = userName.text;
            userAvatar.sprite = userAvatarSprite;
            _characterType = type;
        }
    }
}