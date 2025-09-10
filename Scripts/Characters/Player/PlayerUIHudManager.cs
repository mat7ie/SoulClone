using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace SG
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UI_statBar healthBar;

        public void RefreshHUD()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);
        }

        public void SetNewHealthValue(float oldValue, float newValue)
        {
            healthBar.SetStat(Mathf.RoundToInt(newValue));
        }

        public void SetMaxHealthValue(int maxHealth)
        {
            healthBar.SetMaxStat(maxHealth);
        }
    }
}
