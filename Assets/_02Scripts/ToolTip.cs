using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text m_toolTipText;
    private Text m_contentText;
    private CanvasGroup m_canvasGroup;
    
    private Text M_ToolTipText
    {
        get
        {
            if (m_toolTipText == null)
            {
                m_toolTipText = GetComponent<Text>();
            }
            return m_toolTipText;
        }
    }
    private Text M_ContentText
    {
        get
        {
            if(m_contentText==null)
            {
                m_contentText = transform.Find("Content").GetComponent<Text>();
            }
            return m_contentText;
        }
    }
    private CanvasGroup M_CanvasGroup
    {
        get
        {
            if (m_canvasGroup == null)
            {
                m_canvasGroup = GetComponent<CanvasGroup>();
            }
            return m_canvasGroup;
        }
    }

    private float targetAlpha = 0;
    [SerializeField]
    private float smoothing = 1;

    private void Update()
    {
        if (M_CanvasGroup.alpha != targetAlpha)
        {
            M_CanvasGroup.alpha = Mathf.Lerp(M_CanvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(M_CanvasGroup.alpha - targetAlpha) < 0.01f)
            {
                M_CanvasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Show(string text)
    {
        M_ToolTipText.text = text;
        M_ContentText.text = text;
        targetAlpha = 1;
    }
    public void Hide()
    {
        targetAlpha = 0;
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
