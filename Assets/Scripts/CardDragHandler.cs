using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragHandler : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 initialPosition;
    private Transform parentToReturnTo;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        // Check if the card can be dragged (e.g., player's turn, card is playable, etc.)
        if (CanDragCard())
        {
            isDragging = true;
            initialPosition = transform.position;
            parentToReturnTo = transform.parent;
            transform.SetParent(transform.parent.parent); // Move card out of hand to avoid blocking other cards
            spriteRenderer.sortingOrder = 1; // Bring the card to the front while dragging
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            // Perform action if the card is dropped in a valid play area
            if (IsDropValid())
            {
                // Perform the action associated with dropping the card
                PerformCardAction();
            }
            else
            {
                // Return the card to its initial position
                transform.position = initialPosition;
                transform.SetParent(parentToReturnTo);
            }

            spriteRenderer.sortingOrder = 0; // Reset the card's sorting order
            isDragging = false;
        }
    }

    private bool CanDragCard()
    {
        // Add your custom conditions for determining if the card can be dragged (e.g., player's turn, card is playable, etc.)
        return true;
    }

    private bool IsDropValid()
    {
        // Add your custom conditions for determining if the drop is valid (e.g., checking play area rules)
        return true;
    }

    private void PerformCardAction()
    {
        // Add the action associated with dropping the card (e.g., card effect, updating game state, etc.)
        Debug.Log("Card dropped!");
    }
}