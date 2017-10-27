using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Battle {
	public class Combatant {

		public Character Character { get; private set; }
		public int Level { get; private set; }
		public int Experience { get; private set; }
		private int _expReward;
		public Inventory Inventory { get; private set; }

		private int _pendingDamage;

		private bool _fallen;
		
		/// <summary>
		/// DO NOT CALL DIRECTLY. Use CombatantFactory.
		/// </summary>
		public Combatant(Character character, int level, int expReward) {

			Character = character ?? throw new ArgumentException();
			if (level < 1) throw new ArgumentException();
			Level = level;
			_expReward = expReward;

			Inventory = new Inventory(new List<Equipment>(), new List<Equipment>(), new List<Item>());
			Fallen = false;
		}
		
		public void AutoHeal() {
			Character.Heal(1);
			if (Character.CurrentHealth == Character.MaxHealth) {
				Fallen = false;
			}
		}

		public void AttackCombatant(Combatant antagonist) {
			int pendingDamage = Character.PhysicalAttack - antagonist.Character.Defense;
			if (pendingDamage > 0) {
				antagonist._pendingDamage += pendingDamage;
			}
		}

		public void DefeatCombatant(Combatant antagonist) {
			AddExperience(antagonist._expReward);
			Inventory.Equippables.AddRange(antagonist.Inventory.Equipped.Values);
			Inventory.Equippables.AddRange(antagonist.Inventory.Equippables);
			Inventory.Consumables.AddRange(antagonist.Inventory.Consumables);
			Inventory.Miscellaneous.AddRange(antagonist.Inventory.Miscellaneous);
		}

		public void AddExperience(int experience) {
			Experience += experience;
			if (Experience >= Defaults.NextLevelExpFormula(Level)) {
				Experience %= Defaults.NextLevelExpFormula(Level);
				Level++;
			}
		}

		public void ApplyPendingDamage() {
			Character.ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
			if (Character.CurrentHealth == 0) {
				Fallen = true;
			}
		}

#region Equipment

		public void Equip(Equipment equipment) {
			Inventory.Equip(equipment, out List<ABuff> equip, out List<ABuff> unequip);
			foreach (ABuff buff in equip) {
				Character.AddBuff(buff);
			}

			foreach (ABuff buff in unequip) {
				Character.RemoveBuff(buff);
			}
		}

		public void Unequip(Equipment equipment) {
			Inventory.Unequip(equipment, out List<ABuff> unequip);
			foreach (ABuff buff in unequip) {
				Character.RemoveBuff(buff);
			}
		}

#endregion

#region Getters&Setters

		public bool Fallen {
			get {
				return _fallen;
			}
			private set {
				if (_fallen != value) {
					_fallen = value;
				}
			}
		}

#endregion

	}
}