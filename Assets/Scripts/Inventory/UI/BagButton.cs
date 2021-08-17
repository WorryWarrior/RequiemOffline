using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class responsible for various UI components' toggle on/off button behaviour. Determines what button sprite should be displayed.
/// </summary>
public class BagButton : MonoBehaviour, IPointerClickHandler
{
    public Button button;
    [HideInInspector] public bool switcher = true;
    public Sprite open;
    public Sprite closed;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    /// <summary>
    /// Changes button sprite upon button press.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (switcher)
                button.image.sprite = open;
            else
                button.image.sprite = closed;
            switcher = !switcher;
        }
    }

}
