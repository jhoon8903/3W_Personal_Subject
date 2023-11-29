using _1.Script.Controller;
using _1.Script.InGameUI;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace _1.Script.ActionScript
{
    public class CharacterAction  : MonoBehaviour
    {
        private GameManager _gameManager;
        private MovementEventController _movementEventController;

        [SerializeField] private TextMeshProUGUI characterName;

        #region Move Variables
        [SerializeField] private float speed;
        private Vector2 _moveDirection;
        private Rigidbody2D _rigidbody2D;
        #endregion

        #region LookForward Variables
        [SerializeField] private SpriteRenderer character;
        #endregion

        #region AnimationTrigger
        [SerializeField] private Animator animator;
        private enum Triggers { MOVE, IDLE }
        private Triggers _trigger;
        #endregion

        private GameManager.Character.CharacterTypes _characterType;

        private void Awake()
        {
            _movementEventController = GetComponent<MovementEventController>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _gameManager = ServiceLocator.GetService<GameManager>();
            _movementEventController.MoveEvent += Move;
            _movementEventController.LookForwardEvent += LookForward;
            _gameManager.OnCharacterControllerChanged += UpdateAnimatorController;
            // 이름변경 이벤트 감지 시 UpdateName 메서드 실행  
            _gameManager.OnChangedName += UpdateName;
            _gameManager.OnCharacterTypeChanged += ChangedType;
            CharacterInitialize();
        }

        private void ChangedType(GameManager.Character.CharacterTypes obj)
        {
            _characterType = obj;
        }

        /// <summary>
        ///  현재 이름과 비교하여 OldName과 현재 이름이 같다면, 새로운 이름으로 교체 합니다.
        /// </summary>
        /// <param name="sender"> 발신자 정보 </param>
        /// <param name="e">e.OldName, e.NewName</param>
        private void UpdateName(object sender, NameChangedEventArgs e)
        {
            if (e.OldName == characterName.text)
            {
                characterName.text = e.NewName;
            }
        }

        private void UpdateAnimatorController(AnimatorController newController)
        {
            animator.runtimeAnimatorController = newController;
            animator.SetTrigger(_trigger.ToString());
            _trigger = Triggers.IDLE;
        }

        private void CharacterInitialize()
        {
            animator.runtimeAnimatorController = _gameManager.CharacterController;
            animator.SetTrigger(_trigger.ToString());
            _trigger = Triggers.IDLE;
        }
                                                                                                                           
        private void FixedUpdate()
        { 
            if (!_gameManager.InGame) return;
            if (_gameManager.InGameChangingName) return;
            MoveToward(_moveDirection != Vector2.zero ? _moveDirection : Vector2.zero);
        }

        private void Move(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        private void LookForward(Vector2 lookDirection)
        {
            float rotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            character.flipX = Mathf.Abs(rotate) > 90f;
        }

        private void MoveToward(Vector2 moveDirection)
        {
            _trigger = moveDirection != Vector2.zero ? Triggers.MOVE : Triggers.IDLE;
            animator.SetTrigger(_trigger.ToString());
            Vector2 direction = moveDirection * speed;
            _rigidbody2D.velocity = direction;
        }
    }
}