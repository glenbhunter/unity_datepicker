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

    [Header("Pressed")]
    [SerializeField] Color32 m_BTN_Pressed_Color = new Color32(45, 152, 255, 255);
    [SerializeField] Color32 m_TXT_Pressed_Color = new Color32(50, 50, 50, 255);


    [SerializeField] float m_FadeDuration = .1f;
    [SerializeField] Image m_ButtonIMG;
    [SerializeField] Text m_ButtonText;

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

            FadeButton(m_BTN_Hover_Color);
            FadeButtonText(m_TXT_Hover_Color);
        }
    }

    public void Pressed()
    {
        if(m_CurrentState != State.Normal && m_CurrentState != State.Pressed)
        {
            m_CurrentState = State.Pressed;

            FadeButton(m_BTN_Pressed_Color);
            FadeButtonText(m_TXT_Pressed_Color);
        }
    }

    public void Highlight()
    {
        if(m_CurrentState != State.Highlighted)
        {
            m_CurrentState = State.Highlighted;

            FadeButton(m_BTN_Highlight_Color);
            FadeButtonText(m_TXT_Highlight_Color);
        }
    }

    public void Clear()
    {

        if(m_CurrentState != State.Normal && m_CurrentState == State.Hover)
        {
            m_CurrentState = State.Normal;
           
            if(m_DayIsInCurrentCalenderMonth)
            {
                FadeButton(m_BTN_Normal_Color);
                FadeButtonText(m_TXT_Normal_Color);
            }
            else
            {
                FadeButton(m_BTN_Normal_FadedOut_Color);
                FadeButtonText(m_TXT_Normal_FadedOut_Color);
            }
        }
    }

    public void ForcePressed()
    {
        m_CurrentState = State.Pressed;

        FadeButton(m_BTN_Pressed_Color);
        FadeButtonText(m_TXT_Pressed_Color);
    }

    public void ForceClear()
    {
        if(m_CurrentState != State.Normal && m_CurrentState == State.Pressed || m_CurrentState == State.Highlighted)
        {
            m_CurrentState = State.Normal;

            if (m_DayIsInCurrentCalenderMonth)
            {
                FadeButton(m_BTN_Normal_Color);
                FadeButtonText(m_TXT_Normal_Color);
            }
            else
            {
                FadeButton(m_BTN_Normal_FadedOut_Color);
                FadeButtonText(m_TXT_Normal_FadedOut_Color);
            }
        }
    }


    public void FadeButton(Color32 targetColor)
    {
        m_ButtonIMG.CrossFadeColor(targetColor, m_FadeDuration, true, true);
    }

    public void FadeButtonText(Color32 targetColor)
    {
        m_ButtonText.CrossFadeColor(targetColor, m_FadeDuration, true, true);
    }
}

