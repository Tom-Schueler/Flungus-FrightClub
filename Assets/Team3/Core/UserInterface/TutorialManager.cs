using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class TutorialSlide
{
    public Sprite image;
    public string description;
}
public class TutorialManager : MonoBehaviour
{
    [Header("UI References")]
    public Image slideImage;
    public TextMeshProUGUI slideText;
    public Button nextButton;
    public Button prevButton;
    

    [Header("Slides")]
    public List<TutorialSlide> slides;

    private int currentSlide = 0;

    void Start()
    {
        ShowSlide(0);

        nextButton.onClick.AddListener(NextSlide);
        prevButton.onClick.AddListener(PreviousSlide);
    }

    void ShowSlide(int index)
    {
        if (index < 0 || index >= slides.Count) return;

        currentSlide = index;
        slideImage.sprite = slides[index].image;
        slideText.text = slides[index].description;

        prevButton.interactable = currentSlide > 0;
        nextButton.interactable = currentSlide < slides.Count - 1;
    }

    void NextSlide()
    {
        ShowSlide(currentSlide + 1);
        ButtonCheck();
    }

    void PreviousSlide()
    {
        ShowSlide(currentSlide - 1);
        ButtonCheck();
    }

    void ButtonCheck()
    {
        if (currentSlide == 0)
        {
            prevButton.interactable = false;
        }
        else if (currentSlide == slides.Count)
        {
            nextButton.interactable = false;
        }
    }
}
