using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static _1.Script.LoginPanel.CharacterCard.Character;

namespace _1.Script.LoginPanel
{
    public class SelectCard : MonoBehaviour
    {
        public CharacterTypes CharacterType { get; set; }
        public Sprite CharacterSprite { get; set; }
        public AnimatorController Controller { get; set; }
        private Button _selectBtn;
        private Image _characterImage;
        private void Awake()
        {
            _selectBtn = GetComponent<Button>();
            _selectBtn.onClick.AddListener(Selected);
            _characterImage = GetComponent<Image>();
        }

        private void Start()
        {
            _characterImage.sprite = CharacterSprite;
        }

        private void Selected()
        {
            GameManager.Instance.CharacterType = CharacterType;
            GameManager.Instance.CharacterController = Controller;
            LoginPanel.Instance.CharacterSelectPanelClose(CharacterSprite);
        }
    }
}