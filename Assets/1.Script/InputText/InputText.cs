using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InputText
{
    public class InputText : MonoBehaviour
    {
        private Image _inputPanel;
        [SerializeField] private Button entranceBtn;
        [SerializeField] private TextMeshProUGUI characterName;

        private void Awake()
        {
            _inputPanel = GetComponent<Image>();
            entranceBtn.onClick.AddListener(ClosePanel);
            entranceBtn.interactable = false;
        }

        private void Start()
        {
            _inputPanel.gameObject.SetActive(true);
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
