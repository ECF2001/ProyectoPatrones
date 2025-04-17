using UnityEngine;

public class Slot : MonoBehaviour
{
    private Invetory inventory;
    public int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Invetory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
       
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            Destroy(child.gameObject);
        }
    }
}
