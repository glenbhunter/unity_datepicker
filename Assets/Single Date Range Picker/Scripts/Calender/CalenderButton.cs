
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalenderButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public enum State
    {
        Normal,
        Hover,
        Selected,
        Highlighted,
        Disabled,
    }

    [SerializeField] Text m_ButtonText;

    private Dictionary<State, DisplayState> m_DisplayDictionary;
    [SerializeField] Image m_ButtonPrimaryImage;
    [SerializeField] Image m_ButtonSecondaryImage;
    [SerializeField] DisplayState m_NormalState;
    [SerializeField] DisplayState m_SelectedState;
    [SerializeField] DisplayState m_HoverState;
    [SerializeField] DisplayState m_HighlightedState;
    [SerializeField] DisplayState m_DisabledState;

    public State CurrentState { get; private set; }

    private Calender m_Calender;
    public DateTime Date { get; private set; }

    private bool m_ShowDaysInOtherMonths;

    public void Setup(Calender calender, DateTime buttonDate, string text, bool showDaysInOtherMonths, UITweenManager uiTweenManager)
    {

        m_DisplayDictionary = new Dictionary<State, DisplayState>();
        m_Calender = calender;
        Date = buttonDate;
        m_ButtonText.text = text;
        m_ShowDaysInOtherMonths = showDaysInOtherMonths;

        m_DisplayDictionary.Add(State.Normal, m_NormalState);
        m_DisplayDictionary.Add(State.Hover, m_HoverState);
        m_DisplayDictionary.Add(State.Selected, m_SelectedState);
        m_DisplayDictionary.Add(State.Highlighted, m_HighlightedState);
        m_DisplayDictionary.Add(State.Disabled, m_DisabledState);

        foreach(KeyValuePair<State, DisplayState> displayState in m_DisplayDictionary)
        {
            displayState.Value.Setup(m_ButtonPrimaryImage, m_ButtonSecondaryImage, m_ButtonText, uiTweenManager);
        }

        if (!m_ShowDaysInOtherMonths && buttonDate.Month != calender.Date.Month)
        {
            UpdateState(State.Disabled, m_Calender.Date, null, null);
            return;
        }

        // Force normal display script to trigger
        UpdateState(State.Normal, m_Calender.Date, null, null);
    }

    public void ResetToOriginal()
    {
        if (!m_ShowDaysInOtherMonths && Date.Month != m_Calender.Date.Month)
        {
            UpdateState(State.Disabled, m_Calender.Date, null, null);
            return;
        }

        // Force normal display script to trigger
        UpdateState(State.Normal, m_Calender.Date, null, null);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentState != State.Disabled && m_Calender.PointerEnter != null )
            m_Calender.PointerEnter(this, m_Calender);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(CurrentState != State.Disabled && m_Calender.PointerDown != null)
            m_Calender.PointerDown(this, Date, m_Calender);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CurrentState != State.Disabled && m_Calender.PointerExit != null)
            m_Calender.PointerExit(this, m_Calender);
    }

    public void UpdateState(State newState, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        CurrentState = newState;
        m_DisplayDictionary[newState].UpdateState(Date, calenderDate, selectedStartDate, selectedEndDate);
    }
}
