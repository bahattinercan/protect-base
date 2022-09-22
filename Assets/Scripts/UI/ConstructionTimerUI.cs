using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    private BuildingConstruction buildingConstruction;

    private Image constructionProgressImage;

    private void Awake()
    {
        constructionProgressImage = transform.Find("mask/image").GetComponent<Image>();
    }

    private void Start()
    {
        buildingConstruction = transform.parent.GetComponent<BuildingConstruction>();
    }

    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}