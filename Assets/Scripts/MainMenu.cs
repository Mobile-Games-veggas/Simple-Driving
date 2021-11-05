using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private iOSNotificationHandler iOSNotificationHandler;
    [SerializeField] private Button playButton;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private int energy;

    private void Start() 
    {
        OnApplicationFocus(true);    
    }

    private void OnApplicationFocus(bool hasFocus) 
    {
        if(!hasFocus) { return; };

        CancelInvoke(); // отменяем все Инвоки

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey,0); // отображаем макс результат

        highScoreText.text = $"High Score {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy); // сколько Е осталось

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty); // когда восстановится?

            if (energyReadyString == string.Empty) { return; }; // проверка на ошибки

            DateTime energyReady = DateTime.Parse(energyReadyString); // конвертируем строку в время

            if (DateTime.Now > energyReady) // если время сейчас больше того, когда восстановились, значит откат прошел
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                // playButton.interactable = false; // плохо работает, если игрок остался в главном меню, но свернул приложение. То тогда Инвок прерывается
                Invoke(nameof(EnergyRecharget), (energyReady - DateTime.Now).Seconds); // иначе запускаем инвок через время, оставшееся до конца отката. На случай, если игрок остался в главном меню
            }
        }

        energyText.text = $"Play ({energy})"; // отображаем сколько Е осталось
    }

    private void EnergyRecharget()
    {
        // playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play ({energy})";
    }

    public void Play()
    {
        if (energy <1) { return; }

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy); // записываем сколкьо Е осталось

        if (energy == 0) // БАГ в том, что если я использую последнюю попытку, то мы уже заходим в игру и уже заходим сюда в цикл и запускаем уведомоления через время, а значит если мы в игре продержимся, то придет уведомление о восстановлении, хотя мы используем еще одну попытку. Значит нужно делать так, чтобы уведомления приходили после последней попытки (то есть когда проиграл и энергии 0, а не последнюю попытку использую)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration); // через сколько восстановится

            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString()); // записываем время в которое восстановится
#if  UNITY_ANDROID
            androidNotificationHandler.SchedulNotification(energyReady);
#elif UNITY_IOS
            iOSNotificationHandler.ScheduleNotification(energyRechargeDuration);
#endif
        }

        SceneManager.LoadScene(1); // запуск игры сцены
    }
}
