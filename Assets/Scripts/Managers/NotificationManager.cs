using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Id = "main_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification()
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Penguin is waiting for you!",
            Text = "Don't forget about your friend!🐧",
            FireTime = System.DateTime.Now.AddDays(1),
        };
        AndroidNotificationCenter.SendNotification(notification, "main_channel");
    }

    private void OnApplicationQuit()
    {
        SendNotification();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SendNotification();
    }
}
