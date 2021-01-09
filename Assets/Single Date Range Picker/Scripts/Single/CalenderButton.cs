
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

    [SerializeField] Text m_Text;

    private Dictionary<State, DisplayState> m_DisplayDictionary;
    [SerializeField] DisplayState m_NormalState;
    [SerializeField] DisplayState m_SelectedState;
    [SerializeField] DisplayState m_HoverState;
    [SerializeField] DisplayState m_HighlightedState;
    [SerializeField] DisplayState m_DisabledState;

    public State CurrentState { get; private set; }

    private int m_ButtonIndex;
    private Calender m_Calender;
    private DateTime m_ButtonDate;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonIndex"></param>
    /// <param name="calender"></param>
    /// <param name="buttonDate"></param>
    /// <param name="text"></param>
    /// <param name="disable">decided whether or not to disable button, generally used if the calender date does not want to be shown</param>
    public void Setup(int buttonIndex, Calender calender, DateTime buttonDate, string text, bool disable)
    {

        m_DisplayDictionary = new Dictionary<State, DisplayState>();
        m_ButtonIndex = buttonIndex;
        m_Calender = calender;
        m_ButtonDate = buttonDate;
        m_Text.text = text;

        if (disable)
        {
            m_DisplayDictionary.Add(State.Disabled, m_DisabledState);
            UpdateState(State.Disabled, m_Calender.Date, null, null);
            return;
        }

        m_DisplayDictionary.Add(State.Normal, m_NormalState);
        m_DisplayDictionary.Add(State.Hover, m_HoverState);
        m_DisplayDictionary.Add(State.Selected, m_SelectedState);
        m_DisplayDictionary.Add(State.Highlighted, m_HighlightedState);


   
       
        // Force normal display script to trigger
        UpdateState(State.Normal, m_Calender.Date, null, null);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentState != State.Disabled && m_Calender.PointerEnter != null )
            m_Calender.PointerEnter(m_ButtonIndex, m_Calender);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(CurrentState != State.Disabled && m_Calender.PointerDown != null)
            m_Calender.PointerDown(m_ButtonIndex, m_ButtonDate, m_Calender);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CurrentState != State.Disabled && m_Calender.PointerExit != null)
            m_Calender.PointerExit(m_ButtonIndex, m_Calender);
    }

    public void UpdateState(State newState, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        CurrentState = newState;
        m_DisplayDictionary[newState].UpdateState(m_ButtonDate, calenderDate, selectedStartDate, selectedEndDate);
    }
}
