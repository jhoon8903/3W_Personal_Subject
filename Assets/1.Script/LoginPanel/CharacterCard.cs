using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace _1.Script.LoginPanel
{
    public class CharacterCard : MonoBehaviour
    {
        private GameManager _gameManager;

        #region Character Select Variables
        [SerializeField] private Transform selectPanelTransform;    // 케릭터 카드를 랜더링할 부모 오브젝트의 위치
        [SerializeField] private SelectCard selectCardPrefab;   // 인스턴스화 할 케릭터 프리팹
        private List<GameManager.Character> _characterList = new List<GameManager.Character>();
        #endregion

        #region Default Character Initilaze
        /// <summary>
        ///  기본 InputField 화면의 기본 케릭터 설정
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _characterList = _gameManager.characters;
            GameManager.Character defaultCharacter = _characterList[0];
            _gameManager.CharacterController = defaultCharacter.animator;  // 기본 케릭터 Controller GameManager로 할당
            _gameManager.CharacterType = defaultCharacter.characterType;   // 기본 케릭터 타입 GameManager로 할당
            _gameManager.CharacterThumbnail = defaultCharacter.characterImage;  // 기본 케릭터 이미지 GameManager 할당
            InstantiateSelectCard();    // 선택화면의 케릭터 오브젝트를 인스턴스화
        }

        private void InstantiateSelectCard()
        {
            foreach (var character in _characterList )
            {
                SelectCard cardInstance = Instantiate(selectCardPrefab, selectPanelTransform);
                cardInstance.CharacterType = character.characterType;
                cardInstance.CharacterSprite = character.characterImage;
                cardInstance.Controller = character.animator;
            }
        }
        #endregion
    }
}
