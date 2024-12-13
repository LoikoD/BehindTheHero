using DG.Tweening;
using UnityEngine.UI;

public class PanelOpener
{
    public void PanelClck(Image panel)
    {
        if (panel.transform.localScale.x == 0)
            panel.transform.DOScaleX(1, 0.1f)
                .SetEase(Ease.InCubic);
        else
            panel.transform.DOScaleX(0, 0.1f)
                .SetEase(Ease.InCubic);
    }
}
