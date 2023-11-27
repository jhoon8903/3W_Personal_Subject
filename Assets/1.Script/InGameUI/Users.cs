using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.InGameUI
{
    public class Users : MonoBehaviour
    {
        [SerializeField] private Image userAvatar;  // Image 컴포넌트
        [SerializeField] private TextMeshProUGUI userName;  // Text Mesh Pro 컴포넌트

        public void SetInformation(string userNameText, Sprite userAvatarSprite)
        {
            userName.text = userNameText;
            userAvatar.sprite = userAvatarSprite;
        }
    }
}