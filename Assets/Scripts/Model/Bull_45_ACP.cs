namespace Geekbrains
{
    public sealed class Bull_45_ACP : Ammunition
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();
            float damage = _curDamage + Main.random.Next(-10, 10);
            tempObj?.SetDamage(new InfoCollision(damage, collision.contacts[0], collision.transform, Rigidbody.velocity));
            Destroy(gameObject);
        }
    }
}