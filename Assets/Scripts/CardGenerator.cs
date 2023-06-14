using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Card;
    [SerializeField] private string cardDataFilePath;

    private Dictionary<string, Sprite> cardArtDictionary;

    private void Start()
    {
        // Load card art assets into a dictionary
        LoadCardArt();

        // Load and parse the card data from the spreadsheet
        List<Dictionary<string, string>> cardData = ReadCardDataFromSpreadsheet();

        // Generate card instances based on the card data
        GenerateCards(cardData);
    }

    private void LoadCardArt()
    {
        cardArtDictionary = new Dictionary<string, Sprite>();

        // Load all card art assets from a specific folder
        string artAssetFolderPath = "Assets/CardImages/";
        var artAssetFiles = Directory.GetFiles(artAssetFolderPath, "*.jpg", SearchOption.AllDirectories);

        foreach (var artAssetFile in artAssetFiles)
        {
            string artAssetKey = Path.GetFileNameWithoutExtension(artAssetFile);
            Sprite artSprite = AssetDatabase.LoadAssetAtPath<Sprite>(artAssetFile);

            if (artSprite != null)
            {
                cardArtDictionary.Add(artAssetKey, artSprite);
            }
        }
    }

    private List<Dictionary<string, string>> ReadCardDataFromSpreadsheet()
    {
        // Assuming the spreadsheet is a CSV file
        string[] csvLines = File.ReadAllLines(cardDataFilePath);

        if (csvLines.Length < 2)
        {
            Debug.LogError("Card data file is empty or doesn't contain data rows.");
            return null;
        }

        string[] header = csvLines[0].Split(',');

        List<Dictionary<string, string>> cardDataList = new List<Dictionary<string, string>>();

        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] rowValues = csvLines[i].Split(',');

            if (rowValues.Length != header.Length)
            {
                Debug.LogWarning("Invalid data row at line " + (i + 1) + " in the card data file.");
                continue;
            }

            Dictionary<string, string> cardData = new Dictionary<string, string>();

            for (int j = 0; j < header.Length; j++)
            {
                cardData.Add(header[j], rowValues[j]);
            }

            cardDataList.Add(cardData);
        }

        return cardDataList;
    }

    private void GenerateCards(List<Dictionary<string, string>> cardData)
    {
        foreach (var cardInfo in cardData)
        {
            string cardName = cardInfo["Name"];
            string imageFile = cardInfo["ImageFile"] + ".jpg";
            string cardType = cardInfo["Type"];
            string cardBrigade = cardInfo["Brigade"];
            string cardStrength = cardInfo["Strength"];
            string cardToughness = cardInfo["Toughness"];
            string cardClass = cardInfo["Class"];
            string cardIdentifier = cardInfo["Identifier"];

            // Instantiate the card prefab
            GameObject card = Instantiate(Card, transform);

            // Set card art based on the image file name
            if (cardArtDictionary.ContainsKey(imageFile))
            {
                Sprite cardArt = cardArtDictionary[imageFile];
                card.GetComponent<SpriteRenderer>().sprite = cardArt;
            }
            else
            {
                Debug.LogWarning("Card art sprite not found for image file: " + imageFile);
            }

            // Set other card properties (e.g., name, type, brigade, strength, etc.)
            // Example:
            CardComponent cardComponent = card.GetComponent<CardComponent>();
            cardComponent.SetCardData(cardName, cardType, cardBrigade, cardStrength, cardToughness, cardClass, cardIdentifier);
        }
    }
}