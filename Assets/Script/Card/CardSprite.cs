using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprite : MonoBehaviour {

    // Use to display the image of the card

    private Card card;

    public UISprite speite;//get NGUI UISprite

    private bool isSelected;// whether the card is selected
    
	void Start () {
		
	}
	
    //sprite card
	public Card Poker
    {
        set
        {
            card = value;
            card.isSprite = true;
            SetSprite();
        }

        get { return card; }

    }

    //is card selected?
    public bool Select
    {
        set { isSelected = value; }

        get { return isSelected; }
    }

    //set the display of UI

    void SetSprite()
    {
        if (card.Attribution == CharacterType.Player || card.Attribution == CharacterType.Desk)
        {
            speite.spriteName = card.GetCardName;
        }
        else
        {
            speite.spriteName = "Poker_58";
        }
    }

    //destroy the card销毁卡牌
    public void Destroy()
    {
        card.isSprite = false;

        Destroy(this.gameObject);
    }

    //卡牌点击事件
    public void Onclick()
    {
        if (card.Attribution == CharacterType.Player)
        {
            if (isSelected)
            {
                transform.localPosition -= Vector3.up * 10;
                isSelected = false;
            }
            else
            {
                transform.localPosition += Vector3.up * 10;
                isSelected = true;
            }
        }
    }

    //adjust the card position 调整位置

    public void GotoPosition(GameObject parent, int index)
    {
        speite.depth = index;

        if(card.Attribution == CharacterType.Player)
        {
            transform.localPosition = parent.transform.Find("CardStartPoint").localPosition + Vector3.right * 25 * index;

            if(isSelected)
            {
                transform.localPosition += Vector3.up * 10;
            }
        }
        else if (card.Attribution == CharacterType.Computer)
        {
            transform.localPosition = parent.transform.Find("CardStartPoint").localPosition + Vector3.right * 25 * index;
        }
        else if (card.Attribution == CharacterType.Desk)
        {
            transform.localPosition = parent.transform.Find("PlacePoint").localPosition;
        }
    }
}


