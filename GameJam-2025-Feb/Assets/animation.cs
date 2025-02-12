using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{

    public Vector3 lastPosition;
    public SpriteRenderer sprite;
    public Sprite Up;
    public Sprite Down;
    public Sprite Right;
    public Sprite Left;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>(); 
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationDirection();
    }
    void UpdateAnimationDirection()
    {
        Vector3 movementDirection = transform.position - lastPosition; // Richting bepalen

        if (movementDirection.magnitude > 0.01f) // Beweegt de ghost?
        {
            if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y))
            {
                // Horizontale beweging
                if (movementDirection.x > 0)
                {
                    sprite.sprite = Right;
                }
                else
                {
                    sprite.sprite = Left;
                }
            }
            else
            {
                // Verticale beweging
                if (movementDirection.y > 0)
                {
                   sprite.sprite = Up;
                }
                else
                {
                    sprite.sprite = Down;
                }
            }
        }
        lastPosition = transform.position; // Update de laatste positie
    }

}
