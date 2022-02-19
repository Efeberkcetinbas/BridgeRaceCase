using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBricks : MonoBehaviour
{
    public static GenerateBricks instance;

    public GameObject redCube, blueCube, greenCube;

    public Transform redCubeParent, greenCubeParent, blueCubeParent;

    public int minX, maxX, minZ, maxZ;

    public LayerMask layerMask;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GenerateCube(int number, AgentCollect agentCollect = null)
    {
        if (number == 0)
            Generate(redCube, redCubeParent, agentCollect);
        else if(number==1)
            Generate(blueCube, blueCubeParent, agentCollect);
        else if (number == 2)
            Generate(greenCube, greenCubeParent, agentCollect);

    }

    private void Generate(GameObject gameObject, Transform parent, AgentCollect agentCollectio = null)
    {
        GameObject g = Instantiate(gameObject);

        g.transform.parent = parent;

        Vector3 desPos = GiveRandomPos();

        g.SetActive(false);

        //üst üste gelmesini istemiyorum.

        Collider[] colliders = Physics.OverlapSphere(desPos, 1, layerMask);
        while (colliders.Length != 0)
        {
            desPos = GiveRandomPos();
            colliders=Physics.OverlapSphere(desPos, 1, layerMask);
        }

        //çarpmadığından emin olduğumuz için artık spawn gözükebilir.

        g.SetActive(true);
        g.transform.position = desPos;

        if (agentCollectio != null)
        {
            agentCollectio.targets.Add(g);
        }
    }

    //Verdiğimiz değerler aralığında random spawnlıyorum.
    private Vector3 GiveRandomPos()
    {
        return new Vector3(Random.Range(minX, maxX), blueCube.transform.position.y, Random.Range(minZ, maxZ));
    }

}
