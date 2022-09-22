using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private TextMeshPro text;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        text = transform.Find("text").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position - transform.localPosition);
        float percent = Mathf.Round((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        text.SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}