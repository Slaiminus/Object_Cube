using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SimpleVideoTrigger : MonoBehaviour
{
    public VideoClip videoClip; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayVideo();
        }
    }

    void PlayVideo()
    {
        
        GameObject canvasObj = GameObject.Find("FirstPersonController/Joint/PlayerCamera/CrosshairAndStamina");
        if (canvasObj == null) return;

        Canvas canvas = canvasObj.GetComponent<Canvas>();
        if (canvas == null) return;

        
        GameObject videoObject = new GameObject("VideoPlayer");
        videoObject.transform.SetParent(canvas.transform, false);

       
        RawImage videoScreen = videoObject.AddComponent<RawImage>();

        
        RectTransform rt = videoScreen.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        
        VideoPlayer videoPlayer = videoObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
        videoPlayer.targetTexture = renderTexture;
        videoScreen.texture = renderTexture;

        
        videoPlayer.Play();

        
        videoPlayer.loopPointReached += (vp) => {
            Destroy(videoObject);
        };

        
        Destroy(gameObject);
    }
}