using Unity.Netcode;
using UnityEngine;
using TMPro;
using Team3.Weapons;
public class DisplayDamage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private float force;
    [SerializeField]
    Rigidbody rb;
  

   
        
    private void OnEnable()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce( new Vector3(Random.Range(-1f,1f),1f,Random.Range(-1f,1f)).normalized*force,ForceMode.Impulse );
        Destroy(gameObject, 3f);
    }

    public void Update()
    {
     
    }
    public void SetDamageText(float text, DamageType type)
    {
        this.text.SetText(text.ToString());

        switch (type)
        {
          
            case DamageType.Fire:
                this.text.color = Color.red;
                break;
            case DamageType.Ice:
                this.text.color = Color.green;
                break;
            case DamageType.Water:
                this.text.color = Color.blue;
                break;
            default:
                this.text.color = Color.white;
                break;
        }
    }
}
