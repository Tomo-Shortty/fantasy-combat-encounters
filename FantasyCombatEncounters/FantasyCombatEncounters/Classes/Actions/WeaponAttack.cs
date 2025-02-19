﻿using FantasyCombatEncounters.Classes.Combatants;
using FantasyCombatEncounters.Classes.Types;
using FantasyCombatEncounters.Classes.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyCombatEncounters.Classes.Actions
{
    internal class WeaponAttack : IAction
    {
        private bool _advantage;
        private bool _disadvantage;
        private int _hitResult;

        public WeaponAttack(string name, ActionType type)
        {
            Name = name;
            Type = type;
            Message = "";
            _advantage = false;
            _disadvantage = false;
        }

        public string Name { get; set; }
        public ActionType Type { get; set; }
        public string Message { get; set; }
        public bool Advantage => _advantage;
        public bool Disadvantage => _disadvantage;
        public int HitResult => _hitResult;

        public void Attack(IWeapon weapon, ICombatant attacker, ICombatant defender)
        {
            Random random = new Random();
            int diceRoll;
            if (_advantage == true && _disadvantage == false)
            {
                diceRoll = random.Next(0, 21) + 5;
            }
            else if (_advantage == false && _disadvantage == true)
            {
                diceRoll = random.Next(0, 21) - 5;
            }
            else
            {
                diceRoll = random.Next(0, 21);
            }
            _hitResult = diceRoll + weapon.AttackBonus;
            if (diceRoll == 1 || _hitResult < defender.Armour)
            {
                Message = weapon.Name + " attack missed!";
            }
            else if (diceRoll == 20)
            {
                if (weapon.SecondDamage > 0)
                {
                    Message = attacker.Name + " " + weapon.Name.ToLower() + " attack scored a critical hit! " + defender.Name + " has taken " +
                    defender.DamageJustTaken.ToString() + " " + weapon.DamageType.ToString().ToLower() + " damage and " + 
                    defender.SecondDamageJustTaken.ToString() + " " + weapon.SecondDamageType.ToString().ToLower() + " damage!";
                    defender.TakeDamage(weapon.Damage, weapon.SecondDamage, 2);
                } 
                else
                {
                    Message = attacker.Name + " " + weapon.Name.ToLower() + " attack scored a critical hit! " + defender.Name + " has taken " +
                    defender.DamageJustTaken.ToString() + " " + weapon.DamageType.ToString().ToLower() + " damage!";
                    defender.TakeDamage(weapon.Damage, weapon.SecondDamage, 2);
                }
            }
            else
            {               
                if (weapon.SecondDamage > 0)
                {
                    Message = attacker.Name + " " + weapon.Name.ToLower() + " attack hits! " + defender.Name + " has taken " +
                    defender.DamageJustTaken.ToString() + " " + weapon.DamageType.ToString().ToLower() + " damage and " + 
                    defender.SecondDamageJustTaken.ToString() + " " + weapon.SecondDamageType.ToString().ToLower() + " damage!";
                    defender.TakeDamage(weapon.Damage, weapon.SecondDamage, 1);
                }
                else
                {
                    Message = attacker.Name + " " + weapon.Name.ToLower() + " attack hits! " + defender.Name + " has taken " +
                    defender.DamageJustTaken.ToString() + " " + weapon.DamageType.ToString().ToLower() + " damage!";
                    defender.TakeDamage(weapon.Damage, weapon.SecondDamage, 1);
                }
            }
        }
    }
}
