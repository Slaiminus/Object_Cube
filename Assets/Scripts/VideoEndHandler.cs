using UnityEngine;
using UnityEngine.Video;

public class VideoEndHandler : MonoBehaviour
{
    [Header("Настройки видео")]
    [SerializeField] private VideoPlayer videoPlayer; 

    [Header("Объекты для управления")]
    [SerializeField] private GameObject cameraAnimObject; 
    [SerializeField] private GameObject firstPersonController; 

    [Header("Настройки")]
    [SerializeField] private bool disableOnAwake = true; 

    void Start()
    {
        
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                return;
            }
        }

        
        videoPlayer.loopPointReached += OnVideoEnd;

        
        if (disableOnAwake && firstPersonController != null)
        {
            firstPersonController.SetActive(false);
        }

        
        if (cameraAnimObject == null)
        {
            Debug.LogWarning("CameraAnim объект не назначен в инспекторе!");
        }

        if (firstPersonController == null)
        {
           
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        
        if (cameraAnimObject != null)
        {
            cameraAnimObject.SetActive(false);
            
        }

       
        if (firstPersonController != null)
        {
            firstPersonController.SetActive(true);
            
        }

        
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    
    public void SwitchToFirstPerson()
    {
        OnVideoEnd(videoPlayer);
    }

    void OnDestroy()
    {
       
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}