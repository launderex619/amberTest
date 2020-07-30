using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        // if impacts to ship
        if (distance < 10 && distance > -10) {
            GameObject parent = this.gameObject.transform.parent.gameObject;
            float damage = parent.GetComponent<ShipController>().GetUnitDamage();
            target.GetComponent<ShipController>().SetImpact(damage, parent);
            this.transform.position = Vector3.zero;
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }
}
