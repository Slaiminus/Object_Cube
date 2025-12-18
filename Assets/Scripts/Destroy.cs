using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Настройки")]
    public bool destroyAllEnemies = false;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            DestroyEnemies();
        }
    }

   

    private void DestroyEnemies()
    {
        if (destroyAllEnemies)
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            Debug.Log($"Уничтожено {enemies.Length} врагов");
        }
        else
        {
            
            GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
            if (enemy != null)
            {
                Destroy(enemy);
               
            }
        }

        
    }
}
