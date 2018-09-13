using System.Collections.Generic;
using System.Collections.ObjectModel;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Battle
{
	public class Battlefield : IBattlefield
	{
		#region Private Fields
		
		private readonly List<ICombatant> _protagonists;
		private readonly List<ICombatant> _antagonists;
		private readonly List<ICombatant> _antagonistQueue;

		private IWorld _world;
		
		// TODO: come up with a better way of sending messages to VM
		private string _message;
		
		#endregion Private Fields
		
		public Battlefield(IEnumerable<ICombatant> pProtagonists, IWorld pWorld)
		{
			_protagonists = new List<ICombatant>(pProtagonists);
			_antagonists = new List<ICombatant>();
			_antagonistQueue = new List<ICombatant>();

			Message = string.Empty;
			World = pWorld;
		}

		#region Public Fields
		
		public IReadOnlyList<ICombatant> Protagonists => new ReadOnlyCollection<ICombatant>(_protagonists);
		public IReadOnlyList<ICombatant> Antagonists => new ReadOnlyCollection<ICombatant>(_antagonists);
		
		public string Message
		{
			get {
				var message = _message;
				_message = string.Empty;
				return message;
			}
			private set => _message = value;
		}

		public IWorld World
		{
			get => _world;
			set
			{
				if (_world == value)
					return;
				
				_world = value;
				Flee();
				_antagonistQueue.Clear();
				AddAntagonistToQueue();
			}
		}
		
		#endregion Public Fields
		
		#region Public Methods
		
		public void Tick()
		{
			Message = "";

			ExchangeAttacks();
			ApplyActiveEffects();
			ApplyPendingDamage();
			RemoveInactiveEffects();
			
			// add antagonists in queue to active list
			foreach (var antagonist in _antagonistQueue) {
				_antagonists.Add(antagonist);
			}
			_antagonistQueue.Clear();
		}

		public void Flee()
		{
			string enemyName = null;
			while (Antagonists.Count > 0) {
				enemyName = Antagonists[0].Name;
				_antagonists.RemoveAt(0);
				_antagonistQueue.Add(_world.SpawnEnemy());
			}
			if (enemyName != null) {
				Message += "Fled from " + enemyName + "!\n";
			}
		}
		
		#endregion Public Methods
		
		#region Private Implementation
		
		private void AddAntagonistToQueue()
		{
			var antagonist = _world.SpawnEnemy();
			if (antagonist != null) {
				_antagonistQueue.Add(antagonist);
			}
		}

		private void ExchangeAttacks()
		{
			// all protagonists attack all antagonists
			foreach (var protagonist in Protagonists)
			{
				if (protagonist.Fallen)
				{
					protagonist.AutoHeal();
					if (!protagonist.Fallen)
					{
						AddAntagonistToQueue();
					}
					return;
				}
				
				foreach (var antagonist in Antagonists)
				{
					if (TryAttack(protagonist, antagonist, out int damage))
					{
						Message += protagonist.Name + " attacks " + antagonist.Name + " for "+damage+" damage.\n";
						foreach (var onhit in protagonist.OnHitBuffs) {
							onhit.ApplyToEnemy(antagonist);
							Message += onhit.Name + " applied to " + antagonist.Name + "!\n";
						}
					}
					else
					{
						Message += protagonist.Name + " attacks " + antagonist.Name + " and misses.\n";
					}
					if (TryAttack(antagonist, protagonist, out damage))
					{
						Message += antagonist.Name + " attacks " + protagonist.Name + " for "+damage+" damage.\n";
						foreach (var onhit in antagonist.OnHitBuffs)
						{
							onhit.ApplyToEnemy(protagonist);
							Message += onhit.Name + " applied to " + protagonist.Name + "!\n";
						}
					}
					else
					{
						Message += antagonist.Name + " attacks " + protagonist.Name + " and misses.\n";
					}
				}
			}
		}
		
		private bool TryAttack(ICombatant pAttacker, ICombatant pDefender, out int pDamage)
		{
			if (!Defaults.DidMiss(pAttacker.Accuracy, pDefender.Evasion)) {
				var pendingDamage = pAttacker.PhysicalAttack - pDefender.Defense;
				if (pendingDamage > 0) {
					pDefender.AddPendingDamage(pendingDamage);
				}
				pDamage = pendingDamage;
				return true;
			}
			pDamage = 0;
			return false;
		}
		
		private void ApplyActiveEffects()
		{
			// apply active Effects on all combatants
			foreach (var p in Protagonists)
			{
				foreach (var e in p.ActiveEffects)
				{
					e.ApplyEffect(p);
					Message += e.Name + " applied on " + p.Name + " for " + e.Value + " value!\n";
				}
			}
			foreach (var a in Antagonists)
			{
				foreach (var e in a.ActiveEffects)
				{
					e.ApplyEffect(a);
					Message += e.Name + " applied on " + a.Name+" for " + e.Value + " value!\n";
				}
			}
		}
		
		private void ApplyPendingDamage()
		{
			// apply pending damage to all protagonists
			foreach (var p in Protagonists)
			{
				p.ApplyPendingDamage();
				if (!p.Fallen)
					continue;
				
				Message += p.Name + " has fallen!\n";
				_antagonists.Clear();
			}

			// apply pending damage to all antagonists
			for (var i = 0; i < Antagonists.Count; i++)
			{
				Antagonists[i].ApplyPendingDamage();
				if (!Antagonists[i].Fallen)
					continue;
				
				Message += Antagonists[i].Name + " has been defeated!\n";
				_protagonists.ForEach(p => DefeatCombatant(p, Antagonists[i]));
				_antagonists.RemoveAt(i);
				i--;
				AddAntagonistToQueue();
			}
		}

		private void DefeatCombatant(ICombatant pVictor, ICombatant pDefeated)
		{
			pVictor.AddExperience(pDefeated.ExpReward);
			foreach (var item in pDefeated.Inventory.Equipped.Values)
			{
				pVictor.AddItem(item);
			}
			foreach (var item in pDefeated.Inventory.Equippables)
			{
				pVictor.AddItem(item);
			}
			foreach (var item in pDefeated.Inventory.Consumables)
			{
				pVictor.AddItem(item);
			}
			foreach (var item in pDefeated.Inventory.Miscellaneous)
			{
				pVictor.AddItem(item);
			}
		}

		private void RemoveInactiveEffects()
		{
			var inactive = new List<IEffect>();
			foreach (var protagonist in _protagonists)
			{
				foreach (var buff in protagonist.StatBuffs)
				{
					buff.Decay();
					if (buff.DurationRemaining == 0)
						inactive.Add(buff);
				}
			
				foreach (var buff in protagonist.OnHitBuffs)
				{
					buff.Decay();
					if (buff.DurationRemaining == 0)
						inactive.Add(buff);
				}
			
				foreach (var effect in protagonist.ActiveEffects)
				{
					effect.Decay();
					if (effect.DurationRemaining == 0)
						inactive.Add(effect);
				}

				foreach (var effect in inactive)
				{
					protagonist.RemoveEffect(effect);
				}

				inactive.Clear();
			}

			foreach (var antagonist in _antagonists)
			{
				foreach (var buff in antagonist.StatBuffs)
				{
					buff.Decay();
					if (buff.DurationRemaining == 0)
						inactive.Add(buff);
				}
			
				foreach (var buff in antagonist.OnHitBuffs)
				{
					buff.Decay();
					if (buff.DurationRemaining == 0)
						inactive.Add(buff);
				}
			
				foreach (var effect in antagonist.ActiveEffects)
				{
					effect.Decay();
					if (effect.DurationRemaining == 0)
						inactive.Add(effect);
				}

				foreach (var effect in inactive)
				{
					antagonist.RemoveEffect(effect);
				}

				inactive.Clear();
			}
		}
		
		#endregion Private Implementation
	}
}