using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CalenderButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    private enum State
    {
        Normal,
        Pressed,
        Hover, 
        Highlighted,
        IsInBetween,
    }

    [Header("Normal")]
    [SerializeField] Color32 m_BTN_Normal_Color = Color.white;
    [SerializeField] Color32 m_TXT_Normal_Color = new Color32(50, 50, 50, 255);

    [Header("Normal but not part of current calender month")]
    [SerializeField] Color32 m_BTN_Normal_FadedOut_Color = Color.white;
    [SerializeField] Color32 m_TXT_Normal_FadedOut_Color = new Color32(50, 50, 50, 100);

    [Header("Hover")]
    [SerializeField] Color32 m_BTN_Hover_Color = new Color32(45, 152, 255, 150);
    [SerializeField] Color32 m_TXT_Hover_Color = new Color32(50, 50, 50, 150);

    [Header("Highlight")]
    [SerializeField] Color32 m_BTN_Highlight_Color = new Color32(45, 152, 255, 255);
    [SerializeField] Color32 m_TXT_Highlight_Color = new Color32(50, 50, 50, 255);

    [Header("InBetweenHightlight")]
    [SerializeField] Color32 m_BTN_InBetweenColor = new Color32(45, 152, 255, 255);

    [Header("Pressed")]
    [SerializeField] Color32 m_BTN_Pressed_Color = new Color32(45, 152, 255, 255);
    [SerializeField] Color32 m_TXT_Pressed_Color = new Color32(50, 50, 50, 255);


    [SerializeField] float m_FadeDuration = .1f;
   
    [SerializeField] Text m_ButtonText;

    /// <summary>
    /// Primary image used for calender button for example circle
    /// </summary>
    [SerializeField] Image m_Primary_Image;
    [SerializeField] Image m_SecondaryImage;

    [SerializeField] Sprite m_Secondary_Square_Left;
    [SerializeField] Sprite m_Secondary_Square_Right;
    [SerializeField] Sprite m_Secondary_Rectangle_Center;

    private State m_CurrentState;

    private Calender m_Calender;
    private DateTime m_ButtonDate;
    private bool m_DayIsInCurrentCalenderMonth;

    public void Setup(DateTime btnDate, bool dayIsInCurrentCalenderMonth, Calender calender)
    {
        m_ButtonText.text = btnDate.Day.ToString();

        m_DayIsInCurrentCalenderMonth = dayIsInCurrentCalenderMonth;

        m_ButtonDate = btnDate;

        m_Calender = calender;
    }

    #region PointerClicks - Button States

    #endregion

    #region OverrideState - Button States

    #endregion



    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Calender.OnCalenderButtonClick(m_ButtonDate, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Clear();
    }

    public void Hover()
    {
        if (m_CurrentState == State.Normal)
        {
            m_CurrentState = State.Hover;

            FadePrimaryButton(m_BTN_Hover_Color);
            FadePrimaryButtonText(m_TXT_Hover_Color);
        }
    }

    public void Pressed()
    {
        if(m_CurrentState != State.Normal && m_CurrentState != State.Pressed)
        {
            m_CurrentState = State.Pressed;

            FadePrimaryButton(m_BTN_Pressed_Color);
            FadePrimaryButtonText(m_TXT_Pressed_Color);
        }
    }

    public void Highlight()
    {
        if(m_CurrentState != State.Highlighted)
        {
            m_CurrentState = State.Highlighted;

            FadePrimaryButton(m_BTN_Highlight_Color);
            FadePrimaryButtonText(m_TXT_Highlight_Color);

        }
    }

    public void IsInBetweenFirstAndSecondSelectedDates()
    {
        m_CurrentState = State.IsInBetween;

        FadePrimaryButton(Color.clear, 0);
        FadeSecondaryImage(m_BTN_InBetweenColor, 0);
        m_SecondaryImage.sprite = m_Secondary_Rectangle_Center;
    }

    public void Clear()
    {

        if(m_CurrentState != State.Normal && m_CurrentState == State.Hover)
        {
            m_CurrentState = State.Normal;
           
            if(m_DayIsInCurrentCalenderMonth)
            {
                FadePrimaryButton(m_BTN_Normal_Color);
                FadePrimaryButtonText(m_TXT_Normal_Color);
            }
            else
            {
                FadePrimaryButton(m_BTN_Normal_FadedOut_Color);
                FadePrimaryButtonText(m_TXT_Normal_FadedOut_Color);
            }
        }
    }

    public void ForcePressed(bool firstSelection, bool isLeft, bool isRight)
    {
        if (!firstSelection)
        {
            if (isLeft)
            {
                m_SecondaryImage.sprite = m_Secondary_Square_Left;
            }

            if (isRight)
            {
                m_SecondaryImage.sprite = m_Secondary_Square_Right;
            }

            FadePrimaryButton(m_BTN_Pressed_Color, 0);
            FadeSecondaryImage(m_BTN_InBetweenColor, 0);
            FadePrimaryButtonText(m_TXT_Pressed_Color);
        }

        m_CurrentState = State.Pressed;
    }

    public void ForceClear()
    {
        if(m_CurrentState != State.Normal && m_CurrentState == State.Pressed || m_CurrentState == State.Highlighted || m_CurrentState ==  State.IsInBetween)
        {
            m_CurrentState = State.Normal;

            // Clear secondary
            m_SecondaryImage.sprite = null;
            FadeSecondaryImage(Color.clear, 0);

            if (m_DayIsInCurrentCalenderMonth)
            {
                FadePrimaryButton(m_BTN_Normal_Color, 0);
                FadePrimaryButtonText(m_TXT_Normal_Color, 0);
            }
            else
            {
                FadePrimaryButton(m_BTN_Normal_FadedOut_Color, 0);
                FadePrimaryButtonText(m_TXT_Normal_FadedOut_Color, 0);
            }
        }
    }

    public void FadePrimaryButton(Color32 targetColor)
    {
        m_Primary_Image.CrossFadeColor(targetColor, m_FadeDuration, true, true);
    }

    public void FadePrimaryButton(Color32 targetColor, float fadeDuration)
    {
        m_Primary_Image.CrossFadeColor(targetColor, fadeDuration, true, true);
    }

    public void FadePrimaryButtonText(Color32 targetColor)
    {
        m_ButtonText.CrossFadeColor(targetColor, m_FadeDuration, true, true);
    }

    public void FadePrimaryButtonText(Color32 targetColor, float fadeDuration)
    {
        m_ButtonText.CrossFadeColor(targetColor, fadeDuration, true, true);
    }

    public void FadeSecondaryImage(Color32 targetColor, float fadeDuration)
    {
        m_SecondaryImage.CrossFadeColor(targetColor, fadeDuration, true, true);
    }
}

