namespace Geekbrains
{
	public sealed class Bullet : Ammunition
	{
		private void OnCollisionEnter(UnityEngine.Collision collision)
		{
			var tempObj = collision.gameObject.GetComponent<ISetDamage>();
			// дописать доп урон
			tempObj?.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
			Destroy(gameObject);
		}
	}
}