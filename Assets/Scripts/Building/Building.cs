using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        buildingDemolishButton = transform.Find("buildingDemolishButtonP");
        buildingRepairButton = transform.Find("buildingRepairButtonP");

        healthSystem.SetHealthMax(buildingType.healthMax);
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

        HideDemolishBtn();
        HideRepairBtn();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHealth())
        {
            HideRepairBtn();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        ShowRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
        CinemachineShake.Instance.ShakeCamera(3f, .05f);
        ChromaticAberrationEffect.Instance.SetWeight(.25f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CinemachineShake.Instance.ShakeCamera(6f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
        Instantiate(Resources.Load<Transform>("buildingDestroyedParticleP"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowDemolishBtn();
    }

    private void OnMouseExit()
    {
        HideDemolishBtn();
    }

    private void ShowDemolishBtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    private void HideDemolishBtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }

    private void ShowRepairBtn()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }

    private void HideRepairBtn()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}