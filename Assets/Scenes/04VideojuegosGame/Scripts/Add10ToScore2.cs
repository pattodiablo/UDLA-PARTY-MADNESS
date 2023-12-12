using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class Add10ToScore2: MonoBehaviour
{

private object LocalScore;
     public void add10(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
        if (Variables.Application.IsDefined("LocalScore"))
        {

            LocalScore = Variables.Application.Get("LocalScore");
       if (LocalScore != null)
        {
            if (int.TryParse(LocalScore.ToString(), out int scoreActual))
            {
                // Suma 10 puntos al score actual
                scoreActual += 10;

                // Guarda el nuevo valor de score donde sea necesario (por ejemplo, PlayerPrefs)
                Variables.Application.Set("LocalScore", scoreActual);

                // Puedes imprimir el valor actualizado si lo deseas
                Debug.Log("LocalScore actualizado: " + scoreActual);
            }
            else
            {
                Debug.LogError("No se pudo convertir el objeto a un valor entero.");
            }
        }
           


        }
    }



}
