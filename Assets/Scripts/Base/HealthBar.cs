using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform separatorContainer;

    private void Awake()
    {
        healthSystem = transform.parent.GetComponent<HealthSystem>();
        barTransform = transform.Find("bar");
    }

    private void Start()
    {
        separatorContainer = transform.Find("separatorContainer");
        ConstructHealthBarSeparator();

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnMaxHealthChanged += HealthSystem_OnMaxHealthChanged;
        UpdateBar();
        Update_HealthBarVisible();
    }

    private void HealthSystem_OnMaxHealthChanged(object sender, System.EventArgs e)
    {
        ConstructHealthBarSeparator();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        Update_HealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        Update_HealthBarVisible();
    }

    private void ConstructHealthBarSeparator()
    {
        Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorT in separatorContainer)
        {
            if (separatorT == separatorTemplate)
                continue;
            Destroy(separatorT.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = transform.Find("barBackground").localScale.x;
        float barOneHealthAmountSize = barSize / healthSystem.HealthMax;
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.HealthMax / healthAmountPerSeparator);

        for (int i = 0; i < healthSeparatorCount; i++)
        {
            Transform separatorT = Instantiate(separatorTemplate, separatorContainer);
            separatorT.gameObject.SetActive(true);
            separatorT.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator + separatorTemplate.localScale.x / 2, 0, 0);
        }
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.Get_HealthAmountNormalized(), 1, 1);
    }

    private void Update_HealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}