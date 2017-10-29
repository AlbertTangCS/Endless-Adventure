using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Effects {
	public class ArmorBuff : ABuff {
		
		public ArmorBuff(double value) : base("Armor Buff", StatType.Defense, value) { }

		public override int Apply(int stat) {
			return stat + (int)Value;
		}

		public override int Unapply(int stat) {
			return stat - (int)Value;
		}
	}
}