using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android; //for notifications

public class AndroidNotifications : MonoBehaviour
{
    // to create notifications we have to
    // create notification channel and add notifications to that channel

    // Start is called before the first frame update
    void Start()
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = "example_channer_id", // to identify channel in future
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic Notifications",

        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel); //register channel

        //new notification
        AndroidNotification notification = new AndroidNotification();
        //look of it
        notification.Title = "Hi ther";
        notification.Text = "Come back for new Mission";
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddSeconds(20); //send after 20 seconds

        // to actually send it:
        var identifier = AndroidNotificationCenter.SendNotification(notification, "example_channer_id");
        //until now if scene open more than oe time it can be send more than once
        //so we need this:
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "example_channer_id");

        }
    }

    
}
