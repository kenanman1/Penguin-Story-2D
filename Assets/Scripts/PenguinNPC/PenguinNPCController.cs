using UnityEngine;

public class PenguinNPCController : MonoBehaviour
{
    public float attackTimer = 4f;

    private PenguinNPCAnimantion penguinAnimantion;
    private PenguinNPCAttack penguinNPCAttack;
    private NPCMovenment NPCMovenment;

    private void Awake()
    {
        penguinAnimantion = GetComponent<PenguinNPCAnimantion>();
        penguinNPCAttack = GetComponent<PenguinNPCAttack>();
        NPCMovenment = GetComponent<NPCMovenment>();
    }

    public void Attack()
    {
        NPCMovenment.StopMovement();
        penguinAnimantion.Attack(attackTimer);
        Invoke("NotAttack", attackTimer);
    }

    public void NotAttack()
    {
        NPCMovenment.ResumeMovement();
        GetComponent<PenguinNPCAttack>().AttackCompleted();
    }
}
