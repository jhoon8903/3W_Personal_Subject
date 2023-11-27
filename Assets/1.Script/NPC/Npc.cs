using System;
using System.Collections.Generic;
using _1.Script.InGameUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Script.NPC
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private string npcNameText = "NPC\n뚱이에오";
        [SerializeField] private TextMeshProUGUI npcName;
        [SerializeField] private Sprite npcSprite;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            npcName.text = npcNameText;
        }

        private void Start()
        {
            GameManager.Instance.User = new Dictionary<string, Sprite>
            {
                {npcNameText, npcSprite }
            };
        }
    }
}
