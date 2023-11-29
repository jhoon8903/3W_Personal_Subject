using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InGameUI
{
    public class ChangedName : MonoBehaviour
    {

        [SerializeField] private Button openBtn;
        [SerializeField] private  Button confirmBtn;
        [SerializeField] private TMP_InputField textMesh;
        private GameManager _gameManager;
        private string _changeNameText;

        /// <summary>
        ///  이름 바꾸기 기능 초기화
        /// </summary>
        private void Awake()
        {
            confirmBtn.onClick.AddListener(ChangeName);
            openBtn.onClick.AddListener(OpenPanel);
            textMesh.onEndEdit.AddListener(OnInputFieldEndEdit);
        }

        /// <summary>
        ///  GameManager를 의존성 주입 후 패널 비활성화
        /// </summary>
        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            gameObject.SetActive(false);
        }

        /// <summary>
        ///  이름 바꾸기 패널 활성화, 캐릭터가 움직이는 것을 막기 위해
        ///  bool 값으로 상태 변경
        /// </summary>
        private void OpenPanel()
        {
            gameObject.SetActive(true);
            _gameManager.InGameChangingName = true;
        }

        /// <summary>
        ///  이름 바꾸기에서 InputField 에서 들어온 이벤트를 호출하여 변수에 할당
        /// </summary>
        /// <param name="changeName"></param>
        private void OnInputFieldEndEdit(string changeName)
        {
            _changeNameText = changeName;
        }

        /// <summary>
        ///  닫기 버튼을 누르면 GameManger에 EventHandler 호출
        ///  창이 닫히면 캐릭터를 다시 움직일 수 있도록 bool 값 변경
        /// </summary>
        private void ChangeName()
        {
            _gameManager.ChangeName(this,_changeNameText);
            gameObject.SetActive(false);
            _gameManager.InGameChangingName = false;
        }
    }
}