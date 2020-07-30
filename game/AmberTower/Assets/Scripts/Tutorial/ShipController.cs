using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float units;
    [SerializeField] private float unitDamage;
    [SerializeField] private float regenerationRatio; // show how many units are added by second
    [SerializeField] private float shootSpeed;
    [SerializeField] private float unitVelocityMovement;
    [SerializeField] private Color shipColor;
    [SerializeField] private Color unitColor;
    [SerializeField] private GameObject unit;
    [SerializeField] private GameObject score;
    private TextMeshProUGUI unitIndicatorTxt;

    private void Start() {
        unitIndicatorTxt = this.gameObject.transform.Find("PowerIndicatorTxt").GetComponent<TextMeshProUGUI>();
        this.gameObject.GetComponent<Image>().color = shipColor;
    }

    private void Update() {
        units += Time.deltaTime * regenerationRatio;
        unitIndicatorTxt.text = "" + ((int)(units));
    }

    public float GetUnits() {
        return units;
    }

    public IEnumerator StartCombat(GameObject target) {
        ShipController targetController = target.GetComponent<ShipController>();
        while(targetController.GetUnits() > 0) {
            yield return new WaitForSeconds(shootSpeed);
            Shoot(target);
        }
        if (targetController.GetUnits() <= 0) {
            GameObject.Find("CombatManager").GetComponent<CombatManager>().ConquerShip(this.gameObject, target);
        }
    }

    private void Shoot(GameObject target) {
        GameObject soldier;
        Vector3 lookDirection = target.transform.position - this.gameObject.transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
        soldier = Instantiate(unit, this.transform.position, rotation);
        soldier.transform.SetParent(this.transform);
        soldier.GetComponent<Image>().color = unitColor;
        soldier.GetComponent<UnitController>().SetTarget(target);
        soldier.GetComponent<Rigidbody2D>().velocity = soldier.transform.up * unitVelocityMovement;
        units--;
    }

    public float GetUnitDamage() {
        return unitDamage;
    }

    public void SetImpact(float damage, GameObject originShip) {
        this.units -= damage;
        if (originShip.tag == "Player") {
            score.GetComponent<ScoreModel>().AddScore(ScoreModel.SCORE_HIT_SHIP);
        }
    }

    public void SetShipColor(Color color) {
        shipColor = color;
        this.gameObject.GetComponent<Image>().color = shipColor;
    }

    public void SetUnitColor(Color color) {
        unitColor = color;
    }

    public Color GetShipColor() {
        return shipColor;
    }

    public Color GetUnitColor() {
        return unitColor;
    }
}
