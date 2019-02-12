using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{
    public static class Tag
    {
        public const string PLAYER = "Player";
        public const string TOWER = "Tower";
        public const string MELON = "Melon";
        public const string TRANSFORM = "Tranform";
        public const string STICK = "Stick";
    }

    public static class Audio
    {
        public const string BACKGROUND = "Background";
        public const string SLIDEWATERMELON = "SlideWaterMelon";
        public const string HITPLATFORM = "HitPlatform";
        public const string LANDING = "Landing";
        public const string SLIDE = "Slide";
        public const string DEAD = "Dead";
    }

    public static class Scenes
    {
        public const string MAINSCENE = "MainScene";
    }

    public static class ScoreInfo
    {
        public const string BESTSCORE = "BestScore";
    }
}
