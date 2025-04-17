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
                    Instantiate(itemButton, slotTransform, false);
                    inventory.isFull[i] = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
