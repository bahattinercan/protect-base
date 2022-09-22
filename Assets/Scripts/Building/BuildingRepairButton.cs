using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Start()
    {
        healthSystem = transform.parent.GetComponent<HealthSystem>();
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.HealthMax - healthSystem.Health;
            int repairCost = missingHealth / 2;
            ResourceAmount resourceAmount = new ResourceAmount
            {
                resourceType = goldResourceType,
                amount = repairCost
            };

            if (ResourceManager.Instance.CanAfford(resourceAmount))
            {
                ResourceManager.Instance.SpendResources(resourceAmount);
                healthSystem.HealFull();
            }
            else
            {
                TooltipUI.instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }
}