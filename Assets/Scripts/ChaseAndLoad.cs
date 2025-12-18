using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaseAndLoad : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 3f;
    public AudioSource audioSource;
    private bool musicStarted = false;

    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
           
            if (!musicStarted && audioSource != null)
            {
                audioSource.Play();
                musicStarted = true;
            }

            Vector3 toPlayer = player.position - transform.position;
            toPlayer.y = 0;


            if (toPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(toPlayer) * Quaternion.Euler(0, -90, 0);
            }


            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}