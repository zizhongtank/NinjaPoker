using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private GameManager gm;

    private void Start()
    {
        transform.Find("Normal").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(StartNormalGame));

        gm = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    public void StartNormalGame()
    {

        Destroy(this.gameObject);//xiaohuichangjing

        gm.InitInteraction();//初始化面板

        gm.InitScene();//初始化游戏场景数据

        
    }

}
