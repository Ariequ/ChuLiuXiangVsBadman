using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10;
    
    // 使用字符串变量保存当前状态，避免多处引用写错
    private static readonly string IdleState = "Idle";
    private static readonly string MoveState = "Locomotion";
    private static readonly string ATTACK01 = "attack01";
    private static readonly string ATTACK02 = "attack02";
    private static readonly string ATTACK03 = "attack03";
    private static readonly string ATTACK04 = "attack04";
    private static readonly string ATTACK05 = "attack05";

    // 动画状态机参数Key
    private static readonly string ActionCMD = "ActionCMD";
    [HideInInspector]
    public Animator
        animator = null;
    // 当前连击数（即 玩家按下攻击键的次数）
    public int curComboCount = 0;
    private int _needCheckAttack;

    void Awake()
    {
        animator = GetComponent <Animator>();
    }

    void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(IdleState))
        {
            // 每次设置完参数之后，都应该在下一帧开始时将参数设置清空，避免连续切换
            this.animator.SetInteger(ActionCMD, 0);
        }
        
        if (stateInfo.IsName(ATTACK01) && (stateInfo.normalizedTime > 0.8f) && (this.curComboCount == 2))
        {
            // 当在攻击1状态下，并且当前状态运行了0.6正交化时间（即动作时长的60%），并且用户在攻击1状态下又按下了“攻击键”
            this.animator.SetInteger(ActionCMD, 1);
        }
        else if (stateInfo.IsName(ATTACK02) && (stateInfo.normalizedTime > 0.8f) && (this.curComboCount == 3))
        {
            // 挡在攻击2状态下（同理攻击1状态）
            this.animator.SetInteger(ActionCMD, 1);
        }
        else if (stateInfo.IsName(ATTACK03) && (stateInfo.normalizedTime > 0.8f) && (this.curComboCount == 4))
        {
            // 挡在攻击3状态下（同理攻击1状态）
            this.animator.SetInteger(ActionCMD, 1);
        } 
        else if (stateInfo.IsName(ATTACK04) && (stateInfo.normalizedTime > 0.8f) && (this.curComboCount == 5))
        {
            // 挡在攻击3状态下（同理攻击1状态）
            this.animator.SetInteger(ActionCMD, 1);
        } 
        else if (stateInfo.IsName(ATTACK05) && (stateInfo.normalizedTime > 0.8f) && (this.curComboCount == 6))
        {
            // 挡在攻击3状态下（同理攻击1状态）
            this.animator.SetInteger(ActionCMD, 1);
        } 

        if (Input.GetKeyUp(KeyCode.J))
        {
            // 监听用户输入（假设J键为攻击键）
            Attack();
        }
    }

    void Attack()
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(IdleState) || stateInfo.IsName(MoveState))
        {
            // 在待命状态下，按下攻击键，进入攻击1状态，并记录连击数为1
            this.animator.SetInteger(ActionCMD, 1);
            this.curComboCount = 1;
        }
        else if (stateInfo.IsName(ATTACK01))
        {
            // 在攻击1状态下，按下攻击键，记录连击数为2（切换状态在Update()中）
            this.curComboCount = 2;
        }
        else if (stateInfo.IsName(ATTACK02))
        {
            // 在攻击2状态下，按下攻击键，记录连击数为3（切换状态在Update()中）
            this.curComboCount = 3;
        }
        else if (stateInfo.IsName(ATTACK03))
        {
            // 在攻击3状态下，按下攻击键，记录连击数为4（切换状态在Update()中）
            this.curComboCount = 4;
        }
        else if (stateInfo.IsName(ATTACK04))
        {
            // 在攻击3状态下，按下攻击键，记录连击数为4（切换状态在Update()中）
            this.curComboCount = 5;
        }
        else if (stateInfo.IsName(ATTACK05))
        {
            // 在攻击3状态下，按下攻击键，记录连击数为4（切换状态在Update()中）
            this.curComboCount = 6;
        }
    }

    public void SetNeedCheckAttack(int value)
    {
        NeedCheckAttack = value; 
        StartCoroutine("resetNeedCheckAttack");
    }

    IEnumerator resetNeedCheckAttack()
    {
        yield return new WaitForSeconds(0.01f);
        NeedCheckAttack = -1;
    }

    public int NeedCheckAttack
    {
        get
        {
            return _needCheckAttack;
        }
        set
        {
            _needCheckAttack = value; 
        }
    }
}
