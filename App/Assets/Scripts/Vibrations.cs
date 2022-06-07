using UnityEngine;

public static class Vibrations  // static - > u can access it anywhere inside project
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    // Function to call from the project
    public static void Vibrate(long miliseconds = 250)
    {
        if (IsAndroid())
        {
            vibrator.Call("vibrate", miliseconds);
        }
        else
        {
            Handheld.Vibrate();
        }
    }
    public static void Cancel()
    {
        if (IsAndroid())
        {
            vibrator.Call("cancel");
        }
    }

    //are we on android
    public static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif

    }

}
