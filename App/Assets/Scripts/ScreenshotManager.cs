using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    private static ScreenshotManager instance;
    private Camera myCamera;
    private bool takeScreenshotAtNextFrame;
    int width;
    int height;
    public GameObject screenTakenInfo;
    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        width = Screen.width;
        height = Screen.height;
        
    }
    private void OnPostRender() // this every frame
    {
        if (takeScreenshotAtNextFrame)
        {
            takeScreenshotAtNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false); // create this big texture
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0); // write what in rect to renderResult

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray); // write where, what
            Debug.Log("Screenshot saved");
            StartCoroutine(ScreenTaken());

            //cleanup
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;

        }
    }
    private void TakeScreenshot()
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotAtNextFrame = true;
    }
    public static void TakeScreenshot_Static()
    {
        instance.TakeScreenshot();
    }
    private IEnumerator ScreenTaken()
    {
        screenTakenInfo.SetActive(true);
        yield return new WaitForSeconds(1);
        screenTakenInfo.SetActive(false);
    }
}
