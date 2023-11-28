using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InGameUI
{
    public class SelectedAvatar : MonoBehaviour
    {
        [SerializeField] private Image avatarSelectPanel;
        [SerializeField] private AvatarInstance avatarInstance;
        private GameManager _gameManager;
        private Button _openBtn;
        private List<GameManager.Character> _avatarList = new List<GameManager.Character>();
        private AvatarInstance _avatar;

        /// <summary>
        ///  스크립트 초기화 GameManager 서비스를 검색하고, 버튼 이벤트 활성화
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _gameManager.OnClosePanelHandler += ClosePanel;
            _avatarList = _gameManager.characters;
            _openBtn = GetComponent<Button>();
            _openBtn.onClick.AddListener(OpenAvatarSelectPanel);
            }

        /// <summary>
        ///  선택 화면 패널 활성화
        /// </summary>
        private void OpenAvatarSelectPanel()
        {
            avatarSelectPanel.gameObject.SetActive(true);
            InstantiateAvatar();
        }

        /// <summary>
        ///  패널이 닫히는 이벤트 수신 메서드
        ///  sender가 AvatarInstance 일때만 패널이 닫힙니다.
        /// </summary>
        /// <param name="sender">이벤트 메세지의 발신자</param>
        /// <param name="e">EventArgs 매개변수 여기서는 EventArgs.Empty</param>
        private void ClosePanel(object sender, EventArgs e)
        {
            switch (sender)
            {
                case AvatarInstance:
                    avatarSelectPanel.gameObject.SetActive(false);
                    break;
            }
        }

        /// <summary>
        ///  패널이 열릴때 AvtarInstance를 인스턴스화
        ///  중복 생성을 방지 하기 위해서 Null 일때만 생성하고 그 외에는 return 처리
        /// </summary>
        private void InstantiateAvatar()
        {
            if (_avatar != null) return;
            foreach (var character in _avatarList)
            {
                _avatar = Instantiate(avatarInstance, avatarSelectPanel.transform);
                _avatar.SetAvatar(character.characterType, character.characterImage, character.animator);
            }
        }
    }
}

