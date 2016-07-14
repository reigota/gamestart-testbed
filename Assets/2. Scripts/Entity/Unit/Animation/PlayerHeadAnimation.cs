using UnityEngine;
using System;

namespace Game.Entity.Unit.Animation {
  [RequireComponent(typeof(Animator))]
  public class PlayerHeadAnimation : MonoBehaviour {

    [Serializable]
    public struct MinMax {
      [Range(-5f, 5f)]
      public float Min;
      [Range(-5f, 5f)]
      public float Max;
    }

    #region private variables

    public LayerMask lookAtLayerObjects;
    public Transform headRigTransform;
    public Transform target;
    [Range(0f, 1f)]
    public float w;
    // b.x = min value
    // b.y = max value
    public MinMax b;
    [Range(0f, 1f)]
    public float h;
    [Header("Move Constraints")]
    [SerializeField]
    public MinMax bodyMoveMapAtHeadZ;

    private Animator _anim;
    private float _b;
    private Vector3 _LookAtGoalPosition;

    private GameObject _forwardLookAtGO;

    #endregion

    #region Unity Events 

    private void Awake() {
      _anim = GetComponent<Animator>();
      _b = b.Min;
      _LookAtGoalPosition = transform.forward;

      // creates an GameObject to look forward smoothly when the mouse is not over an object.
      _forwardLookAtGO = new GameObject("Head Looking Forward");
      _forwardLookAtGO.transform.SetParent(transform);
      _forwardLookAtGO.transform.position = headRigTransform.position + (Vector3.forward * 2);
      _forwardLookAtGO.transform.rotation = Quaternion.identity;
    }

    public void OnAnimatorIK(int layerIndex) {
      target = GetLookingObject();

      Vector3 ITP = transform.InverseTransformPoint(target.position);
      if (ITP.z <= bodyMoveMapAtHeadZ.Min) {
        _b = Remap(ITP.z, bodyMoveMapAtHeadZ.Min, bodyMoveMapAtHeadZ.Max, b.Min, b.Max);
      } else
        _b = b.Min;

      _LookAtGoalPosition = CalcGoalPosition();
      _anim.SetLookAtPosition(_LookAtGoalPosition);
      _anim.SetLookAtWeight(w, _b, h, 1f);
    }

    #endregion

    #region private methods

    private Vector3 CalcGoalPosition() {
      Vector3 tmp = Vector3.Lerp(_LookAtGoalPosition, target.position, 0.05f);
      return tmp;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2) {
      return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private Transform GetLookingObject() {
      Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(mouseRay, out hit, 1000f, lookAtLayerObjects))
        return hit.transform;

      return _forwardLookAtGO.transform;
    }

    #endregion
  }
}