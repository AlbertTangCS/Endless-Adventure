using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Items {
	public class Item {
		public string Name { get; private set; }
		public string Description { get; private set; }
		public ItemType Type { get; private set; }
		public int Cost { get; private set; }

		// for Consumables and Equippables
		public List<AStatBuff> Buffs { get; private set; }
		// for Consumables
		public List<AEffect> Effects { get; private set; }

		public Item(ItemData data) {
			Name = data.Name;
			Description = data.Description;
			Type = data.Type;
			Cost = data.Cost;

			if (data.BuffValueDict != null) {
				Buffs = new List<AStatBuff>();
				foreach (string key in data.BuffValueDict.Keys) {
					Buffs.Add(Database.Buffs[key](data.BuffValueDict[key], -1));
				}
			}

			if (data.EffectValueDict != null) {
				Effects = new List<AEffect>();
				foreach (string key in data.EffectValueDict.Keys) {
					Effects.Add(Database.Effects[key](data.EffectValueDict[key], -1));
				}
			}
		}

		public void Equip(Character character) {
			if (Type == ItemType.Consumable || Type == ItemType.Miscellaneous) throw new InvalidOperationException();
			if (Buffs != null) {
				foreach (AStatBuff buff in Buffs) {
					character.AddBuff(buff);
				}
			}
		}

		public void Unequip(Character character) {
			if (Type != ItemType.Consumable || Type == ItemType.Miscellaneous) throw new InvalidOperationException();
			if (Buffs != null) {
				foreach (AStatBuff buff in Buffs) {
					character.RemoveBuff(buff);
				}
			}
		}

		public void Consume(Character character) {
			if (Type != ItemType.Consumable) throw new InvalidOperationException();

			if (Effects != null) {
				foreach (AEffect effect in Effects) {
					effect.ApplyEffect(character);
				}
			}
			if (Buffs != null) {
				foreach (AStatBuff buff in Buffs) {
					character.AddBuff(buff);
				}
			}
		}

	}
}