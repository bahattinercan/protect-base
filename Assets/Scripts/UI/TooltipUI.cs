using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundTransform;
    private TooltipTimer tooltipTimer;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundTransform = transform.Find("background").GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string toolTipNext)
    {
        textMeshPro.SetText(toolTipNext);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundTransform.sizeDelta = textSize + padding;
    }

    public void Show(string toolTipText,TooltipTimer tooltipTimer=null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(toolTipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;
    }
}
