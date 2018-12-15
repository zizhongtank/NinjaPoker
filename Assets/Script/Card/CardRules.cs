using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//card rules

public class CardRules{

    //卡牌数组排序
	public static void SortCards(List<Card>cards, bool ascending)
    {
        cards.Sort(
            (Card a, Card b) =>

            {
                if (!ascending)
                {
                    return -a.GetCardWeight.CompareTo(b.GetCardWeight) * 2 + a.GetCardSuit.CompareTo(b.GetCardSuit);
                }
                else
                {
                    return a.GetCardWeight.CompareTo(b.GetCardWeight);
                }
            }
            );
    }

    //判断是否符合出牌规则
    public static bool PopEnable(Card [] cards, out CardsType type)
    {
        type = CardsType.None;

        bool IsRule = false;

        switch (cards.Length)
        {
            case 1:
                IsRule = true;
                type = CardsType.Number;
                break;

        }

        return IsRule;

        
       
    }

}
