using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public Toggle Music_Toggle;
    public AudioSource Music_Object;

    public void Set_Music_Enabled()
    {
        Music_Object.gameObject.SetActive(Music_Toggle.isOn);
    }
}
