using UnityEngine;
using System.Linq;

namespace Team3.MOBA
{
    public class PooledVFX : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private float lifetime = 1.5f;

        private void OnEnable()
        {
            Invoke(nameof(ReleaseToPool), lifetime);
        }

        private void ReleaseToPool()
        {
            gameObject.layer = 2;
            var children = gameObject.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
            children.ForEach(c => c.layer = 2);


            ObjectPoolManager.Instance.ReturnObject(id, gameObject);
        }

        private void OnDisable()
        {
            gameObject.layer = 2;
            var children = gameObject.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
            children.ForEach(c => c.layer = 2);

            CancelInvoke(); // falls Objekt manuell deaktiviert wurde
        }
    }

}