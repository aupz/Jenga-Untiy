using UnityEngine;

public class RemoveGlassJenga : MonoBehaviour
{
    public GameObject glassJengaPrefab;  // The prefab to be removed

    public void RemoveObjects()
    {
        GameObject[] glassJengaObjects = GameObject.FindGameObjectsWithTag(glassJengaPrefab.tag);

        foreach (GameObject obj in glassJengaObjects)
        {
            if (obj.name.Contains(glassJengaPrefab.name))
            {
                Destroy(obj);
            }
        }
    }
}