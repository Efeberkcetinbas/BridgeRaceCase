using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public enum Ai_Agent
{
    red = 0,
    green = 2
}


public class AgentCollect : MonoBehaviour
{


    public List<GameObject> targets = new List<GameObject>();
    public List<GameObject> cubes = new List<GameObject>();

    public bool haveTarget = false;

    [Header("At Least Collect")]
    public int collected_Random_Number = 5;
    //[SerializeField] private int collected_max, collected_min;

    public Transform finalPoint;

    [Header("Collection")]
    [SerializeField] private Transform _collectedObjects;
    [SerializeField] private GameObject _prevObject;


    public Ai_Agent aisEnum;

    int multiplier = 0;

    

    private void OnTriggerEnter(Collider other)
    {

        /*Kod için uygun bir yöntem değil. Ama görünüş uygunluğu açısından agent orada durmalı. 
        Aksi halde tekrar target'a döner.
        Bunu önlemek için Generatebricks'te generate konumları değiştirilip hedef stair ve target küpler ile oynanabilir.
         */
        if (other.gameObject.CompareTag("Final"))
        {
            gameObject.transform.position = new Vector3(-6.9f, 6.58f, 39.68f);
            GameEvents.gameEvents.is_green = true;
            GameEvents.gameEvents.is_red = true;
            FindObjectOfType<AgentMovement>().agent.SetDestination(finalPoint.transform.position);
        }

        //Optimizasyon için SkinnedMeshRenderer değişken içinde tutulabilir.
        if (other.gameObject.tag.StartsWith(transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1)))
        {
            //Toplanacak olan brickleri içine parent altında topluyorum.
            other.transform.SetParent(_collectedObjects);
            //Bir önceki küpün konumunu alıyorum ve üstüne ekleyerek gidiyorum. Child olduğu için localPosition.
            Vector3 pos = _prevObject.transform.localPosition;


            multiplier += 1;

            //pos.y += 0.22f;
            pos.y += 0.22f * multiplier;
            pos.z = 0;
            pos.x = 0;
            //Quaternion sınıfının 4.bir açı mantığı olduğu için, hesaplamalarda daha net sonuçlar alabilmek için tercih edilir.
            //Üst üste binen küplerin düz bir açı oluşturmasını sağlıyor.
            other.transform.localRotation = new Quaternion(0, 0.7f, 0, 0.7f);
            other.transform.DOLocalMove(pos, 0.2f);

            //Missing Reference Null Hatası bazı durumlarda
            //prevObject = other.gameObject;

            cubes.Add(other.gameObject);

            //Topladığımız küpün hedef olmasına artık gerek yok.
            //Eğer burada hata alırsam radius'u arttırabilir ve non-negative hatası olursa çözümleyebilirim.
            targets.Remove(other.gameObject);
            //Colliderlar arası çarpışmayı önlemek için.
            other.tag = "Untagged";
            haveTarget = false;

            //this
            GenerateBricks.instance.GenerateCube((int)aisEnum, this);
        }

        //Merdivenlere dizmek için gerekli şartlar.
        else if (other.gameObject.tag == "SetB" || other.gameObject.tag != "Set" + transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1) && other.gameObject.tag.StartsWith("Set"))
        {
            //1 tane küp player'da kalıyor.
            if (cubes.Count > 1)
            {
                GameObject obje = cubes[cubes.Count - 1];
                cubes.RemoveAt(cubes.Count - 1);
                Destroy(obje);

                //Önceden tanımlanmış bricklerin rendererini açıp karakterin materyalini veriyorum.
                //Topladığımızın scale'i üzerinde oynayıpda da yapılabilirdi.
                other.GetComponent<MeshRenderer>().material = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                other.GetComponent<MeshRenderer>().enabled = true;

                other.tag = "Set" + transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1);
                multiplier = 0;

            }


            else
            {
                
                //collected_Random_Number = Random.Range(collected_min, collected_max);
                _prevObject = cubes[0].gameObject;
                haveTarget = false;
            }
        }

    }
}
