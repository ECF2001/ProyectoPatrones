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
                    // Fix: instantiate and parent correctly
                    GameObject buttonInstance = Instantiate(itemButton);
                    buttonInstance.transform.SetParent(slotTransform, false); // <--- THIS IS CRUCIAL

                    inventory.isFull[i] = true;
                    Destroy(gameObject); // remove world pickup
                    break;
                }
            }
        }
    }
}
