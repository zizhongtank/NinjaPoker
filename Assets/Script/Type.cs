using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Library=0,

    Player,//myself

    Computer,//AI

    Desk
}

public enum Suits
{
    Club,

    Diamond,

    Heart,

    None,
}

public enum CardsType 
{
    Number,

    Skill,

    None,

}

//the card value
public enum Weight
{
    One = 0,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Blade,
    Recover,
    Millor
}

public enum Indentity
{
    KAGA,//甲贺

    IGA,//伊贺
}

//store the type
[System. Serializable]

public class GameData
{
    public int playerIntergaration;

    public int computerIntergaration;

}
