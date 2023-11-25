using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _1.Script.LoginPanel
{

    public class CharacterCard : MonoBehaviour
    {
        [Serializable]
        public class Character
        {
            public enum CharacterTypes { FROG, PINKYMAN }
            public CharacterTypes characterType;
            public Sprite characterImage; 
            public AnimatorController animator;
        }
        [SerializeField] private List<Character> characters;
        [SerializeField] private Transform selectPanelTransform;
        [SerializeField] private SelectCard selectCardPrefab;
        public Image characterThumbnail;

        private void Awake()
        {
            characterThumbnail = GetComponent<Image>();
        }

        private void Start()
        {
            characterThumbnail.sprite = characters[0].characterImage;
            GameManager.Instance.CharacterController = characters[0].animator;
            GameManager.Instance.CharacterType = characters[0].characterType;
            InstantiateSelectCard();
        }

        private void InstantiateSelectCard()
        {
            foreach (var character in characters)
            {
                SelectCard cardInstance = Instantiate(selectCardPrefab, selectPanelTransform);
                cardInstance.CharacterType = character.characterType;
                cardInstance.CharacterSprite = character.characterImage;
                cardInstance.Controller = character.animator;
            }
        }
    }
}
