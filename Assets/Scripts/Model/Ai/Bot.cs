using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
	public sealed class Bot : BaseObjectScene
	{
		public float Hp = 100;
		public Transform Target { get; set; }
		public Vision Vision;
		public Weapon Weapon; // с разным оружием 
		public NavMeshAgent Agent { get; private set; }

		private float _waitTime = 3;
		private StateBot _stateBot;
		private Vector3 _point;


        protected override void Awake()
		{
			base.Awake();
			Agent = GetComponent<NavMeshAgent>();
			var bodyBot = GetComponentInChildren<BodyBot>();
			if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

			var headBot = GetComponentInChildren<HeadBot>();
			if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
		}

		public void Tick()
		{
			Debug.Log(_stateBot);
			if (_stateBot == StateBot.Died) return;

			if (_stateBot != StateBot.Detected)
			{
                
                if (!Agent.hasPath)
				{
					if (_stateBot != StateBot.Inspection)
					{
						if (_stateBot != StateBot.Patrol)
						{
							_stateBot = StateBot.Patrol;
							_point = Patrol.GenericPoint(transform);
							Agent.SetDestination(_point);
							Agent.stoppingDistance = 0;
                            
                        }
						else
						{
							if (Vector3.Distance(_point, transform.position) <= 1)
							{
								_stateBot = StateBot.Inspection;
								Invoke(nameof(ReadyPatrol), _waitTime);
							}
						}
					}
				}

				if (Vision.VisionM(transform, Target))
				{
					_stateBot = StateBot.Detected;
				}
			}
			else
			{
                Debug.Log("Vision: " + Vision.VisionM(transform, Target));
                if (Vision.VisionM(transform, Target))
                {
                    Agent.stoppingDistance = 6;
                    Agent.SetDestination(Target.position);
                    this.transform.LookAt(Target.transform);
                    // Остановиться
                    Weapon.Fire();
                    if (Vector3.Distance(this.transform.position, Target.position) < 6)
                        Agent.ResetPath();
                }
                else
                {
                    if (Vector3.Distance(Target.position, transform.position) > 15)
                    {
                        Debug.Log("Бля");
                        _stateBot = StateBot.Inspection;
                        Invoke(nameof(ReadyPatrol), _waitTime);
                    }
                }
            }
        }

		public void SetDamage(InfoCollision info)
		{
			if (Hp > 0)
			{
				Hp -= info.Damage;
			}

			if (Hp <= 0)
			{
				_stateBot = StateBot.Died;
				Agent.enabled = false;
				foreach (var child in GetComponentsInChildren<Transform>())
				{
					child.parent = null;
					var tempRbChild = child.GetComponent<Rigidbody>();
					if (!tempRbChild)
					{
						tempRbChild = child.gameObject.AddComponent<Rigidbody>();
					}
					//tempRbChild.AddForce(info.Dir * Random.Range(10, 300));

					Main.Instance.BotController.RemoveBotToList(this);
					Destroy(child.gameObject, 10);
				}
			}
		}

		private void ReadyPatrol()
		{
			_stateBot = StateBot.Non;
            Agent.ResetPath();
        }

		public void MovePoint(Vector3 point)
		{
			Agent.SetDestination(point);
		}
	}
}