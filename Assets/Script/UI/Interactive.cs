using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//待完成
public class Interactive : MonoBehaviour {

    //interactive Panel

    private GameObject deal;//DEAL

    private GameObject play;//PLAY

    private GameObject grab;

    private GameObject disgrab;

    private GameManager gm; 

    private void Start()
    {
        deal = gameObject.transform.Find("DealBtn").gameObject;//get deal button

        play = gameObject.transform.Find("PlayBtn").gameObject;//get play button

        grab = gameObject.transform.Find("GrabBtn").gameObject;

        disgrab = gameObject.transform.Find("DisgrabBtn").gameObject;

        gm = GameObject.Find("GameController").GetComponent<GameManager>();

        //添加回调

        deal.GetComponent<UIButton>().onClick.Add(new EventDelegate(DealCallBack));

        play.GetComponent<UIButton>().onClick.Add(new EventDelegate(PlayCallBack));

        grab.GetComponent<UIButton>().onClick.Add(new EventDelegate(GrabCallBack));

        disgrab.GetComponent<UIButton>().onClick.Add(new EventDelegate(DisgrabCallBack));

        //激活出牌按钮事件绑定
        playController.Instance.ActiveButton += ActiveCardButton;

        //hide the button
        play.SetActive(false);

        grab.SetActive(false);

        disgrab.SetActive(false);

    }

    public void ActiveCardButton(bool canRject)
    {
        play.SetActive(true);
        
    }

    //deal 回调
    public void DealCallBack()
    {

        
        gm.DealCards();

        grab.SetActive(true);//先手按钮出现

        disgrab.SetActive(true);//不抢先手按钮出现

        deal.SetActive(false);//hide deal button
    }

    public void PlayCallBack()
    {
        PlayCard playCard = GameObject.Find("Player").GetComponent<PlayCard>();

        if(playCard.CheckSelectCards())
        {
            play.SetActive(false);
        }
    }

    public void GrabCallBack()
    {
        gm.CardsOnTable(CharacterType.Player);

        playController.Instance.Init(CharacterType.Player);

        grab.SetActive(false);

        disgrab.SetActive(false);
    }

    public void DisgrabCallBack()
    {
        gm.CardsOnTable(CharacterType.Computer);

        playController.Instance.Init(CharacterType.Computer);

        grab.SetActive(false);
    }
}