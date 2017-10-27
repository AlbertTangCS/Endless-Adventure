using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Effects {
	public class ArmorBuff : ABuff {
		
		public ArmorBuff(string name, double value) : base(name, StatType.Defense, value) { }

		public override int Apply(int stat) {
			return stat + (int)Value;
		}

		public override int Unapply(int stat) {
			return stat - (int)Value;
		}
	}
}