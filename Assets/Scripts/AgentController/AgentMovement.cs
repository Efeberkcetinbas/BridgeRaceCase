using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{

    [SerializeField] private GameObject _targetParents;

    public Transform[] ropes;

    private Vector3 targetTransform;
    private Animator animator;

    [HideInInspector]
    public NavMeshAgent agent;

    [SerializeField] private float _radius = 2f;

    //Kendim Ekledim.
    [SerializeField] private int _random_number;

    public AgentCollect agentCollect;

    void Start()
    {

        for (int i = 0; i < _targetParents.transform.childCount; i++)
        {
            agentCollect.targets.Add(_targetParents.transform.GetChild(i).gameObject);
        }

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Running_Anim()
    {
        if (!animator.GetBool("running"))
        {
            animator.SetBool("running", true);
        }
    }

    void FixedUpdate()
    {
        if (!agentCollect.haveTarget && agentCollect.targets.Count > 0)
        {
            ChooseTarget();
        }
    }

    void ChooseTarget()
    {

        //random_number gideceğim köprülerin olasılığı.
        int randomNumber = Random.Range(0, _random_number);

        //10 tane küp aldıysan git Kaç küp alacağını belirliyorsun.
        //collected_Random_Number ile en az kaç tane toplamasını random bir şekilde belirliyorum.

        try
        {
            if (randomNumber == 0 && agentCollect.cubes.Count >= agentCollect.collected_Random_Number)
            {
                int randomRope = Random.Range(0, ropes.Length);
                List<Transform> ropesNonActiveChild = new List<Transform>();
                //Kendi dizdiğimizi almamamız lazım. Bunun için önemli.
                foreach (Transform bricks_item in ropes[randomRope])
                {
                    //Daha önce brick konulmadıysa veya konulup ve tagi aynı değilse ekleme.
                    if (!bricks_item.GetComponent<MeshRenderer>().enabled || bricks_item.GetComponent<MeshRenderer>().enabled && bricks_item.gameObject.tag != "Set" + transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1))
                    {
                        //Aynı küp değilse ekle diyoruz.
                        ropesNonActiveChild.Add(bricks_item);
                    }
                }

                //Son bricke gelmek için
                targetTransform = agentCollect.cubes.Count > ropesNonActiveChild.Count ? ropesNonActiveChild[ropesNonActiveChild.Count - 1].position : ropesNonActiveChild[agentCollect.cubes.Count].position;
            }

            else
            {
                //Bütün Colliderları alıyor ama ben bunu istemiyorum.
                Collider[] hitCollide = Physics.OverlapSphere(transform.position, _radius);
                List<Vector3> colors = new List<Vector3>();


                for (int i = 0; i < hitCollide.Length; i++)
                {

                    /*
                     Çarptığımız bütün colliderlar yerine materialin ilk harfini almasını istiyorum.
                     AgentPlayer - Visuals(0) - Running(0) - Whiteman(material.name=G)(1) 
                     Ileride collection işleminde bu material name ilk harfi if döngüsü için önemli.
                     */
                    if (hitCollide.Length >= 1)
                    {
                        if (hitCollide[i].tag.StartsWith(transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0, 1)))
                        {
                            colors.Add(hitCollide[i].transform.position);
                        }
                    }
                }

                if (colors.Count > 0)
                {
                    //Eğer radius içinde color varsa agent hedefi o renk küp.
                    targetTransform = colors[0];
                }
                else
                {
                    //Eğer bulmadıysa rastgele bir şekilde etrafına ilerleyecek.
                    int random = Random.Range(0, agentCollect.targets.Count);
                    targetTransform = agentCollect.targets[random].transform.position;
                }

            }
        }

        catch
        {
            agentCollect.haveTarget = false;
            Debug.Log("Index Out Of Range ERROR!");
        }
        

        //agent'a yolunu çizdik. Hedef toplamadan önce true.
        agent.SetDestination(targetTransform);
        Running_Anim();
        agentCollect.haveTarget = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
