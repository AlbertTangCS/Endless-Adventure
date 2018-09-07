using System.Collections.Generic;
using System.Collections.ObjectModel;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Battle
{
	public class Battlefield : IBattlefield
	{
		private readonly List<Combatant> _antagonistQueue;

		public Battlefield(IEnumerable<ICombatant> pProtagonists, IWorld pWorld)
		{
			_protagonists = new List<ICombatant>(pProtagonists);
			_antagonists = new List<ICombatant>();
			_antagonistQueue = new List<Combatant>();

			Message = string.Empty;
			World = pWorld;
		}

		private readonly List<ICombatant> _protagonists;
		public IReadOnlyList<ICombatant> Protagonists => new ReadOnlyCollection<ICombatant>(_protagonists);
		
		private readonly List<ICombatant> _antagonists;
		public IReadOnlyList<ICombatant> Antagonists => new ReadOnlyCollection<ICombatant>(_antagonists);
		
		private string _message;
		public string Message
		{
			get {
				var message = _message;
				_message = string.Empty;
				return message;
			}
			private set => _message = value;
		}

		private IWorld _world;
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
		
		public void Tick()
		{
			Message = "";

			ExchangeAttacks();
			ApplyActiveEffects();
			ApplyPendingDamage();
			
			// add antagonists in queue to active list
			foreach (var antagonist in _antagonistQueue) {
				_antagonists.Add(antagonist);
			}
			_antagonistQueue.Clear();
		}

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
					if (protagonist.TryAttack(antagonist, out int damage))
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
					if (antagonist.TryAttack(protagonist, out damage))
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
		
		private void ApplyActiveEffects()
		{
			// apply active Effects on all combatants
			foreach (var p in Protagonists)
			{
				var decayedEffects = new List<AEffect>();
				foreach (AEffect e in p.ActiveEffects)
				{
					e.ApplyEffect(p);
					e.Decay();
					if (e.DurationRemaining <= 0)
						decayedEffects.Add(e);
					Message += e.Name + " applied on " + p.Name + " for " + e.Value + " value!\n";
				}
				foreach (var effect in decayedEffects)
				{
					p.RemoveEffect(effect);
					Message += effect + " removed from " + p.Name+ ".\n";
				}
			}
			foreach (var a in Antagonists)
			{
				var decayedEffects = new List<AEffect>();
				foreach (var e in a.ActiveEffects)
				{
					e.ApplyEffect(a);
					e.Decay();
					if (e.DurationRemaining <= 0)
						decayedEffects.Add(e);
					Message += e.Name + " applied on " + a.Name+" for " + e.Value + " value!\n";
				}
				foreach (var effect in decayedEffects)
				{
					a.RemoveEffect(effect);
					Message += effect + " removed from " + a.Name + ".\n";
				}
			}
		}
		
		private void ApplyPendingDamage()
		{
			// apply pending damage to all protagonists
			foreach (var p in Protagonists) {
				p.ApplyPendingDamage();
				if (p.Fallen) {
					Message += p.Name + " has fallen!\n";
					_antagonists.Clear();
				}
			}

			// apply pending damage to all antagonists
			for (var i = 0; i < Antagonists.Count; i++) {
				Antagonists[i].ApplyPendingDamage();
				if (Antagonists[i].Fallen) {
					Message += Antagonists[i].Name + " has been defeated!\n";
					_protagonists.ForEach(p => p.DefeatCombatant(Antagonists[i]));
					_antagonists.RemoveAt(i);
					i--;
					AddAntagonistToQueue();
				}
			}
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
	}
}