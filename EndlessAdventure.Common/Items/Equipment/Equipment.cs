using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items {

	public class Equipment : Item {

		public List<ABuff> Buffs { get; private set; }

		public Equipment(string name,
										 ItemType type,
										 int cost,
										 string description,
										 List<ABuff> buffs) : base(name, type, cost, description) {

			Buffs = buffs ?? throw new ArgumentException();
		}

	}
}