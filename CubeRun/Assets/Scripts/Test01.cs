using UnityEngine;

public class Test01 : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, 1f);//һ���ٲ���
        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, 2f);//�����ٲ���
        //}
        //else if (Input.GetKeyDown(KeyCode.C))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, -1f);//һ���ٵ���
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, -2f);//�����ٵ���
        //}
        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AnimatorManager.Instance.StopAnim(anim);//ֹͣanim����
        //}
    }
}
