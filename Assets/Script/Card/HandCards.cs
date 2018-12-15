using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour {

    public CharacterType cType;

    private List<Card> library;//定义扑克牌数组

    private Indentity identity;

    private int integration;//积分

    private void Start()
    {
        library = new List<Card>();

        identity = Indentity.KAGA;
    }

    //积分
    public int Integration
    {
        set { integration = value; }
        get { return integration; }
    }

    //get the amount of the hand cards
    public int CardsCount
    {
        get { return library.Count; }
    }

    //访问身份
    public Indentity AccessIdentity
    {
        set
        {
            identity = value;
        }

        get { return identity; }
    }

    //get the hand card
    public Card this[int index]
    {
        get { return library[index]; }
    }

    //获取值的索引
    public int this[Card card]
    {
        get { return library.IndexOf(card); }
    }

    //add hand card
    public void AddCard (Card card)
    {
        card.Attribution = cType;
        library.Add(card);
    }

    //deal
    public void PopCard(Card card)
    {
        library.Remove(card);
    }

    //Sort
    public void Sort()
    {
        CardRules.SortCards(library, false);
    }
}
