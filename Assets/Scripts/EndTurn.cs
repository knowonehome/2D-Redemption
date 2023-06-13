using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    public GameObject Card;
    public GameObject HandArea;

    public void OnClick()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(HandArea.transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
