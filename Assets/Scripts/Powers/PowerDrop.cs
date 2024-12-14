using UnityEngine;

public class PowerDrop : MonoBehaviour
{
    [System.Serializable]
    public class PowerType
    {
        public Power power;
        public float dropProbability;
    }
    [SerializeField] private PowerType[] powers;
    public void TryDropPower()
    {
        for (int i = 0; i < powers.Length; i++)
        {
            if (Random.value < powers[i].dropProbability)
            {
                Power powerToDrop = powers[i].power;
                Instantiate(powerToDrop, transform.position, Quaternion.identity);
            }

        }
    }
}
