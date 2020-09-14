using System;
using System.Collections.Generic;
using UnityEngine;

public class GoodsService : MonoBehaviour
{
    public const int FIRST_RUNE_SALES_ID = 1;
    public const int RANDOM_POTION_SALES_ID = 9;
    public const int RANDOM_ARTIFACTPIECE_SALES_ID = 16;

    public const int MIN_NUMBER_OF_RANDOM_RUNES = 1;

    public const string GOLD_IMAGE_PATH = "Images/Main/Top/Gold";
    public const string DIAMOND_IMAGE_PATH = "Images/Main/Top/Diamond";

    public readonly static Sprite GOLD_IMAGE = Resources.Load<Sprite>(GOLD_IMAGE_PATH);
    public readonly static Sprite DIAMOND_IMAGE = Resources.Load<Sprite>(DIAMOND_IMAGE_PATH);

    //public static readonly List<int> RUNE_SALES_ID_LIST = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    public static readonly List<(int runeId, RuneRating rating)> RANDOM_RUNE_SALES_ID_AND_RATING_LIST =
        new List<(int, RuneRating)> 
        {
            (10, RuneRating.Normal),
            (11, RuneRating.Normal),
            (12, RuneRating.Unique),
            (13, RuneRating.Unique)
        };

    public static readonly List<int> GOLD_SALES_ID_LIST =
        new List<int>
        {
            25,
            26,
            27
        };

    public static readonly List<int> HEART_SALES_ID_LIST =
    new List<int>
    {
        31,
        32,
        33
    };

}
