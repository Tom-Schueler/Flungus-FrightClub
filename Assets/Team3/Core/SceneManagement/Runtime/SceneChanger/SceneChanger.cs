using System.Threading.Tasks;
using UnityEngine;

namespace KekwDetlef.SceneManagement
{
    public abstract class SceneChanger : MonoBehaviour
    {
        protected async Task RetreveKey()
        {
            if (!RuntimeSceneContainer.TryRetreveKey())
            {
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

                RuntimeSceneContainer.OnKeyReturned += TryRetreveAgain;

                void TryRetreveAgain()
                {
                    if (!RuntimeSceneContainer.TryRetreveKey())
                    { return; }

                    RuntimeSceneContainer.OnKeyReturned -= TryRetreveAgain;
                    tcs.SetResult(true);
                }

                await tcs.Task;
            }
        }

        protected void ReturnKey()
        {
            RuntimeSceneContainer.ReturnKey();
        }
    }
}
