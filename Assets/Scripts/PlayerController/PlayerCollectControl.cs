using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollectControl : MonoBehaviour
{

    [SerializeField] private Transform collectedObjects;
    [SerializeField] private GameObject prevBrick;
    [SerializeField] private List<GameObject> bricks = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        //Brickleri küpleri topluyoruz.
        if (other.gameObject.tag.StartsWith(transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1)))
        {
            other.transform.SetParent(collectedObjects);
            Vector3 pos = prevBrick.transform.localPosition;

            //Sound
            AudioManagement.audioM.Play("collect");

            pos.y += 0.22f;
            pos.z = 0;
            pos.x = 0;
            //Quaternion sınıfının 4.bir açı mantığı olduğu için, hesaplamalarda daha net sonuçlar alabilmek için tercih edilir.
            other.transform.localRotation = new Quaternion(0, 0.7f, 0, 0.7f);
            other.transform.DOLocalMove(pos, 0.2f);
            prevBrick = other.gameObject;
            bricks.Add(other.gameObject);

            other.tag = "Untagged";

            GenerateBricks.instance.GenerateCube(1);
        }


       
        //Merdivene diziyoruz.
        //Aynı renk varsa veya set ise üzerinden geçebiliyoruz şu an. SetRed ve SetGreen eklersen onlardan da geçebilirsin.
        if (bricks.Count > 1 && other.gameObject.tag == "SetR" || bricks.Count > 1 && other.gameObject.tag != "Set" + transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1) && other.gameObject.tag.StartsWith("Set"))
        {
            GameObject obje = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            Destroy(obje);

            //Sound

            AudioManagement.audioM.Play("put");

            //AgentCollect'te açıkladığım gibi burası da.
            other.GetComponent<MeshRenderer>().material = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            other.GetComponent<MeshRenderer>().enabled = true;

            other.tag = "Set" + transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1);

            prevBrick = bricks[bricks.Count - 1];
        }

        //Eğer brick yoksa veya farklı renkte bir brick ise elimizde koyacağımız küp olmadığı taktirde ilerleyemiyoruz.
        if (bricks.Count == 1 && other.gameObject.tag=="Set" || other.gameObject.tag == "SetR" || other.gameObject.tag == "SetG" )
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        }
      
    }
}
