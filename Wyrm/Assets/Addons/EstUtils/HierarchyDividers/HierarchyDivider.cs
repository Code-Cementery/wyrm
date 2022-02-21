using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyDivider : MonoBehaviour
{
    public Color bgColor;

    void Start()
    {
        
    }


#if UNITY_EDITOR
    [ContextMenu("Set PURPLE")] void _AddColor1() { ColorUtility.TryParseHtmlString("#5C4C9E", out bgColor); }
    [ContextMenu("Set RED")] void _AddColor2() { ColorUtility.TryParseHtmlString("#CC3750", out bgColor); }
    [ContextMenu("Set ORANGE")] void _AddColor3() { ColorUtility.TryParseHtmlString("#F5502F", out bgColor); }
    [ContextMenu("Set BLUE")] void _AddColor4() { ColorUtility.TryParseHtmlString("#238ED0", out bgColor); }
    [ContextMenu("Set YELLOW")] void _AddColor5() { ColorUtility.TryParseHtmlString("#FFFF00", out bgColor); }
    [ContextMenu("Set BLACK")] void _AddColor6() { ColorUtility.TryParseHtmlString("#1F272F", out bgColor); }
#endif

}
