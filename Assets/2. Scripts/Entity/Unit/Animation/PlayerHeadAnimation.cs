using UnityEngine;
using System.Collections;
using Game.Entity.Unit;
using DG.Tweening;

namespace Game.Entity.Unit.Animation {
    [RequireComponent(typeof(UserInput))]
    public class PlayerHeadAnimation : MonoBehaviour {

        #region Auxiliar/internal structs, enums and classes

        [System.Serializable]
        public struct HeadAnimationAttributes {
            [SerializeField]
            public Transform _headTransformGameObject;
            [SerializeField]
            // FIXME: creio que o "floor" não seja um único gameobject, portanto é interessante
            //        definir os terrenos com um Layer específico para que utilize o raycast
            //        neste layer.
            public Transform _floorGameObject;
            [SerializeField]
            [Range(0.0f, 1.0f)]
            public float _dampRotation;
            [Header("One side angle limitation")]
            [SerializeField]
            public float _maxAngleRotation;
        }

        #endregion

        #region private variables

        public UserInput userInput;
        [SerializeField]
        HeadAnimationAttributes headAnimationAttributes;

        #endregion

        #region Unity Events
    	
        private void Awake() {
            userInput = GetComponent<UserInput>();
        }

    	private void Update () {
            LookAtRayPosition();
    	}

        #endregion

        #region private methods

        private bool IsVariablesAssigned() {
            return 
                (headAnimationAttributes._headTransformGameObject != null) &&
                (headAnimationAttributes._floorGameObject != null);
        }

        private void LookAtRayPosition() {
            if (!IsVariablesAssigned()) {
                Debug.LogError("PlayerHeadAnimation - Did you forget to assign the variables?");
                return;
            }

            Ray mouseRay = userInput.getMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit)) {
                // We need the main body to be a reference to get the angle that the head will be looking at
                Quaternion qFrom = transform.rotation;
                // TODO: we might use hit.point if it will look up or down on the scene
                Vector3 pointPos = new Vector3(
                    hit.point.x,
                    headAnimationAttributes._headTransformGameObject.position.y,
                    hit.point.z);
                Quaternion qTo = Quaternion.LookRotation(pointPos - headAnimationAttributes._headTransformGameObject.position);

                float mouseAngleDiff = Quaternion.Angle(qFrom, qTo);

                if (mouseAngleDiff <= headAnimationAttributes._maxAngleRotation) {
                    headAnimationAttributes._headTransformGameObject.DOLookAt(
                        hit.point,
                        headAnimationAttributes._dampRotation,
                        AxisConstraint.Y,
                        Vector3.up);
                } else {
                    headAnimationAttributes._headTransformGameObject.DOLookAt(
                        transform.forward,
                        headAnimationAttributes._dampRotation,
                        AxisConstraint.Y,
                        Vector3.up);
                }
            }

        }

        #endregion
    }
}