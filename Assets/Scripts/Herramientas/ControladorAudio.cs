using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    private float value;
    public static bool existAudio = false;

    void Awake()
    {      
        AudioVolume();
        // Evitar que el objeto se destruya al cambiar de escena
        if(!existAudio)
        {
            DontDestroyOnLoad(gameObject);
            existAudio = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Funcion para obtener el valor del audio de la configuracion en la seccion opciones - audio
    public void AudioVolume()
    {      
        value = PlayerPrefs.GetFloat("volumenAudio");
        AudioListener.volume = value;    
    }
}
