using UnityEngine;
using UnityEngine.InputSystem;

namespace _1.Script.Controller
{
    public class PlayerController  : MovementEventController
    {
        private Camera _camera;

        /// <summary>
        ///  스크립트가 처음으로
        /// 메인 카메라를 하이어라키에서 찾아 할당하는 메서드
        /// </summary>
        private void Awake()
        {
            // 기존 카메라를 Virtual Camera 로 대체
            _camera = Camera.main;
        }

        /// <summary>
        ///  이동 관련 message를 수신하여 이벤트를 호출하는 메서드
        /// </summary>
        /// <param name="value"></param>
        public void OnMove(InputValue value)
        {
            // 매개변수로 전달받은 'value'에서 Vector2 값을 찾아 정규화 하여
            // 대각선에 대해서 일정한 속도를 유지 할 수 있도록 보정하는 코드
            Vector2 moveValue = value.Get<Vector2>().normalized;

            // 보정된 Vector2 값을 호출하는 이벤트 메서드
            CallMoveEvent(moveValue);
        }

        /// <summary>
        ///  회전 관련 message를 수신하여 이벤트를 호출하는 메서드
        /// </summary>
        /// <param name="value"></param>
        public void OnLookForward(InputValue value)
        {
            // 매개변수로 받은 벨류값에서 Vector2를 찾음
            Vector2 lookValue = value.Get<Vector2>();
        
            // 마우스 좌표는 카메라의 좌표와는 다르기 때문에 혀재 스크린의 좌표로 변환
            // Vector2 worldPos = _camera.ScreenToWorldPoint(lookValue);
            Vector2 worldPos = _camera.ScreenToWorldPoint(new Vector3(lookValue.x, lookValue.y, _camera.nearClipPlane));
            // 카메라의 값과, 실제 오브젝트의 vector 값을 비교하여 정규화 하여 보장된 상대적 좌표 값을 반환
            Vector2 relativeLookValue = (worldPos - (Vector2)transform.position).normalized;
        
            if (relativeLookValue.magnitude >= 0.9f)
            {
                // 이벤트 호출
                CallLookForwardEvent(relativeLookValue);
            }
        }
    }
}