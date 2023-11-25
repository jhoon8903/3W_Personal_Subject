using System;
using UnityEditor.Animations;
using UnityEngine;
using CharacterTypes = _1.Script.LoginPanel.CharacterCard.Character.CharacterTypes;

namespace _1.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private AnimatorController _characterController;
        public AnimatorController CharacterController
        {
            get => _characterController;
            set
            {
                _characterController = value;
                OnCharacterControllerChanged?.Invoke(_characterController);
            }
        }
        public CharacterTypes CharacterType { get; set; }

        public event Action<AnimatorController> OnCharacterControllerChanged;
        
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
        }
    }
}