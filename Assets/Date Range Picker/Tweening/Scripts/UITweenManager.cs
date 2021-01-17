using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;


    /// <summary>
    /// This must in scene in order for tweening to work
    /// </summary>
    public class UITweenManager : MonoBehaviour
    {
        private List<UIItem> m_TweeningItems = new List<UIItem>();

        public void Tween(Text textComponent, Color32 to, UnityAction onComplete, float duration = 1)
        {
            int instanceId = textComponent.GetInstanceID();

            if(!IsItemCurrentlyTweening(instanceId))
            {
                m_TweeningItems.Add(new UITextItem(textComponent.color, to, textComponent, () =>
                {
                    m_TweeningItems.RemoveAll(x => x.InstanceID == instanceId);

                    onComplete?.Invoke();
                },
                duration));
            }
            else
            {
                Debug.Log("Item is already tweening");
                return;
            }
        }

        public void Tween(Image imageComponent, Color32 to, UnityAction onComplete, float duration = 1)
        {
            int instanceId = imageComponent.GetInstanceID();

            if (!IsItemCurrentlyTweening(instanceId))
            {
                m_TweeningItems.Add(new UIImageItem(imageComponent.color, to, imageComponent, () =>
                {
                    m_TweeningItems.RemoveAll(x => x.InstanceID == instanceId);

                    onComplete?.Invoke();
                },
                duration));
            }
            else
            {
                Debug.Log("Item is already tweening");
                return;
            }
        }


        /// <summary>
        /// If current component is listed for tweening, it will be cancelled and new tween will take place
        /// </summary>
        public void ForceTween(Text textComponent, Color32 to, UnityAction onComplete, float duration = 1)
        {
            int instanceId = textComponent.GetInstanceID();

            if (IsItemCurrentlyTweening(instanceId))
                CancelTween(instanceId);

            if(duration == 0)
            {
                textComponent.color = to;
                onComplete?.Invoke();
                return;
            }

            m_TweeningItems.Add(new UITextItem(textComponent.color, to, textComponent, () =>
            {
                m_TweeningItems.RemoveAll(x => x.InstanceID == instanceId);

                onComplete?.Invoke();
            },
            duration));
        }

        /// <summary>
        /// If current component is listed for tweening, it will be cancelled and new tween will take place
        /// </summary>
        public void ForceTween(Image imageComponent, Color32 to, UnityAction onComplete, float duration = 1)
        {
            int instanceId = imageComponent.GetInstanceID();

            if (IsItemCurrentlyTweening(instanceId))
                CancelTween(instanceId);

            if(duration == 0)
            {
                imageComponent.color = to;
                onComplete?.Invoke();
                return;
            }

            m_TweeningItems.Add(new UIImageItem(imageComponent.color, to, imageComponent, () =>
            {
                m_TweeningItems.RemoveAll(x => x.InstanceID == instanceId);

                onComplete?.Invoke();
            },
            duration));
        }

        public void CancelTween(int instanceId, UnityAction onComplete = null)
        {
            m_TweeningItems.RemoveAll(x => x.InstanceID == instanceId);

            onComplete?.Invoke();

        }

        private bool IsItemCurrentlyTweening(int instanceId)
        {
            foreach(UIItem item in m_TweeningItems)
            {
                if(item.InstanceID == instanceId)
                {
                  
                    return true;
                }
            }

            return false;
        }

        private void Update()
        {
            if(m_TweeningItems != null)
            {
                for (int i = 0; i < m_TweeningItems.Count; i++)
                {
                    m_TweeningItems[i].Update(Time.deltaTime);
                }
            }
        }
    }

    public class UITextItem : UIItem
    {
        public Text Text { get; private set; }

        public UITextItem(Color32 from, Color32 to, Text textComponent, UnityAction onComplete, float duration = 1)
            : base(textComponent.GetInstanceID(), from, to, onComplete, duration)
        {
            Text = textComponent;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Text.color = CurrentColor;
        }
    }

    public class UIImageItem : UIItem
    {
        public Image Image { get; private set; }

        public UIImageItem(Color32 from, Color32 to, Image imageComponent, UnityAction onComplete, float duration = 1)
            : base(imageComponent.GetInstanceID(), from, to, onComplete, duration)
        {
            Image = imageComponent;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Image.color = CurrentColor;
        }
    }

    public abstract class UIItem
    {
        public enum State
        {
            Tweening,
            Sleep,
        }

        public int InstanceID { get; private set; }
        private State m_CurrentState;
        private Color32 m_FromColor;
        private Color32 m_TargetColor;
        protected Color32 CurrentColor;
        private float m_Time = 0.0f;

        private UnityAction m_OnCompleteCallback;
        private float m_Duration;

        public UIItem(int instanceId, Color32 from, Color32 to, UnityAction onComplete, float duration = 1)
        {
            InstanceID = instanceId;
            m_FromColor = from;
            m_TargetColor = to;
            m_OnCompleteCallback = onComplete;
            m_CurrentState = State.Tweening;
            m_Time = 0.0f;
            m_Duration = duration;
        }

        public virtual void Update(float deltaTime)
        {
            if(m_CurrentState == State.Tweening)
            {
                m_Time += deltaTime / m_Duration;

                CurrentColor = Color32.Lerp(m_FromColor, m_TargetColor, m_Time);

                if(m_Time >= 1f)
                {
                    m_CurrentState = State.Sleep;
                    m_OnCompleteCallback?.Invoke();
                }
            }
        }
}
