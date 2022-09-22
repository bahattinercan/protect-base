using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    [SerializeField] private List<ResourceAmount> startingResourceAmounts;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    public event EventHandler OnResourceAmountChanged;

    private void Awake()
    {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        foreach (ResourceAmount resourceAmount in startingResourceAmounts)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        // use event listener
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            if (GetResourceAmount(resourceAmount.resourceType) > resourceAmount.amount)
            {
                // can afford
            }
            else
            {
                // can't afford
                return false;
            }
        }
        return true;
    }

    public bool CanAfford(ResourceAmount resourceAmount)
    {
        if (GetResourceAmount(resourceAmount.resourceType) > resourceAmount.amount)
        {
            // can afford
        }
        else
        {
            // can't afford
            return false;
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }

    public void SpendResources(ResourceAmount resourceAmount)
    {
        resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
    }
}