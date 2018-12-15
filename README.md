# Overview

This is my thesis project called Ninja Poker, It is a 2D Card game made by Unity. In this game, all the player's actions can be done with the mouse. Also, Players must follow the rules of this card game and finally win the game.

The game was inspired by the card game Blade in the Japanese RPG game TRAILS OF THE COLD STEEL. Ninja Poker is a two-player card game, but unlike Magic or Hearthstone, the type of card is more like poker or UNO. Simply put, the player needs to win the game by comparing the points of the cards in his hand. This reminds me of the fact that some Japanese movies or comics such as Naruto have pictures of Japanese ninjas. These ninjas throw swords or bitterness at each other, just as they play cards with each other. So I set the image of both players to ninja and changed the name of the game to Ninja Poker.

For technical reasons, the current game is not a finished version, but a demo version of the demo test with obvious defects, so if you have any inconvenience, I hope everyone can understand. I will refine this project in the future and update the finished version.

# Game Mechanic

## Game Control

Ninja Poker is basically operated by the player clicking the mouse. However, there are several buttons you can use.

In the current test demo version：

<img src="{{ site.baseurl }}/Assets/UI/startBtn.png" width="200" height="100"> is start button.

<img src="{{ site.baseurl }}/Assets/UI/dealbutton.png" width="200" height="100"> is deal button.
<img src="{{ site.baseurl }}/Assets/UI/playbotton.png" width="200" height="100"> is play button.

<img src="{{ site.baseurl }}/Assets/UI/Poker_55.gif" width="80" height="80"> is go first button.
<img src="{{ site.baseurl }}/Assets/UI/Poker_54.gif" width="80" height="80"> is go last button.

and in the current test demo version, you can also press "q" button to make computer player to play the card.

## Card composition

1.	Regular card: there are 30 regular cards in the card deck, the cards’ value is from 1 to 10, and there are 3 cards each with the same value. For example, there are 3 cards with a value of 1, 3 cards with a value of 2, and so on.

2.	Skill card: there are 6 skill cards in the card deck. There are three different types of skills cards, each with two cards.

PS：In the demo version, all the Regular cards are replacing by Poker cards.

## Game objective:

Players accumulate their values by playing the cards in their hands, and ultimately the player with a greater cumulative value wins the game. For example, When the two sides have finished the cards in their respective hands, the accumulated value of me is 40, and the accumulated value of the other is 38, then I win.

## Gameplay

This game has four phases: the preparation phase, the start phase, the battle phase, and the end phase.

First, In the preparation phase, 36 cards will be shuffled in the deck. Both players will draw 10 cards from top to bottom on the deck cards as their own hand cards. 

Then, when the preparation phase is over, each player has 10 cards on hand. The game move to the start phase. Each player draws 1 card to the battle space from their own card pile and compare the value of each card. The player with the smaller value is the first to play. For example, players A and B draw a card from each card pile for comparison. The value of A's card is 3 and the value of B’s card is 5, the player A plays first.

Then, in the battle phase, when it is the turn of the player, the player must play one regular or skill card to allow the accumulated value to be greater than or equal to the value accumulated by the opponent. And then switch to the opponent’s turn. Follow the above example, if the value of player’s A’s card is 3 and the value of player’s B’s card is 5, A plays first, so A should play a regular card which value is up to 2 in his hand to add his card value to 5 or more, or play a skill card which can make his card value greater than or equal to 5. For example, A plays a regular card which value is 3, so his card accumulated value is 6, which is greater than B’s card accumulated value. Then switch to the B's turn. This loops alternately until there is a player who can't make his card's cumulative value greater than or equal to his opponent in his turn, and the game ends. As shown in the following image, I temporarily use poker as a substitute.

<img src="{{ site.baseurl }}/image%20file/111.jpg" width="300" height="240">

The card with the spade above represents the card of player B. The card with the heart below represents the card of player A. Currently, the cumulative value of Player B is 38, and the cumulative value of Player A is 31, it’s A’s turn, A should plays a card to make his total value equal or up to 38. However, there is no card in his hand can make it(because the greatest value card in his card is 6, his greatest total value should be 37), therefore, player A will lose this game. 

After determining the outcome of the game, the screen will show the result of win or lose. The game will go to the end phase, all the cards will be shuffled and the player can restart the game.

## Video demo

Here is a video demo which use the demo version of this game to introduce the gameplay and the card game rules.

[Video demo](https://youtu.be/YEghSLHw4tc)

# Designing process

## Game logic

This is the most important part of the game design. It is necessary to figure out and design reasonable game logic to be able to design the game step by step. For this card game, the player’s operation of the game must conform to the rules and procedures of the game. First, the player need to trigger an event. Simply put, press the start button and the game will enter the game interface from the start interface. After entering the game flow, the player clicks the "Deal" button, the system will assign cards to both sides of the game; next, according to the rules of the game, when in the player's turn, the player selects the card and clicks the play button to play the card. After the player has finished the card, he will turn to the computer round, when the computer player will complete the card by the set AI script. After each party has finished the card, determine the player's outcome according to the rules of the game, enter the settlement screen, and set the restart button to restart the game.

## Game display

Regarding the UI scene design of the game, here I used a unity plugin called NGUI. After a series of studies, I think that compared to the UGUI component that unity comes with, the NGUI plugin is very flexible and convenient in creating sprites and saving atlases. In addition, NGUI's tween animation function is very worry-free, no need to define additional code, I can use the packaged script to achieve some simple animation.

In terms of simple animation effects, I also tried to use some animation plug-ins such as DOTween, however there are many code conflicts in the debugging code. So I have to give up and use the system's own animation system and NGUI. The tween component included with the plugin to design animation effects

<img src="{{ site.baseurl }}/image%20file/233.png" width="400" height="240">

## Game Scripts

This is the hardest part of game design. Unfortunately, this part has not been fully completed so far. This is the hardest part of game design. Unfortunately, this part has not been fully completed so far. In the game, I designed scripts that includes card design, scripts for UI interface design, and scripts for running the game. And used most of the time to compile and modify the scripts.  The card scripts use to design various elements of the card, such as weight, type, attribution, and card rules and click card events. UI scripts are use to set the interface of the game and the interaction between the player and the game; the Game-manager scripts are use to run the game's progress, switch scenes, store data, clear the cache, etc. 

<img src="{{ site.baseurl }}/image%20file/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20181214190245.png">

# Postmortem

As mentioned before, due to the lack of code programming ability of my own, script errors and logic errors frequently occur during code writing, which leads me to spend a lot of time debugging and improving. This also makes my game still in an unfinished version. In order to make the game have the most basic running effect, I have to simplify the rules of the game and the elements of the scene in some scripts. For example, regarding the determination of the order of the players' cards, since the draw decision script cannot be written so far, I can only simply set two buttons to let the player choose whether to play first or last. Besides, In terms of computer player's AI design, in order to test, I set up a manual operation of the computer player to play, and the logic of the card is only designed to be higher than the player's hand weight.

Another test question is about the use of the NGUI plugin. Due to the inexperience of the use of the plug-in, there is often a problem that the game image cannot be displayed or some images such as the front and back of the card are displayed incorrectly. So I had to use the DOTWeen animation plugin in the beta version to try to make the animation of the game and the image display more stable.

<img src="{{ site.baseurl }}/image%20file/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20181214181747.png" width="200" height="300">

# Future Plan

It's a great pity for me that I can't design the finished version of this card game in a limited time. But I still don't give up this project after the end of the semester. The urgent task in the future is how to improve my code ability to improve the rules script and the computer AI script is a problem I have to overcome in the next time. 

At the same time, I will also improve the game's graphics and animations, such as beautifying the game scene and card display, improving the animation of the game and so on.

I will continue to improve this game and update the improved game version and game video on this project website. 

After completing the final version of the game, I will export the game to PC version and Mac version and update the downloaded link on this project website.

# Download (demo version)
PC

Mac
