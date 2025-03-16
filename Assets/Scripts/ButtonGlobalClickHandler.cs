using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonGlobalClickHandler : MonoBehaviour
{
    public Image imageUp;
    public Image imageDown;

    bool globalViewActivated = false;

    public UnityEvent OnZoom;
    public UnityEvent OnDezoom;

    public void HandleGlobalClick()
    {
        if (globalViewActivated)
        {
            imageUp.gameObject.SetActive(false);
            imageDown.gameObject.SetActive(true);

            OnZoom.Invoke();
        }
        else
        {
            imageUp.gameObject.SetActive(true);
            imageDown.gameObject.SetActive(false);

            OnDezoom.Invoke();
        }

        globalViewActivated = !globalViewActivated;
    }

    //public void Zoom()
    //{
    //    imageUp.gameObject.SetActive(false);
    //    imageDown.gameObject.SetActive(true);
    //}

    //public void Dezoom()
    //{
    //    imageUp.gameObject.SetActive(true);
    //    imageDown.gameObject.SetActive(false);
    //}
}
