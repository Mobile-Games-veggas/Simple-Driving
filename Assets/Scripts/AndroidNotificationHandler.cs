using System;
using System.Collections;
using System.Collections.Generic;
#if  UNITY_ANDROID // только для ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
#if  UNITY_ANDROID // только для ANDROID
    private const string ChannelId = "notification_channel";

    public void SchedulNotification(DateTime dateTime) // public тк будем запускать из другого класса
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel // создание канала уведомления
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel); // регистрация канала

        AndroidNotification notification = new AndroidNotification // создание самого уведомления
        {
            Title = "Energy Recharged!",
            Text = "Your Energy has recharges, come back to play again!",
            SmallIcon = "default", // можно прикрепить свои в Edit -> PS -> Mobile Notification -> Android -> +
            LargeIcon = "default", // -//-
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId); // вызов этого уведомления

    }
#endif
}
