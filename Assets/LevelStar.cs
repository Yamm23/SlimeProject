using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class StarRatingUI : MonoBehaviour
{
    // References to the star images
    public Image star1Image;
    public Image star2Image;
    public Image star3Image;

    // Full and empty star sprites
    public Sprite fullStarSprite;
    public Sprite emptyStarSprite;

    // Function to update the star UI based on the number of stars
    public void UpdateStarUI(int starCount)
    {
        // Update each star's image based on the number of stars earned
        star1Image.sprite = starCount >= 1 ? fullStarSprite : emptyStarSprite;
        star2Image.sprite = starCount >= 2 ? fullStarSprite : emptyStarSprite;
        star3Image.sprite = starCount >= 3 ? fullStarSprite : emptyStarSprite;
    }
}

