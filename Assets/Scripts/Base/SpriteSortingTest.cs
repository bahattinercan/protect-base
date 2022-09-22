using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingTest : MonoBehaviour
{
    private float precisionMultiplier = 10f;
    [SerializeField] private bool runOnce=true;
    int index = 0;

    float positionOffsetY;
    private void Start()
    {

        foreach (Transform childGameObject in this.transform)
        {
            index++;

            if (childGameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer childSprite))
            {

                if (childGameObject.localPosition.y < 0f)
                {
                    positionOffsetY = -.1f;
                }
                else if (childGameObject.localPosition.y > 0f)
                {
                    positionOffsetY = -(childGameObject.localPosition.y + .1f);
                }
                else if (childGameObject.localPosition.y == 0f)
                {
                    positionOffsetY = 0f;
                }


                childSprite.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);

            }
        }

    }


    private void Update()
    {
        if (runOnce && index == this.transform.childCount) Destroy(this);
    }
}
