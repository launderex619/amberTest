using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject[] ships;
    [SerializeField] private GameObject endScreen;

    public void ShowPossibleTargets() {
        for (int i = 0; i < ships.Length; i++) {
            ships[i].transform.Find("Target").gameObject.SetActive(true);
        }
        actionPanel.GetComponent<PanelManager>().Hide();
    }

    public void Attack(GameObject target)
    {
        GameObject attacker = actionPanel.GetComponent<PanelManager>().GetCaller();
        for (int i = 0; i < ships.Length; i++) {
            ships[i].transform.Find("Target").gameObject.SetActive(false);
        }
        StartCoroutine(attacker.GetComponent<ShipController>().StartCombat(target));
    }

    public void ConquerShip(GameObject attacker, GameObject target) {
        ShipController attackerController = attacker.GetComponent<ShipController>();
        ShipController targetController = target.GetComponent<ShipController>();
        targetController.SetShipColor(attackerController.GetShipColor());
        targetController.SetUnitColor(attackerController.GetUnitColor());
        target.tag = attacker.tag;
        if (IsGameOver()) {
            FinishLevel();
        }
    }

    public bool IsGameOver() {
        for (int i = 0; i < ships.Length; i++) {
            if (ships[i].tag == "Enemy") {
                return false;
            }
        }
        return true;
    }

    public void FinishLevel() {
        endScreen.GetComponent<EndScreenController>().Finish();
        endScreen.SetActive(true);
    }
}
