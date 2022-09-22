using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 pos, BuildingTypeSO buildingType)
    {        
        Transform prefab = Resources.Load<Transform>("buildingConstructionP");
        
        Transform objectTransform = Instantiate(prefab, pos, Quaternion.identity);

        BuildingConstruction construction = objectTransform.GetComponent<BuildingConstruction>();
        construction.BuildingType = buildingType;
        construction.Invoke("SetBuildingType", buildingType.constructionTime);
        return construction;
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - (timer / buildingType.constructionTime);
    }

    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    private float timer;
    private static SpriteRenderer spriteRenderer;
    private Material constructionMat;

    public BuildingTypeSO BuildingType { get => buildingType; set => buildingType = value; }

    private void Start()
    {
        timer = buildingType.constructionTime;
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = buildingType.sprite;
        constructionMat = spriteRenderer.material;
        GetComponent<BuildingTypeHolder>().buildingType = buildingType;
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        Instantiate(Resources.Load<Transform>("buildingPlacedParticleP"), transform.position, Quaternion.identity);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        constructionMat.SetFloat("_Progress", GetConstructionTimerNormalized());
    }

    public void SetBuildingType()
    {
        Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
        Instantiate(Resources.Load<Transform>("buildingPlacedParticleP"), transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
        Destroy(gameObject);
    }
}