using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SlackLogger : MonoBehaviour
{
    private string webhookUrl = "https://hooks.slack.com/services/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";

        //Funcion para habilitar el envio de logs al slack cuando se active el objeto al que se encuentra vinculado el script
    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    //Funcion para deshabilitar el envio de logs al slack cuando se active el objeto al que se encuentra vinculado el script
    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    //Funcion para detectar los logs de unity
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logType = type.ToString().ToUpper();
        string message = $"{logType}: {logString}\n{stackTrace}";

        StartCoroutine(PostMessageToSlack(message));
    }

    //Funcion para enviar los logs del sistema al slack configurado
    private IEnumerator PostMessageToSlack(string message)
    {
        SlackMessage slackMessage = new SlackMessage { text = message };
        string jsonMessage = JsonUtility.ToJson(slackMessage);

        using (var request = new UnityWebRequest(webhookUrl, "POST"))
        {
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonMessage);
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error al enviar mensaje a Slack: {request.error}");
            }
        }
    }

    // Llamar a OnDisable cuando el objeto se destruya para des-suscribirse adecuadamente del evento.
    private void OnDestroy()
    {
        OnDisable();
    }
 
}

// Clase para el mensaje de Slack
[Serializable]
public class SlackMessage
{
    public string text;
}
