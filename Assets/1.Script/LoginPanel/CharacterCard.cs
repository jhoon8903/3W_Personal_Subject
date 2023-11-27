using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace _1.Script.LoginPanel
{
    public class CharacterCard : MonoBehaviour
    {
        private GameManager _gameManager;

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

        [SerializeField] private List<Character> characters;    // Character Class를 리스트 형태로 관리
        #endregion

        #region Character Select Variables
        [SerializeField] private Transform selectPanelTransform;    // 케릭터 카드를 랜더링할 부모 오브젝트의 위치
        [SerializeField] private SelectCard selectCardPrefab;   // 인스턴스화 할 케릭터 프리팹
        #endregion

        #region Default Character Initilaze
        /// <summary>
        ///  기본 InputField 화면의 기본 케릭터 설정
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            Character defaultCharacter = characters[0];
            _gameManager.CharacterController = defaultCharacter.animator;  // 기본 케릭터 Controller GameManager로 할당
            _gameManager.CharacterType = defaultCharacter.characterType;   // 기본 케릭터 타입 GameManager로 할당
            _gameManager.CharacterThumbnail = defaultCharacter.characterImage;  // 기본 케릭터 이미지 GameManager 할당
            InstantiateSelectCard();    // 선택화면의 케릭터 오브젝트를 인스턴스화
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
        #endregion
    }
}
