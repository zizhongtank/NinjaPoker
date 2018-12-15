using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private readonly string cardName;

    private readonly Weight weight;

    private readonly Suits color;

    private CharacterType belongTo;

    private bool makedSprite;


    public Card(string name, Weight weight, Suits color, CharacterType belongTo)
    {
        makedSprite = false;

        cardName = name;

        this.weight = weight;

        this.color = color;

        this.belongTo = belongTo;

    }

    public string GetCardName
    {
        get { return cardName; }
    }

    public Weight GetCardWeight
    {
        get { return weight; }
    }

    public Suits GetCardSuit
    {
        get { return color; }
    }

    public bool isSprite
    {
        set { makedSprite = value; }
        get { return makedSprite; }
    }  

    //牌的归属
    public CharacterType Attribution
    {
        set { belongTo = value; }

        get { return belongTo; }
    }
}

