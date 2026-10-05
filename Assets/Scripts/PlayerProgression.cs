using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    float HP;
    float MP;
    float EXP;

    InventoryManager inv;

    void Start()
    {
        inv = GetComponent<InventoryManager>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
    }
}
