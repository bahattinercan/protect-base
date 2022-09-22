using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGO;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGO = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("resourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnActiveBuildingTypeChanged;
    }

    private void OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite,e.activeBuildingType.nameString);

            if (e.activeBuildingType.hasResourceGeneratorData)
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
            else
                resourceNearbyOverlay.Hide();
        }
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPos();
    }

    private void Show(Sprite ghostSprite,string buildingName)
    {
        spriteGO.SetActive(true);
        switch (buildingName)
        {
            case "HQ":
                spriteGO.transform.localScale = Vector3.one * 13;
                break;
            case "Gold Harvester":
            case "Stone Harvester":
            case "Wood Harvester":
                spriteGO.transform.localScale = Vector3.one * 6;
                break;
            case "Normal Tower":
                spriteGO.transform.localScale = Vector3.one * 8;
                break;

            default:
                spriteGO.transform.localScale = Vector3.one;
                break;
        }
        spriteGO.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide()
    {
        spriteGO.SetActive(false);
    }
}