using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.LoginPanel
{
    public class LoginPanel : MonoBehaviour
    {
        private Image _inputPanel;
        [SerializeField] private Button entranceBtn;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private Button characterSelectBtn;
        [SerializeField] private Image characterSelectPanel;
        [SerializeField] private CharacterCard characterCard;
        public static LoginPanel Instance;
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

        private void Start()
        {
            _inputPanel = GetComponent<Image>();
            entranceBtn.onClick.AddListener(ClosePanel);
            entranceBtn.interactable = false;
            characterSelectBtn.onClick.AddListener(CharacterSelectPanelOpen);
            _inputPanel.gameObject.SetActive(true);
        }

        public void CharacterSelectPanelOpen()
        {
            characterSelectPanel.gameObject.SetActive(true);
        }

        public void CharacterSelectPanelClose(Sprite sprite)
        {
            characterCard.characterThumbnail.sprite = sprite;
            characterSelectPanel.gameObject.SetActive(false);
        }

        private void ClosePanel()
        {
            _inputPanel.gameObject.SetActive(false);
        }

        public void OnInputFieldEndEdit(string value)
        {
            characterName.text = value;
            entranceBtn.interactable = value.Length is >= 2 and < 10;
        }
    }
}
