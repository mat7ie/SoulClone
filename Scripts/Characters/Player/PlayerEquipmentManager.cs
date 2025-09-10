using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        public override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
            InitializeWeaponSlot();
        }

        public override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }

        private void InitializeWeaponSlot()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                if(weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandSlot = weaponSlot;
                }
                else if(weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
                {
                    leftHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        // RIGHT WEAPON
        public void SwitchRightWeapon()
        {
            
        }
        
        public void LoadRightWeapon()
        {
            if(player.playerInventoryManager.currentRightHandWeapon != null)
            {
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);
            }
        }


        // LEFT WEAPON
        public void SwitchLeftWeapon()
        {
            
        }

        public void LoadLeftWeapon()
        {
            if(player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);
            }
        }
    }
}
