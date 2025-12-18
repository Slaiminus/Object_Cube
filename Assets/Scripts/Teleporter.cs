using UnityEngine;


public class Teleporter : MonoBehaviour
{
    [Header("Settings of teleport")]
    public Transform teleportDestination;
    public float teleportDelay = 3f;

    [Header("effects")]
    public ParticleSystem teleportEffect;
    public AudioClip teleportSound;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportWithDelay(other.transform));
        }
    }

    System.Collections.IEnumerator TeleportWithDelay(Transform playerTransform)
    {
        isTeleporting = true;

        
        PlayTeleportEffects();

        
        Debug.Log($"Teleport after {teleportDelay} sec");
        yield return new WaitForSeconds(teleportDelay);

       
        playerTransform.position = teleportDestination.position;
        

        
        if (teleportEffect != null)
        {
            Instantiate(teleportEffect, teleportDestination.position, Quaternion.identity);
        }

        isTeleporting = false;
    }

    void PlayTeleportEffects()
    {
        if (teleportEffect != null)
        {
            Instantiate(teleportEffect, transform.position, Quaternion.identity);
        }

        if (teleportSound != null)
        {
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
        }
    }
}
