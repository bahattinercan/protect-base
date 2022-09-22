using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    private ResourceGenerator generator;
    private Transform barTransform;

    private void Start()
    {
        generator = transform.parent.GetComponent<ResourceGenerator>();
        ResourceGeneratorData data = generator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = data.resourceType.sprite;        
        transform.Find("text").GetComponent<TextMeshPro>().SetText(generator.GetGeneratedAmountPerSecond().ToString("F1"));
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1-generator.GetTimerNormalized(), 1, 1);
    }
}
