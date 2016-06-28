using UnityEngine;
using DG.Tweening;

namespace Game.Entity.Unit.Animation {
  public class PlayerHeadAnimation : MonoBehaviour {

    #region Auxiliar/internal structs, enums and classes

    public enum HeadSide {
      Forward,
      Left,
      Right
    }

    [System.Serializable]
    public struct HeadAnimationAttributes {
      [SerializeField]
      public Transform headTransformGameObject;
      [SerializeField]
      [Range(0.0f, 1.0f)]
      public float dampRotation;
      [SerializeField]
      public LayerMask lookAtLayerObjects;
      [Header("One side angle limitation")]
      [SerializeField]
      public float maxAngleRotation;
    }

    #endregion

    #region private variables
    
    private float _lastDeltaTime = 0f;
    [SerializeField]
    private HeadAnimationAttributes _headAnimationAttributes;
    private Tweener _myTweener;
    private float _rotateBodyAfterLookOn;

    #endregion

    #region Unity Events 

    private void Update() {
      LookAtRayPosition();
    }

    #endregion

    #region private methods

    private bool IsVariablesAssigned() {
      return
          (_headAnimationAttributes.headTransformGameObject != null);
    }

    private void LookAtRayPosition() {
      if (!IsVariablesAssigned()) {
        Debug.LogError("PlayerHeadAnimation - Did you forget to assign the variables?");
        return;
      }

      Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      // the rotation is still under way.
      if (_myTweener != null && _myTweener.IsPlaying())
        return;

      if (Physics.Raycast(mouseRay, out hit, 1000f, _headAnimationAttributes.lookAtLayerObjects)) {
        Quaternion qTo = Quaternion.LookRotation(hit.point - _headAnimationAttributes.headTransformGameObject.position);

        float mouseAngleDiff = Quaternion.Angle(transform.rotation, qTo);

        if (mouseAngleDiff <= _headAnimationAttributes.maxAngleRotation) {
          // look at objects within the _maxAngleRotation
          LookAt(hit.point);
        } else {
          // look at the maximum angle define at _maxAngleRotation
          Vector3 tPos = transform.position;
          HeadSide hs = AngleDir(tPos + transform.forward, hit.point, tPos + transform.up);
          if (hs == HeadSide.Left) {
            Debug.Log("Left");
            _myTweener = _headAnimationAttributes.headTransformGameObject.DORotate(
                new Vector3(hit.point.x, -_headAnimationAttributes.maxAngleRotation, hit.point.z),
                _headAnimationAttributes.dampRotation);

          } else if (hs == HeadSide.Right) {
            Debug.Log("Right");
            _myTweener = _headAnimationAttributes.headTransformGameObject.DORotate(
                new Vector3(hit.point.x, _headAnimationAttributes.maxAngleRotation, hit.point.z),
                _headAnimationAttributes.dampRotation);
          } else
            Debug.LogError("PlayerHeadAnimation - something wrong with the look at angle code!");
        }

        /* else 
            LookAt(transform.forward);*/
      } else {
        LookAt(transform.position + transform.forward);
      }

    }

    private HeadSide AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
      Vector3 perp = Vector3.Cross(fwd, targetDir);
      float dir = Vector3.Dot(perp, up);

      if (dir > 0f) {
        return HeadSide.Right;
      } else if (dir < 0f) {
        return HeadSide.Left;
      } else {
        return HeadSide.Forward;
      }
    }

    /// <summary>
    /// Looks at point and assignTweener.
    /// </summary>
    /// <param name="point">Point.</param>
    /// <param name="assignTweener">If set to <c>true</c> assign tweener.</param>
    private void LookAt(Vector3 point, bool assignTweener = true) {
      LookAt(_headAnimationAttributes.headTransformGameObject, point, assignTweener);
    }

    /// <summary>
    /// makes the body Looks at point and assignTweener.
    /// </summary>
    /// <param name="body">Body.</param>
    /// <param name="point">Point.</param>
    /// <param name="assignTweener">If set to <c>true</c> assign tweener.</param>
    private void LookAt(Transform body, Vector3 point, bool assignTweener = true) {
      Debug.Log("point: " + point);
      if (assignTweener) {
        _myTweener = body.DOLookAt(
            point,
            _headAnimationAttributes.dampRotation,
            AxisConstraint.Y,
            Vector3.up);
      } else
        body.DOLookAt(
            point,
            _headAnimationAttributes.dampRotation,
            AxisConstraint.Y,
            Vector3.up);
    }

    #endregion
  }
}