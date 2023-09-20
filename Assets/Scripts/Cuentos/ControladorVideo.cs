using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

    public class ControladorVideo : MonoBehaviour
    {
        //Variables de los botones para los videos
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private GameObject btn_play;
        [SerializeField] private GameObject btn_pause;
        [SerializeField] private GameObject btn_reset;
        public bool play;

        private AudioSource audioSource;

        private void Start()
        {
            videoPlayer.Play();
            videoPlayer.Pause();
            GameObject audioObject = GameObject.Find("Audio");

            if (audioObject != null)
            {
                audioSource = audioObject.GetComponent<AudioSource>();
                AudioGameMute();
            }
            else
            {
                Debug.Log("El objeto Audio no se encontro en la Escena");
            }

        }

        //Funcion para mutear el audio del juego
        public void AudioGameMute()
        {
            audioSource.mute = true;
        }

        //Funcion para desmutear el audio del juego
        public void AudioGamePlay()
        {
            audioSource.mute = false;
        }

        //Funcion para reproducir el video
        public void PlayVideo()
        {
            videoPlayer.Play();
            play = true;
        }

        //Funcion para pausar el video
        public void PauseVideo()
        {
            videoPlayer.Pause();
            play = false;
            
        }

        //Funcion para resetear el video
        public void ResetVideo()
        {
            videoPlayer.frame = 0;
            PlayVideo();
        }
    }


