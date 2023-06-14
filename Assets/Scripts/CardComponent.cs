using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{
    public string CardName { get; private set; }
    public string CardType { get; private set; }
    public string CardBrigade { get; private set; }
    public string CardStrength { get; private set; }
    public string CardToughness { get; private set; }
    public string CardClass { get; private set; }
    public string CardIdentifier { get; private set; }

    public void SetCardData(string name, string type, string brigade, string strength, string toughness, string cardClass, string identifier)
    {
        CardName = name;
        CardType = type;
        CardBrigade = brigade;
        CardStrength = strength;
        CardToughness = toughness;
        CardClass = cardClass;
        CardIdentifier = identifier;
    }

    // Add any additional methods or functionality related to the card behavior or interactions here
}
