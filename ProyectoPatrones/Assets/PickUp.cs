using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Invetory inventory;
    public GameObject itemButton;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Invetory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.items.Length; i++)
            {
                Transform slotTransform = inventory.items[i].transform;

                if (slotTransform.childCount == 0)
                {

                    GameObject buttonInstance = Instantiate(itemButton);
                    buttonInstance.transform.SetParent(slotTransform, false);

                    RectTransform rect = buttonInstance.GetComponent<RectTransform>();
                    rect.anchoredPosition = Vector2.zero;  // Center in UI slot
                    rect.localRotation = Quaternion.identity;
                    rect.localScale = Vector3.one;


                    // Mark slot as full
                    inventory.isFull[i] = true;

                    // Destroy world pickup item
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    }

