using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairsGenerateManager : MonoBehaviour
{
    public int id;

    [SerializeField] private float x, y, z;

    //Listeyle de tutulabilir.
    [SerializeField] private GameObject Blue_Bricks, Red_Bricks, Green_Bricks;

    void Start()
    {
        GameEvents.gameEvents.onBricksChange += Change_Bricks;
    }


    private void Change_Bricks(int id)
    {
        if (id == this.id)
        {
            if (GameEvents.gameEvents.is_blue)
            {
                Blue_Bricks.transform.DOMove(new Vector3(x,y,z),.2f);
            }

            else if (GameEvents.gameEvents.is_red)
            {
                //Üst üste binmesini gereksiz yere kontrol etmemek için.
                Red_Bricks.transform.DOMove(new Vector3(x + 2, y, z), .2f);
            }

            else if (GameEvents.gameEvents.is_green)
            {
                Green_Bricks.transform.DOMove(new Vector3(x + 1, y, z), .2f);
            }
        }
    }

    void OnDestroy()
    {
        GameEvents.gameEvents.onBricksChange -= Change_Bricks;
    }

    
}
