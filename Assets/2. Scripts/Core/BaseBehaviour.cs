using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public abstract class BaseBehaviour : MonoBehaviour
    {

        #region Log Methods
        protected void LogError(string msg)
        {
            Debug.LogErrorFormat("Cena: {0} | GameObject: {2} => {1}", SceneManager.GetActiveScene().name, msg, gameObject.name);
        }

        protected void LogWarning(string msg)
        {
            Debug.LogWarningFormat("Cena: {0} | GameObject: {2} => {1}", SceneManager.GetActiveScene().name, msg, gameObject.name);
        }

        protected void Log(string msg)
        {
            Debug.LogFormat("Cena: {0} | GameObject: {2} => {1}", SceneManager.GetActiveScene().name, msg, gameObject.name);
        }
        #endregion

    }

}