using System;
using UnityEngine;

namespace Geekbrains
{
	public class Aim : MonoBehaviour, ISetDamage
	{
		public event Action OnPointChange;

		public float Hp = 100;
        public float Shield = 20;

        private System.Random rand = new System.Random();
		private bool _isDead;
		// дописать поглащение урона
		public void SetDamage(InfoCollision info)
		{
			if (_isDead) return;
			if (Hp > 0)
			{
				Hp -= info.Damage * Shield * (float)rand.NextDouble();
			}

			if (Hp <= 0)
			{
				var tempRigidbody = GetComponent<Rigidbody>();
				if (!tempRigidbody)
				{
					tempRigidbody = gameObject.AddComponent<Rigidbody>();
				}
				tempRigidbody.velocity = info.Dir;
				Destroy(gameObject, 10);

				OnPointChange?.Invoke();
				_isDead = true;
			}
		}
	}
}