using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider audioSlider;
    public void setVolume(float volume)
    {
        AudioController.Instance.volume(audioSlider.value);
    }
}
