using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InGameUI
{
    public class StatusBarScript : MonoBehaviour
    {
        #region UnderBar UI Variables
        [SerializeField] private Button openUserBtn;    // 오픈 버튼 오브젝트
        #endregion

        #region SideBar UI Variables
        [SerializeField] private Image sidebar; // 사이드바 오브젝트
        [SerializeField] private Button closeBtn;   // 닫기 버튼 오브젝트
        [SerializeField] private Users userPrefab;  // 접속 유저마다 1개씩 생성 되는 Users Prefab 오브젝트 
        [SerializeField] private Transform contentTransform;    // 인스턴스가 생성 될 부모 오브젝트 위치
        #endregion

        #region Initilaze UI Instance
        /// <summary>
        ///     인게임 UI Initialize
        ///     버튼 이벤트 설정 및 사이드바 패널 초기상태를 설정 (비활성화)
        /// </summary>
        private void Awake()
        {
            openUserBtn.onClick.AddListener(OpenSideBar);   // 사이드바 onClick Open Event
            closeBtn.onClick.AddListener(CloseSideBar); // 사이드바 onClick Close Event
            sidebar.gameObject.SetActive(false);    // 사이드바 오브젝트 초기 값 (비활성화)
        }
        #endregion

        /// <summary>
        ///  GameMaanger의 OnUser 이벤트 구독
        /// </summary>
        private void Start()
        {
            GameManager.Instance.OnUser += InstantiateUser;
        }

        private void OpenSideBar()
        {
            sidebar.gameObject.SetActive(true);
        }

        private void CloseSideBar()
        {
            sidebar.gameObject.SetActive(false);
        }

        /// <summary>
        ///     Callback 으로 메서드가 실행되면 Instantiate로 프리팹을 인스턴스화 하고
        ///     이벤트에서 전달 받은 메서드를 Users 인스턴스에 세팅합니다.
        /// </summary>
        /// <param name="user"></param>
        private void InstantiateUser(Dictionary<string, Sprite> user)
        {
            foreach (var info in user)
            {
                Users userInstance = Instantiate(userPrefab, contentTransform);
                userInstance.SetInformation(info.Key, info.Value);
            }
        }
    }
}
