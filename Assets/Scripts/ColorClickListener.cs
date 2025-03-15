using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorClickListener : MonoBehaviour
{
    public UnityEvent<int, Color> OnColorSelected;

    Image image;

    private void Start()
    {
       image = GetComponent<Image>(); 
    }

    public void SelectColor(int value)
    {
        OnColorSelected.Invoke(value, image.color);
    }
}
