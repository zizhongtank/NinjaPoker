using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//restart the game
public class Restart : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        //get the gamemanager when we start the game
        gm = GameObject.Find("GameController").GetComponent<GameManager>();

        //get the button
        GameObject clickButton = transform.Find("Button").gameObject;

        //add an event to button
        clickButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(RestartGame));

    }

    //面板定时消失

    public void SetTimeToNext(float sec)
    {
        Invoke("Next", sec);
    }

    //restart the game
    public void RestartGame()
    {
        //clear all
        gm.BackTodeck();

        gm.DestroyAllSprite();

        DeskCardCache.Instance.Clear();

        Destroy(GameObject.Find("InteractionPanel").gameObject);

        Destroy(GameObject.Find("SencePanel").gameObject);

        Destroy(this.gameObject);

        //reset event

        playController.Instance.ResetButton();

        playController.Instance.RestSmartCard();

        GameObject panel = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("StartPanel"));

        panel.AddComponent<Menu>();

        panel.transform.Find("NoticeLable").gameObject.SetActive(true);
    }

    //start next game
    public void Next()
    {
        gm.BackTodeck();

        gm.DestroyAllSprite();

        DeskCardCache.Instance.Clear();

        GameObject deal = GameObject.Find("InteractionPanel").transform.Find("DealBtn").gameObject;

        deal.SetActive(true);

        Destroy(this.gameObject);

        ResetDisplay();
    }

    //reset the player's display

    public void ResetDisplay()
    {
        for (int i=1; i<3; i++)
        {
            if (GameObject.Find(((CharacterType)i).ToString()))
            {
                GameManager.UpdateLeftCardsCount((CharacterType)i, 0);

                GameManager.UpdateIndentity((CharacterType)i, Indentity.KAGA);
            }
        }
    }

}
