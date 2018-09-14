using System.Collections.Generic;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Items
{
	public class Inventory : IInventory
	{
		#region Private Fields
		
		private readonly Dictionary<ItemType, IItem> _equipped;
		private readonly List<IItem> _equippables;
		private readonly List<IItem> _consumables;
		private readonly List<IItem> _miscellaneous;
		
		#endregion Private Fields

		public Inventory()
		{
			_equipped =  new Dictionary<ItemType, IItem>();
			_equippables = new List<IItem>();
			_consumables = new List<IItem>();
			_miscellaneous = new List<IItem>();
		}

		#region Public Fields

		public IReadOnlyDictionary<ItemType, IItem> Equipped => _equipped;
		public IEnumerable<IItem> Equippables => _equippables;
		public IEnumerable<IItem> Consumables => _consumables;
		public IEnumerable<IItem> Miscellaneous => _miscellaneous;
		
		#endregion Public Fields
		
		#region Public Methods
		
		public void AddItem(IItem pItem)
		{
			switch (pItem.Type)
			{
				case ItemType.Consumable:
					_consumables.Add(pItem);
					break;
				case ItemType.Miscellaneous:
					_miscellaneous.Add(pItem);
					break;
				default:
					_equippables.Add(pItem);
					break;
			}
		}

		public void RemoveItem(IItem pItem)
		{
			switch (pItem.Type)
			{
				case ItemType.Consumable:
					_consumables.Remove(pItem);
					break;
				case ItemType.Miscellaneous:
					_miscellaneous.Remove(pItem);
					break;
				default:
					_equippables.Remove(pItem);
					break;
			}
		}
		
		public bool TryEquipItem(IItem pItem, out IItem pUnequipped)
		{
			if (pItem.Type == ItemType.Consumable || pItem.Type == ItemType.Miscellaneous || !_equippables.Contains(pItem))
			{
				pUnequipped = null;
				return false;
			}

			_equippables.Remove(pItem);
			if (Equipped.TryGetValue(pItem.Type, out var unequipped))
			{
				_equipped.Remove(pItem.Type);
				_equippables.Add(unequipped);
				pUnequipped = unequipped;
				_equipped.Add(pItem.Type, pItem);
			}
			else
			{
				pUnequipped = null;
				_equipped.Add(pItem.Type, pItem);
			}

			return true;
		}

		public bool UnequipItem(IItem pItem)
		{
			if (!_equipped.ContainsValue(pItem))
				return false;

			Equipped.TryGetValue(pItem.Type, out var unequipped);
			_equipped.Remove(pItem.Type);
			_equippables.Add(unequipped);
			return true;
		}

		public bool Contains(IItem pItem)
		{
			switch (pItem.Type)
			{
				case ItemType.Consumable:
					return _consumables.Contains(pItem);
				case ItemType.Miscellaneous:
					return _miscellaneous.Contains(pItem);
				default:
					return _equippables.Contains(pItem);
			}
		}
		
		#endregion Public Methods
	}
}