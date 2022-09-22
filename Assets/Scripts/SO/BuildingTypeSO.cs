using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public GameObject prefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCosts;
    public int healthMax=100;
    public float constructionTime=3f;

    public string GetString_ConstructionResourceCost()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCosts)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.nameShort + resourceAmount.amount 
                + " </color>";
        }
        return str;
    }
}