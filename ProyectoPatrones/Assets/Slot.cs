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
    public void OnButtonClicked()
    {
        Debug.Log("Button was clicked!");
        DropItem();
    }

    public void DropItem()
    {
        Debug.Log($"Intentando soltar {transform.childCount} objetos en el slot.");
        foreach (Transform child in transform)
        {
            Spawn spawnComponent = child.GetComponent<Spawn>();
            if (spawnComponent != null)
            {
                Debug.Log($"Soltando el objeto: {child.name}");
                spawnComponent.SpawnDroppedItem();
                Destroy(child.gameObject);
            }
            else
            {
                Debug.LogWarning($"El objeto {child.name} no tiene el componente Spawn.");
            }
        }
    }
}
