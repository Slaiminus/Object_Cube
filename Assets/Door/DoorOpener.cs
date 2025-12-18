using System.Threading;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Oss;
    [SerializeField] private float distanceCheck;

    private float distance;
    private bool _isOpened = false;
    float timersss = 2f;



    void FixedUpdate()
    {
        distance = (transform.position - player.transform.position).magnitude;
        //Debug.Log(distance);
        if (Input.GetKeyDown(KeyCode.E) && (distance < distanceCheck) && !_isOpened)
        {
            _isOpened = true;
        }
        if (timersss > 0 && _isOpened)
        {
            Oss.transform.Rotate(0f, -1.5f, 0f);
            timersss -= Time.deltaTime;
        }
        if (timersss < 0f)
        {
            //дверь полностью открыта, тут можно добавить скрипт по желанию
        }
    }
}

