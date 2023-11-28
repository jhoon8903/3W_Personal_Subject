using System;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static _1.Script.GameManager.Character;

namespace _1.Script.InGameUI
{
    public class AvatarInstance : MonoBehaviour
    {
        [SerializeField] private Image avatar;
        [SerializeField] private TextMeshProUGUI avatarName;
        private Button _selectBtn;
        private GameManager _gameManager;
        private AnimatorController _controller;

        /// <summary>
        ///  GameManager Service 등록
        /// </summary>
        public void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _selectBtn = GetComponent<Button>();
            _selectBtn.onClick.AddListener(SelectedAvatar);
        }

        /// <summary>
        ///  인스턴스화 시에 할당할 세팅 메서드
        /// </summary>
        /// <param name="type">케릭터의 이름</param>
        /// <param name="avatarSprite">케릭터의 이미지</param>
        /// <param name="controller">선택시에 캐릭터에 넘겨 줄 애니메이터 컨트롤러</param>
        public void SetAvatar (CharacterTypes type, Sprite avatarSprite, AnimatorController controller)
        {
            avatar.sprite = avatarSprite;
            avatarName.text = type.ToString();
            _controller = controller;
        }

        /// <summary>
        ///  선탣된 아바타의 컨르롤러 정보를 GameManager에 전달
        /// </summary>
        private void SelectedAvatar()
        {
            _gameManager.CharacterController = _controller;
            _gameManager.ClosePanel(this);
        }
    }
}