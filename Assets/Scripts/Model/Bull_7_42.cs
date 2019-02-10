namespace Geekbrains
{
	public sealed class Bull_7_42 : Ammunition
	{
		private void OnCollisionEnter(UnityEngine.Collision collision)
		{
			var tempObj = collision.gameObject.GetComponent<ISetDamage>();
            float damage = _curDamage + Main.random.Next(-5, 15);
            tempObj?.SetDamage(new InfoCollision(damage, Rigidbody.velocity));
			Destroy(gameObject);
		}
	}
}