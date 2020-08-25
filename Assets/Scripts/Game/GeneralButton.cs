using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to play sound fx on button click
/// </summary>
public class GeneralButton : MonoBehaviour
{
    [SerializeField] private Sound sound; //Sound the button will make when pressed.

    protected virtual void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    protected virtual void ButtonPressed()
    {
        AudioManager.Instance.PlayAudio(sound);
    }
}