using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvas;

    public void OpenClose()
    {
        if(_canvas.alpha==0)
        {
            _canvas.alpha = 1;
            _canvas.blocksRaycasts = true;
        }
        else
        {
            _canvas.alpha = 0;
            _canvas.blocksRaycasts = false;
        }
    }
    public void Open()
    {
                if(_canvas.alpha==0)
        {
            _canvas.alpha = 1;
            _canvas.blocksRaycasts = true;
        }
    }
    public void Close()
    {
        if (_canvas.alpha > 0)
        {
            _canvas.alpha = 0;
            _canvas.blocksRaycasts = false;
        }
    }
}
