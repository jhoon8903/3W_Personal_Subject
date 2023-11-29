using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace _1.Script.InGameUI
{
    public class TimeUI : MonoBehaviour
    {
        private TextMeshProUGUI _time;
        private float _timer = 0f;
        private const string TimeZone = "Asia/Seoul";

        private void Start()
        {
            _time = GetComponent<TextMeshProUGUI>();
            UpdateTime();
        }

        /// <summary>
        ///  타이머가 1 보다 커지면 UpdateTime() 을 호출하여 매 초마다 시간이 변하게 됩니다.
        /// </summary>
        private void Update()
        {
            _timer += Time.deltaTime;
            if (!(_timer >= 1f)) return;
            UpdateTime();
            _timer = 0;
        }

        /// <summary>
        ///   한국시간을 표시
        /// </summary>
        private void UpdateTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo koreaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
            DateTime koreanTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, koreaTimeZone);

            // 2023/11/29
            // [오전] 05:19:34 

            //  AM/PM (오전/오후)
            string amPm = koreanTime.Hour < 12 ? "오전" : "오후";

            // 12시간을 기준으로 값이 바뀜
            int hour = koreanTime.Hour % 12;
            if (hour == 0)
            {
                hour = 12;
            }

            string formattedTime = $"{koreanTime:yyyy/MM/dd} \n[{amPm}] {hour:00}:{koreanTime:mm:ss}";

            _time.text = formattedTime;
        }
    }
}
