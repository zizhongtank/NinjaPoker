using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deck Deal Shuffle
public class Deck
{
    private static Deck instance;

    private List<Card> library;

    private CharacterType ctype;

    //judge whether the deck is empty
    public static Deck Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Deck();
            }
            return instance;
        }
    }

    //get the card number in the deck
    public int CardCount
    {
        get { return library.Count; }
    }

    //indexes
    public Card this[int index]
    {
        get
        {
            return library[index];
        }
    }

    private Deck()
    {
        library = new List<Card>();

        ctype = CharacterType.Library;

        CreateDeck();
    }

    //create a deck
    void CreateDeck()
    {
        //CREATE NUMBER CARD 
        for (int color = 0; color<3; color++)
        {
            for (int value = 0; value < 10; value++)
            {
                Weight w = (Weight)value;

                Suits s = (Suits)color;

                string name =s.ToString()+ w.ToString();

                Card card = new Card(name, w, s, ctype);

                library.Add(card);
            }
        }

        //create skill card

        Card blade1 = new Card("Blade", Weight.Blade, Suits.None, ctype);

        Card blade2 = new Card("Blade", Weight.Blade, Suits.None, ctype);

        Card recover1 = new Card("Recover", Weight.Recover, Suits.None, ctype);

        Card recover2 = new Card("Recover", Weight.Recover, Suits.None, ctype);

        Card mirror1 = new Card("Mirror", Weight.Millor, Suits.None, ctype);

        Card mirror2 = new Card("Mirror", Weight.Millor, Suits.None, ctype);

        library.Add(blade1);

        library.Add(blade2);

        library.Add(recover1);

        library.Add(recover2);

        library.Add(mirror1);

        library.Add(mirror2);

    }

    //SHUFFLE

    public void Shuffle()
    {
        if (CardCount == 36)
        {
            System.Random random = new System.Random();

            List<Card> newList = new List<Card>();

            foreach(Card item in library)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }

            library.Clear();

            foreach (Card item in newList)
            {
                library.Add(item);
            }

            newList.Clear();
        }
    }

    //Deal
    public Card Deal()
    {
        Card ret = library[library.Count - 1];//Each card is dealt reduced by one card from the library.

        library.Remove(ret);//remove the card from the deck after deal

        return ret;
    }

    public void AddCard(Card card)
    {
        card.Attribution = ctype;// Set the owner of the card

        library.Add(card);
    }
}

