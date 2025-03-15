using UnityEngine;

public class MenuClickHandler : MonoBehaviour
{
    public GameObject MusicGameObject;
    public GameObject MenuGameObject;

    bool menuShown = false;

    private void Start()
    {
        MusicGameObject.SetActive(true);
        MenuGameObject.SetActive(false);
    }

    public void SwitchShowMenu()
    {
        menuShown = !menuShown;

        if(menuShown)
        {
            MusicGameObject.SetActive(false);
            MenuGameObject.SetActive(true);
        }
        else
        {
            MusicGameObject.SetActive(true);
            MenuGameObject.SetActive(false);
        }
    }
}
