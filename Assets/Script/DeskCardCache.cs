using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskCardCache
{

    private static DeskCardCache instance;

    private List<Card> library;

    private CharacterType ctype;

    private CardsType rule;

    public void Init()
    {

    }
	
    public static DeskCardCache Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DeskCardCache();
            }

            return instance;

        }
    }

    public CardsType Rule
    {
        set { rule = value; }

        get { return rule; }
    }

    //创建索引器
    public Card this[int index]
    {
        get
        {
            return library[index];
        }
    }
    //get the amount of the deck
    public int CardsCount
    {
        get { return library.Count; }
    }

    //get the min weight
    public int MinWeight
    {
        get { return (int)library[0].GetCardWeight; }
    }

    //get the max weight


   
    private DeskCardCache()
    {
        library = new List<Card>();

        ctype = CharacterType.Desk;

        rule = CardsType.None;
    }

    //deal
    public Card Deal()
    {
        Card ret = library[library.Count - 1];//erver time deal, deck card-1

        library.Remove(ret);

        return ret;
    }

    //add card to deck
    public void AddCard(Card card)
    {
        card.Attribution = ctype;

        library.Add(card);
    }

    //clear the desk
   public void Clear()
    {
        if(library.Count !=0)
        {
            CardSprite[] cardSprites = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();

            for (int i=0; i< cardSprites.Length; i++)
            {
                cardSprites[i].transform.parent = null;

                cardSprites[i].Destroy();
            }

            while (library.Count != 0)
            {
                Card card = library[library.Count - 1];

                library.Remove(card);

                Deck.Instance.AddCard(card);
            }

            rule = CardsType.None;
        }
    }
    
    //Sort
    public void Sort()
    {
        CardRules.SortCards(library, true);
    }
}
