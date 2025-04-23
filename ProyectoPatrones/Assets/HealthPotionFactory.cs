using UnityEngine;

public class HealthPotionFactory : WorldItemFactory
{
    public override GameObject CreateItem(Vector2 position)
    {
        GameObject prefab = Resources.Load<GameObject>("Health Potion PickUp");
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
