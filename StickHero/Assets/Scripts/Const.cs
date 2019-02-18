using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{

    public static bool isMode1;
    public static class Tag
    {
        public const string PLAYER = "Player";
        public const string TOWER = "Tower";
        public const string MELON = "Melon";
        public const string TRANSFORM = "Tranform";
        public const string STICK = "Stick";
        public const string STAR = "Star";
        public const string DIE = "Die";
    }

    public static class Audio
    {
        public const string BACKGROUND = "Background";
        public const string SLIDEWATERMELON = "SlideWaterMelon";
        public const string HITPLATFORM = "HitPlatform";
        public const string LANDING = "Landing";
        public const string SLIDE = "Slide";
        public const string DEAD = "Dead";
        public const string SCORE = "Score";
        public const string EATFRUIT = "EattingFruit";
        public const string AUDIO = "Audio";
        public const string BUTTON = "Button";
        public const string STICKGROW = "StickGrow";
        public const string PERFECT = "Perfect";
        public const string KICK = "Kick";
        public const string FLIP = "Flip";
        public const string STAR = "Star";
    }

    public static class Scenes
    {
        public const string MODE2 = "MainScene";
        public const string MODE1 = "Mode1";
    }

    public static class ScoreInfo
    {
        public const string BESTSCOREMODE1 = "BestScoreMode1";
        public const string BESTSCORE = "BestScore";
        public const string TOTALSTAR = "TotalStar";
        public static string[] CHARACTERACTIVED = new string[5] {"char0","char1","char2","char3","char4"};
    }

    public static class Anim
    {
        public const string SCALE = "scaleText";
        public const string SHOPANIM = "ShopAnim";
        public const string HIDESHOP = "HideShop";
        public const string ISMOVE = "isMove";

    }

    public static class PlayerInfo
    {
        public const string CURRENTPLAYER = "CurrentPlayer";
    }
}
