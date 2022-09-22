using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Start()
    {
        building = transform.parent.GetComponent<Building>();
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCosts)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * .5f));
            }

            Destroy(building.gameObject);
        });
    }
}