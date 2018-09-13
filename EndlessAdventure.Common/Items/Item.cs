using System.Collections.Generic;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Items
{
	public class Item : IItem
	{
		#region Private Fields

		private readonly List<IEffect> _equipEffects;
		
		#endregion Private Fields
		
		public Item(ItemData data)
		{
			Name = data.Name;
			Description = data.Description;
			Type = data.Type;
			Cost = data.Cost;

			_equipEffects = new List<IEffect>();

			/*if (data.BuffValueDict != null) {
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
			}*/
		}

		#region Public Fields
		
		public string Name { get; }
		public string Description { get; }
		public ItemType Type { get; }
		public int Cost { get; }

		public IEnumerable<IEffect> EquipEffects => _equipEffects;

		#endregion Public Fields

		#region Public Methods

		// only for use by factory
		public void AddEquipEffect(IEffect pEquipEffect)
		{
			_equipEffects.Add(pEquipEffect);
		}
		
		#endregion Public Methods
	}
}