using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void CardEvent(bool arg);

public class playController{

    private CharacterType bigger;

    private CharacterType currentplayer;

    private static playController instance;

    public event CardEvent SmartCard;// computer

    public event CardEvent ActiveButton;// 

    public static playController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new playController();
            }

            return instance;
        }
    }

    //get the current player
    public CharacterType Type
    {
        get { return currentplayer; }
    }

    //get the bigger player

    public CharacterType Bigger
    {
        set { bigger = value; }
        get { return bigger; }
    }

    private playController()
    {
        currentplayer = CharacterType.Desk;
    }

    public void Init(CharacterType type)
    {
        currentplayer = type;

        Bigger = type;

        if(currentplayer == CharacterType.Player)
        {
            ActiveButton(false);
        }
        else
        {
            SmartCard(true);
        }

    }

    public void Turn()
    {
        currentplayer += 1;

        if(currentplayer == CharacterType.Desk)
        {
            currentplayer = CharacterType.Player;
        }

        if(currentplayer == CharacterType.Computer)
        {
            SmartCard?.Invoke(bigger == currentplayer);

        }
        else if (currentplayer == CharacterType.Player)
        {
            ActiveButton(bigger != currentplayer);
        }
    }

    public  void ResetButton()
    {
        ActiveButton = null;
    }

    public void RestSmartCard()
    {
        SmartCard = null;
    }
}
