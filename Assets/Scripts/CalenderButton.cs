using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalenderButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] Color32 m_NormalColor;
    [SerializeField] Color32 m_HighlightColor;
    [SerializeField] Color32 m_PressedColor;

    UnityAction m_OnClick;
    CalenderManager m_CalenderManager;
    private bool m_Selected = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!m_Selected)
            m_Button.CrossFadeColor(m_HighlightColor, .1f, true, true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Selected = !m_Selected;

        m_Button.CrossFadeColor((m_Selected) ? m_PressedColor : m_NormalColor, .1f, true, true);

        m_OnClick?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!m_Selected)
            m_Button.CrossFadeColor(m_NormalColor, .1f, true, true);
    }

    public void OnDeselect()
    {
        m_Selected = false;

        m_Button.CrossFadeColor(m_NormalColor, .1f, true, true);
    }

    [Header("Button")]
    [SerializeField] Image m_Button;

    [Header("Text")]
    [SerializeField] Text m_Text;

    private int m_Day;
    private int m_Month;

    public void Setup(int day, int month, CalenderManager manager)
    {
        m_Text.text = day.ToString();

        m_Day = day;
        m_Month = month;
        m_CalenderManager = manager;
        m_OnClick += () =>
        {
            m_CalenderManager.OnClick_SelectedCalenderDay(this, m_Month, m_Day);
        };
    }

    public void DayIsPartOfCurrentMonth()
    {
        FadeImageAlpha(m_Text, 1f);
    }

    public void DayIsPartOfPreviousMonth()
    {
        FadeImageAlpha(m_Text, 0.20f);
    }

    public void DayIsToday()
    {
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <param name="alpha">must between 0 and 1f </param>
    private void FadeImageAlpha(Text textComponent, float alpha)
    {
        Color32 color = textComponent.color;
        color.a = (byte)(alpha * 255);

        textComponent.color = color;
    }

   
}   