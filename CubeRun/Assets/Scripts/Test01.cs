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
        //    AnimatorManager.Instance.PlayAnim(anim, 1f);//一倍速播放
        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, 2f);//二倍速播放
        //}
        //else if (Input.GetKeyDown(KeyCode.C))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, -1f);//一倍速倒放
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    AnimatorManager.Instance.PlayAnim(anim, -2f);//二倍速倒放
        //}
        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AnimatorManager.Instance.StopAnim(anim);//停止anim动画
        //}
    }
}
