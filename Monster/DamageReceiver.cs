using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour {

    [SerializeField] private Monster monster;

    void OnCollisionEnter(Collision col)
    {
        SpellsPrefab magic = col.transform.GetComponent<SpellsPrefab>();
        if (magic != null)
        {
            TextMesh go = Instantiate(Resources.Load<TextMesh>("Prefabs/damageNum"), col.contacts[0].point, Quaternion.identity) as TextMesh;
            go.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
            //go.text = "-" + magic.damage;
            Destroy(go.gameObject, 0.3f);
            monster.actions.InvokeOnHitBySpells(magic.spells);
        }
    }
}
