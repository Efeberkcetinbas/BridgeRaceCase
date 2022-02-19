using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    //Singleton
    public static GameEvents gameEvents;

    void Awake()
    {
        gameEvents = this;
    }

    /*
     Kapı açılma ve kapanma eventi tanımlayalım. Fakat her levelde kapı sayısı değişeceği
     için id verelim ki her kapı açılmasın trigger olduğunda.
         */
    public event Action<int> onDoorTrigger;
    public event Action<int> offDoorTrigger;

    /*
     Şampiyonu belirleyeceğimiz GameEventi oluşturalım.
         */
    public event Action onChamp;

    public void DoorEnter(int id)
    {
        if (onDoorTrigger != null)
        {
            onDoorTrigger(id);
        }
    }

    public void DoorExit(int id)
    {
        if (offDoorTrigger != null)
        {
            offDoorTrigger(id);
        }
    }

    public void ChampSelected()
    {
        if (onChamp != null)
        {
            onChamp();
        }
    }

    /*
     Şimdi üst kademeye geçtiğimizde geçen oyuncunun bricklerinin tekrar spawn olmasını istiyorum.
     Ama bunun için tekrar spawn etmek istemiyorum.
     Event ile üst kademedeki platforma koyduğum trigger ile tetikleyeceğim. 
     Ve ilk spawn olan bricklerimin sadece yerleriyle oynayacağım.
     Buna int ile id vericeğim. Çünkü ilerleyen levellerde 2'den fazla kat olabilir.
     */

    public event Action<int> onBricksChange;

    public bool is_blue, is_red, is_green = false;

    public void BrickPositionChanged(int id)
    {
        if (onBricksChange != null)
        {
            onBricksChange(id);
        }
    }


}
