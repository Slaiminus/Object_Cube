using UnityEngine;
using UnityEngine.UI;

public class SimpleItemCollector : MonoBehaviour
{
    public string itemTag = "Collectible";
    public float range = 3f;

    private int collected = 0;
    private int total = 8; // Фиксируем количество - 8 папок
    private Camera cam;
    private Text counterText;
    private Text promptText;

    void Start()
    {
        cam = Camera.main;

        // ГАРАНТИРУЕМ, что DoorOpener отключен при старте
        DisableDoorOpener();

        CreateUI();
    }

    // Метод для гарантированного отключения DoorOpener
    void DisableDoorOpener()
    {
        DoorOpener doorScript = FindAnyObjectByType<DoorOpener>();
        if (doorScript != null)
        {
            doorScript.enabled = false;
            Debug.Log("DoorOpener отключен при старте");
        }
        else
        {
            Debug.LogWarning("DoorOpener не найден на сцене");
        }
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        bool showPrompt = false;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag(itemTag))
            {
                showPrompt = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    collected++;
                    UpdateUI();

                    // Включаем DoorOpender ТОЛЬКО когда собраны ВСЕ 8
                    if (collected >= 8)
                    {
                        EnableDoorOpener();
                    }
                }
            }
        }

        if (promptText != null)
            promptText.gameObject.SetActive(showPrompt);

      
    }

    
    void EnableDoorOpener()
    {
        DoorOpener doorScript = FindAnyObjectByType<DoorOpener>();
        if (doorScript != null)
        {
            doorScript.enabled = true;
            Debug.Log($"DoorOpener ВКЛЮЧЕН! Собрано {collected}/8 папок");

            // Показываем сообщение
            if (counterText != null)
            {
                StartCoroutine(ShowMessage("ДВЕРЬ РАЗБЛОКИРОВАНА!", 3f));
            }
        }
    }

    // Метод для проверки статуса
    void CheckDoorOpenerStatus()
    {
        DoorOpener doorScript = FindAnyObjectByType<DoorOpener>();
        if (doorScript != null)
        {
            Debug.Log($"DoorOpener.enabled = {doorScript.enabled}, Собрано: {collected}/8");
        }
        else
        {
            Debug.Log("DoorOpener не найден!");
        }
    }

    System.Collections.IEnumerator ShowMessage(string message, float duration)
    {
        string originalText = counterText.text;
        Color originalColor = counterText.color;

        counterText.text = message;
        counterText.color = Color.green;

        yield return new WaitForSeconds(duration);

        counterText.text = originalText;
        counterText.color = originalColor;
    }

    void CreateUI()
    {
        // Найти Canvas
        GameObject canvas = GameObject.Find("FirstPersonController/Joint/PlayerCamera/CrosshairAndStamina");
        if (!canvas) return;

        // Счётчик
        GameObject counter = new GameObject("Counter");
        counter.transform.SetParent(canvas.transform, false);
        counterText = counter.AddComponent<Text>();
        counterText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        counterText.fontSize = 24; 
        counterText.color = Color.white;
        counterText.alignment = TextAnchor.UpperRight;

        RectTransform crt = counter.GetComponent<RectTransform>();
        crt.anchorMin = new Vector2(1, 1);
        crt.anchorMax = new Vector2(1, 1);
        crt.pivot = new Vector2(1, 1);
        crt.anchoredPosition = new Vector2(-10, -10);

       
        GameObject prompt = new GameObject("Prompt");
        prompt.transform.SetParent(canvas.transform, false);
        promptText = prompt.AddComponent<Text>();
        promptText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        promptText.fontSize = 18;
        promptText.color = Color.yellow;
        promptText.alignment = TextAnchor.MiddleCenter;
        promptText.text = "Нажми E чтобы взять";

        RectTransform prt = prompt.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0.5f, 0.5f);
        prt.anchorMax = new Vector2(0.5f, 0.5f);
        prt.pivot = new Vector2(0.5f, 0.5f);
        prt.anchoredPosition = new Vector2(0, -100);

        prompt.SetActive(false);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = $"Собрано: {collected}/{total}";
    }
}