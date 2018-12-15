using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//读取流文件
using System.Xml.Serialization;//序列化数据

public class GameManager : MonoBehaviour {

    public int basepoints;

    private void Start()
    {
        InitMenu();
    }


    //init the NGUI panel
    public void InitMenu()
    {
        GameObject panel = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("StartPanel"));

        panel.AddComponent<Menu>();

        //panel.transform.Find("NoticeLabel").gameObject.SetActive(false);
    }

    //初始化交互面板
    public void InitInteraction()
    {
        GameObject interaction = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("InteractionPanel"));

        interaction.name = interaction.name.Replace("Clone", "");

        interaction.AddComponent<Interactive>();
    }

    //初始化游戏场景
    public void InitScene()
    {
        string fileName = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            fileName = Application.persistentDataPath;
        }
        else
        {
            fileName = Application.dataPath;
        }

        FileInfo info = new FileInfo(fileName + @"\data.json");

        GameObject scene = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("ScencePanel"));

        scene.name = scene.name.Replace("(Clone)", "");

        GameObject player = scene.transform.Find("Player").gameObject;

        HandCards playerCards = player.AddComponent<HandCards>();//给玩家添加手牌

        playerCards.cType = CharacterType.Player;//类型为玩家

        player.AddComponent<PlayCard>();//为玩家添加出牌的脚本


        GameObject computer = scene.transform.Find("Computer").gameObject;//添加电脑玩家

        HandCards computerCards = computer.AddComponent<HandCards>();//为电脑玩家添加手牌

        computerCards.cType = CharacterType.Computer;//设置类型为电脑玩家

        computer.AddComponent<SimpleSmartCard>();//为电脑玩家添加延迟出牌脚本

        computer.transform.Find("ComputerNotice").gameObject.SetActive(false);


        //desk

        GameObject desk = scene.transform.Find("Desk").gameObject;//查找桌面

        desk.transform.Find("NoticeLable").gameObject.SetActive(false);

        

        if(!info.Exists)
        {
            playerCards.Integration = 1000;
            computerCards.Integration = 1000;

        }
        else
        {
            GameData data = GetDataWit(fileName);

            playerCards.Integration = data.playerIntergaration;

            computerCards.Integration = data.computerIntergaration;
            
        }

        GameManager.UpdateIntegration(CharacterType.Player);//更新玩家数据

        GameManager.UpdateIntegration(CharacterType.Computer);//更新电脑数据
        
    }

    //shuffle
    public void DealCards()
    {
        //shuffle

        Deck.Instance.Shuffle();

        //Deal
        CharacterType currentCharacter = CharacterType.Player;

        for (int i=0; i<20; i++)
        {
            if( currentCharacter == CharacterType.Desk)
            {
                currentCharacter = CharacterType.Player;
            }
            DealTo(currentCharacter);

            currentCharacter++;

        }

        
        for (int i=0; i<1; i++)
        {
            DealTo(CharacterType.Desk);
        }
        

        for(int i = 1; i< 4; i++)
        {
            MakeHandCardsSprite((CharacterType)i, false);
        }
        
    }

    //Deal
    public void DealTo(CharacterType person)
    {
        if( person == CharacterType.Desk)
        {
            Card moveCard = Deck.Instance.Deal();

            DeskCardCache.Instance.AddCard(moveCard);
        }

        else
        {
            GameObject palyerobj = GameObject.Find(person.ToString());

            HandCards cards = palyerobj.GetComponent<HandCards>();

            Card moveCard = Deck.Instance.Deal();

            cards.AddCard(moveCard);
        }
    }

    //使卡片精灵化
    public void MakeSprite(CharacterType type, Card card, bool selected)
    {
        if(!card.isSprite)
        {
            GameObject obj = Resources.Load("poker") as GameObject;

            GameObject poker = NGUITools.AddChild(GameObject.Find(type.ToString()), obj);

            CardSprite sprite = poker.gameObject.GetComponent<CardSprite>();

            sprite.Poker = card;

            sprite.Select = selected;
        }
    }

    //精灵化Poke
    public void MakeHandCardsSprite(CharacterType type, bool isSelected)
    {
        if (type == CharacterType.Desk)
        {
            DeskCardCache instance = DeskCardCache.Instance;

            for (int i=0; i< instance.CardsCount; i++)
            {
                MakeSprite(type, instance[i], isSelected);
            }
        }
        else
        {
            GameObject parentObj = GameObject.Find(type.ToString());

            HandCards cards = parentObj.GetComponent<HandCards>();

            //排序

            cards.Sort();

            //精灵化

        for( int i=0; i< cards.CardsCount; i++)
            {
                if (!cards[i].isSprite)
                {
                    MakeSprite(type, cards[i], isSelected);
                }
            }
            //显示剩余卡牌

            UpdateLeftCardsCount(cards.cType, cards.CardsCount);
        }
        //调整精灵位置

        AdjustCardSpritePosition(type);
    }

    //更新积分显示
    public static void UpdateIntegration(CharacterType type)
    {
        int integration = GameObject.Find(type.ToString()).GetComponent<HandCards>().Integration;

        GameObject obj = GameObject.Find(type.ToString()).transform.Find("IntegrationLable").gameObject;

        obj.GetComponent<UILabel>().text = "Point:" + integration;

    }

    GameData GetDataWit(string fileName)
    {
        GameData data = new GameData();

        Stream stream = new FileStream(fileName + @"\data.json", FileMode.Open, FileAccess.Read, FileShare.None);

        StreamReader streamreader = new StreamReader(stream, true);

        XmlSerializer xmlserializer = new XmlSerializer(data.GetType());

        data = xmlserializer.Deserialize(streamreader) as GameData;

        streamreader.Close();

        stream.Close();

        return data;
    }

    //销毁所有精灵
    public void DestroyAllSprite()
    {

        //数组的下标都从0开始
        CardSprite[][] cardSprite = new CardSprite[2][];

        cardSprite[0] = GameObject.Find("Player").GetComponentsInChildren<CardSprite>();

        cardSprite[1] = GameObject.Find("Computer").GetComponentsInChildren<CardSprite>();

        for (int i=0; i< cardSprite.GetLength(0); i++)
        {
            for (int j=0; j< cardSprite[i].Length; j++)
            {
                cardSprite[i][j].Destroy();
            }
        }
    }

    public static void DisplayOverInfo(bool enough, GameObject gameoverPanel, Indentity Winner)
    {
        if (enough)
        {
            gameoverPanel.transform.Find("Button").gameObject.SetActive(false);

            gameoverPanel.GetComponent<Restart>().SetTimeToNext(3.0f);

        }
        if(Winner == Indentity.KAGA)
        {
            gameoverPanel.GetComponentInChildren<UISprite>().spriteName = "rolel";

            gameoverPanel.GetComponentInChildren<UILabel>().text = "KAGA NINJA WIN";

        }
        else
        {
            gameoverPanel.GetComponentInChildren<UISprite>().spriteName = "rolel";

            gameoverPanel.GetComponentInChildren<UILabel>().text = "IGA NINJA WIN";
        }
    }

    //电脑玩家离开显示
    public static void DisplayDeskNotice(bool show)
    {
        GameObject.Find("Desk").transform.Find("NoticeLable").gameObject.SetActive(show);

    }

    //更新身份
    public static void UpdateIndentity(CharacterType type, Indentity identity)
    {
        GameObject obj = GameObject.Find(type.ToString()).transform.Find("Identity").gameObject;

        //改变属性

        GameObject.Find(type.ToString()).GetComponent<HandCards>().AccessIdentity = identity;

        //改变显示

        obj.GetComponent<UISprite>().spriteName = "Identity" + identity.ToString();

    }

    //更新扑克牌显示张数
    public static void UpdateLeftCardsCount(CharacterType type, int cardsCount)
    {
        GameObject obj = GameObject.Find(type.ToString()).transform.Find("LeftPoker").gameObject;

        obj.GetComponent<UILabel>().text = "Left Cards" + cardsCount;
    }

    //获取指定数组的权值
    public static int GetWeight(Card[] cards, CardsType rule)
    {
        int totalWeight = 0;

        for (int i = 0; i < cards.Length; i++)
        {
            totalWeight += (int)cards[i].GetCardWeight;

        }
        return totalWeight;
    }

    //调整手牌位置
    public static void AdjustCardSpritePosition(CharacterType type)
    {
        if (type == CharacterType.Desk)
        {
            DeskCardCache instance = DeskCardCache.Instance;

            CardSprite[] cs = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();

            for (int i=0; i< cs.Length; i++)
            {
                for (int j=0; j <cs.Length; j++)
                {
                    if(cs[j].Poker == instance[i])
                    {
                        cs[j].GotoPosition(GameObject.Find(type.ToString()), i);
                    }
                }
            }
        }
        else
        {
            HandCards hc = GameObject.Find(type.ToString()).GetComponent<HandCards>();

            CardSprite[] cs = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();

            for (int i =0; i<hc.CardsCount; i++)
            {
                for (int j =0; j<cs.Length; j++)
                {
                    if ( cs[j].Poker == hc[i])
                    {
                        cs[j].GotoPosition(GameObject.Find(type.ToString()), i);
                    }
                }
            }
        }
    }

    //把所有扑克牌放到牌库中
    public void BackTodeck()
    {
        HandCards[] handCards = new HandCards[2];

        handCards[0] = GameObject.Find("Player").GetComponent<HandCards>();

        handCards[1] = GameObject.Find("Computer").GetComponent<HandCards>();

        for (int i=0; i > handCards.Length;i++)
        {
            while (handCards[i].CardsCount !=0)
            {
                Card last = handCards[i][handCards[i].CardsCount - 1];

                Deck.Instance.AddCard(last);

                handCards[i].PopCard(last);

            }
        }
    }

    //发底牌
    public void CardsOnTable(CharacterType type)
    {
        GameObject parentObj = GameObject.Find(type.ToString());

        HandCards cards = parentObj.GetComponent<HandCards>();

        //销毁底牌精灵

        CardSprite[] sprites = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].Destroy();
        }

        while (DeskCardCache.Instance.CardsCount != 0)
        {
            Card card = DeskCardCache.Instance.Deal();

            cards.AddCard(card);
        }

        MakeHandCardsSprite(type, true);
     }

    //更换电脑玩家
    IEnumerator changeComputer(CharacterType type)
    {
        DisplayDeskNotice(true);

        yield return new WaitForSeconds(3.0f);

        GameObject oldcomputer = GameObject.Find(type.ToString());

        Destroy(oldcomputer);

        MatchNewComputer(type);

    }

    //匹配新的玩家

    public void MatchNewComputer(CharacterType type)
    {
        BackTodeck();

        DestroyAllSprite();

        DeskCardCache.Instance.Clear();

        DisplayDeskNotice(false);

        GameObject newComputer = NGUITools.AddChild(GameObject.Find("ScenePanel"), (GameObject)Resources.Load(type.ToString()));

        newComputer.name = newComputer.name.Replace("(Clone)", "");

        newComputer.AddMissingComponent<HandCards>().cType = type;//设置类型

        newComputer.AddMissingComponent<HandCards>().Integration = 1000;//设置玩家积分

        newComputer.transform.Find("ComputerNotice").gameObject.SetActive(false);

        newComputer.AddComponent<SimpleSmartCard>();//添加自动出牌的AI脚本

        if (Random.Range(1,3) == 1)
        {
            newComputer.transform.Find("HeadPortrat").gameObject.GetComponent<UISprite>().spriteName = "rolel";
        }
        else
        {
            newComputer.transform.Find("HeadPortrat").gameObject.GetComponent<UISprite>().spriteName = "rolel";
        }
    }

    //存储数据
    public void SaveDataWitUtf8(GameData data)
    {
        string fileName = "";

        if (Application.platform == RuntimePlatform.Android)
        { 
            fileName = Application.persistentDataPath;
        }
        else
        {
            fileName = Application.dataPath;
        }

        Stream stream = new FileStream(fileName + @"\data.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

        StreamWriter writter = new StreamWriter(stream, System.Text.Encoding.UTF8);

        XmlSerializer xmlserializer = new XmlSerializer(data.GetType());

        writter.Close();

        stream.Close();
    }

    //统计积分
    public void StatisticalIntegral(CharacterType type, Indentity winner)
    {
        HandCards cards = GameObject.Find(type.ToString()).GetComponent<HandCards>();

        int integration = basepoints * (int)(cards.AccessIdentity + 1);

        if(cards.AccessIdentity != winner)
        {
            integration = -integration;//如果失败扣分
        }
        else
        {
            cards.Integration = cards.Integration + integration;
        }

        //更新积分界面
        UpdateIntegration(type);

    }

    public void GameOver()
    {
        GameObject currentCharactor = GameObject.Find(playController.Instance.Type.ToString());

        Indentity identity = currentCharactor.GetComponent<HandCards>().AccessIdentity;

        //count points

        for (int i = 1; i< 3; i++)
        {
            StatisticalIntegral((CharacterType)i, identity);
        }

        GameObject gameover = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("GameOverPanel"));

        gameover.AddComponent<Restart>();

        int playerIntegration = GameObject.Find(CharacterType.Player.ToString()).GetComponent<HandCards>().Integration;

        int computerIntegration = GameObject.Find(CharacterType.Computer.ToString()).GetComponent<HandCards>().Integration;

        //待完成
        if(playerIntegration>0 )
        {
            gameover.GetComponent<Restart>().SetTimeToNext(3.0f);

            if(computerIntegration <=0)
            {
                StartCoroutine(changeComputer(CharacterType.Computer));
            }

            DisplayOverInfo(true, gameover, identity);

            //store the gamedata

            GameData data = new GameData
            {
                playerIntergaration = playerIntegration > 0 ? playerIntegration : 1000,
                computerIntergaration = computerIntegration > 0 ? computerIntegration : 1000
            };

            SaveDataWitUtf8(data);
        }
        else
        {
            DisplayOverInfo(false, gameover, identity);

            GameData data = new GameData
            {
                playerIntergaration = 1000,

                computerIntergaration = 1000
            };

            SaveDataWitUtf8(data);
        }
    }

    
   

}
