using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS // только для iOS
using Unity.Notifications.iOS;
#endif
using UnityEngine;

public class iOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS // только для iOS
    public void ScheduleNotification(int minutes)
    {
        iOSNotification notification = new iOSNotification
        {
            Title = "Energy Recharged",
            Subtitle = "Your energy has been rechanged",
            Body = "Your energy has been recharged, come back to play again!",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a", // если одно уведомление - не имеет смысла, если группировать по категориям, то можно будет отключить уведомления опеределлным группам
            ThreadIdentifier = "thread1",
            Trigger = new iOSNotificationTimeIntervalTrigger // когда запускать уведомление? Можно и в календарь уведомление добавить и тп
            {
                TimeInterval = new System.TimeSpan(0, minutes, 0), // часы, минуты, секунды когда push уведомление
                Repeats = false
            }
        };
        iOSNotificationCenter.ScheduleNotification(notification); // запуск уведомления
    }
#endif
}
