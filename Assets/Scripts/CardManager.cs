using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject Card;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private List<CardData> deck;

    private List<GameObject> activeCards = new List<GameObject>();

    private void Start()
    {
        // Shuffle the deck if needed
        ShuffleDeck();

        // Load the initial cards
        LoadInitialCards(5);
    }

    private void ShuffleDeck()
    {
        // Shuffle the deck using a shuffling algorithm
        // (e.g., Fisher-Yates shuffle)
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            CardData temp = deck[randomIndex];
            deck[randomIndex] = deck[i];
            deck[i] = temp;
        }
    }

    private void LoadInitialCards(int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            if (deck.Count > 0)
            {
                CardData cardData = deck[0];
                deck.RemoveAt(0);

                GameObject card = InstantiateCard(cardData);
                activeCards.Add(card);
            }
        }
    }

    private GameObject InstantiateCard(CardData cardData)
    {
        GameObject card = Instantiate(Card, cardSpawnPoint);
        CardComponent cardComponent = card.GetComponent<CardComponent>();
        cardComponent.SetCardData(cardData);

        // Add any additional setup or customization for the instantiated card

        return card;
    }

    // Call this method when a player draws a card
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            CardData cardData = deck[0];
            deck.RemoveAt(0);

            GameObject card = InstantiateCard(cardData);
            activeCards.Add(card);
        }
    }

    // Call this method when a card is no longer needed and should be removed
    public void RemoveCard(GameObject card)
    {
        activeCards.Remove(card);
        Destroy(card);
    }
}