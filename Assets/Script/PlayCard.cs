using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//待完成

public class PlayCard : MonoBehaviour {

    private GameManager gm;
    private CardSprite cardsprite;

    //遍历选中的牌和精灵
    public bool CheckSelectCards()
    {
        CardSprite[] sprites = this.GetComponentsInChildren<CardSprite>();

        List<Card> selectedCardsList = new List<Card>();

        List<CardSprite> selectedSpriteList = new List<CardSprite>();

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].Select)
            {
                selectedSpriteList.Add(sprites[i]);

                selectedCardsList.Add(sprites[i].Poker);
            }
        }

        //sort

        CardRules.SortCards(selectedCardsList, true);
        return CheckPlayCards(selectedCardsList, selectedSpriteList);
    }

        bool CheckPlayCards (List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
        {
            //游戏管理其结合

            Card[] selectedCardsArray = selectedCardsList.ToArray();

            //检查是否符合出牌规则

            CardsType type;

            if (CardRules.PopEnable(selectedCardsArray, out type))
            {
                CardsType rule = DeskCardCache.Instance.Rule;

                if (playController.Instance.Bigger == playController.Instance.Type)
                {
                    PlayCards(selectedCardsList, selectedSpriteList, type);

                    return true;
                }
                else if (DeskCardCache.Instance.Rule == CardsType.None)
                {
                    PlayCards(selectedCardsList, selectedSpriteList, type);

                    return true;
                }
            //这里需要补充 因为我们缺少一个脚本
            
            
              
            }
            return true;
        }

        //players play
        void PlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList, CardsType type)
        {
            //在NGUI上面查找游戏对象
            HandCards player = GameObject.Find("Player").GetComponent<HandCards>();

            //如果扑克牌符合 将从手牌移动到缓冲区

            DeskCardCache.Instance.Clear();

            DeskCardCache.Instance.Rule = type;

            for (int i=0; i< selectedSpriteList.Count; i++)
            {
                //move card first
                player.PopCard(selectedSpriteList[i].Poker);

                DeskCardCache.Instance.AddCard(selectedSpriteList[i].Poker);

                selectedSpriteList[i].transform.parent = GameObject.Find("Desk").transform;
                selectedSpriteList[i].GotoPosition(selectedSpriteList[i].transform.parent.gameObject, i);
            }

        
            
        DeskCardCache.Instance.Sort();


        playController.Instance.Turn();
            //缺少游戏管理器

        }
 }

    










