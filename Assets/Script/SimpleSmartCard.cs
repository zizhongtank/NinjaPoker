using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;


public class SimpleSmartCard : MonoBehaviour
{
    private GameManager gm;

    private CardSprite cardsprite;

    playController pc = playController.Instance;

    //players play
    private void Update()
    {
        if(pc.Type== CharacterType.Computer)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                List<GameObject> childgo;
                childgo = new List<GameObject>();
                var hc = GameObject.Find("Computer").GetComponent<HandCards>();
                foreach(Transform ts in hc.transform)
                {
                    childgo.Add(ts.gameObject);

                }
                childgo[childgo.Count-1].transform.parent= GameObject.Find("Desk").transform;
                DeskCardCache.Instance.AddCard(childgo[childgo.Count - 1].GetComponent<CardSprite>().Poker);
                childgo[childgo.Count - 1].GetComponent<CardSprite>().GotoPosition(childgo[childgo.Count - 1].transform.parent.gameObject, 0);
                print("aaaaa");

                pc.Turn();
            }

        }




    }
}