using UnityEngine;
using System.Collections;
using Game.Entity.Unit;
using DG.Tweening;

namespace Game.Entity.Unit.Animation {
  [RequireComponent(typeof(UserInput))]
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
      [Header("Leave 0 to don't move")]
      [SerializeField]
      public float rotateBodyAfterLookOn;
    }

    #endregion

    #region private variables

    private UserInput _userInput;
    private float _lastDeltaTime = 0f;
    [SerializeField]
    private HeadAnimationAttributes _headAnimationAttributes;
    private Tweener _myTweener;

    #endregion

    #region Unity Events

    private void Awake() {
      _userInput = GetComponent<UserInput>();
    }

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

      Ray mouseRay = _userInput.getMouseRay();
      RaycastHit hit;

      // the rotation is still under way.
      if (_myTweener != null && _myTweener.IsPlaying())
        return;

      if (Physics.Raycast(mouseRay, out hit, 1000f, _headAnimationAttributes.lookAtLayerObjects)) {
        // We need the main body to be a reference to get the angle that the head will be looking at
        // TODO: we might use hit.point if it will look up or down on the scene
        Vector3 pointPos = new Vector3(
            hit.point.x,
            _headAnimationAttributes.headTransformGameObject.position.y,
            hit.point.z);
        Quaternion qTo = Quaternion.LookRotation(pointPos - _headAnimationAttributes.headTransformGameObject.position);

        float mouseAngleDiff = Quaternion.Angle(transform.rotation, qTo);

        if (mouseAngleDiff <= _headAnimationAttributes.maxAngleRotation) {
          // look at objects within the _maxAngleRotation
          LookAt(hit.point);
        } else {
          // look at the maximum angle define at _maxAngleRotation
          Vector3 tPos = transform.position;
          HeadSide hs = AngleDir(tPos + transform.forward, pointPos, tPos + transform.up);
          if (hs == HeadSide.Left) {
            Debug.Log("Left");
            _myTweener = _headAnimationAttributes.headTransformGameObject.DORotate(
                new Vector3(0, -_headAnimationAttributes.maxAngleRotation, 0),
                _headAnimationAttributes.dampRotation).OnComplete(() => CheckandRotateBody(pointPos));

          } else if (hs == HeadSide.Right) {
            Debug.Log("Right");
            _myTweener = _headAnimationAttributes.headTransformGameObject.DORotate(
                new Vector3(0, _headAnimationAttributes.maxAngleRotation, 0),
                _headAnimationAttributes.dampRotation).OnComplete(() => CheckandRotateBody(pointPos)); ;
          } else
            Debug.LogError("PlayerHeadAnimation - something wrong with the look at angle code!");
        }

        /* else 
            LookAt(transform.forward);*/
      } else {
        LookAt(transform.position + transform.forward);
      }

    }

    /// <summary>
    /// Checkands the rotate body.
    /// </summary>
    /// <param name="lookingPoint">Looking point.</param>
    private void CheckandRotateBody(Vector3 lookingPoint) {
      if (_headAnimationAttributes.rotateBodyAfterLookOn > 0) {
        if (_lastDeltaTime == 0f) {
          _lastDeltaTime = Time.deltaTime;
        } else {
          if (_lastDeltaTime >= _headAnimationAttributes.rotateBodyAfterLookOn) {
            LookAt(transform, lookingPoint, false);
            _lastDeltaTime = 0f;
          } else
            _lastDeltaTime += Time.deltaTime;
        }
      } else {
        _lastDeltaTime = 0f;
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
            Vector3.up).OnComplete(() => CheckandRotateBody(point));
      } else
        body.DOLookAt(
            point,
            _headAnimationAttributes.dampRotation,
            AxisConstraint.Y,
            Vector3.up).OnComplete(() => CheckandRotateBody(point));
    }

    #endregion
  }
}