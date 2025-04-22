using UnityEngine;
using UnityEngine.UI;

public class Conector : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        Debug.Log(button);
    }
    private void OnClick()
    {
        Debug.Log("Clicked on the object");
        Transform slot1 = transform.Find("slot1");
        slot1.GetComponent<Slot>().DropItem();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
