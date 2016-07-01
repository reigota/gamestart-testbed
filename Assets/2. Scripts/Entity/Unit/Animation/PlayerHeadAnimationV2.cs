using UnityEngine;

namespace Game.Entity.Unit.Animation {
  [RequireComponent(typeof(Animator))]
  public class PlayerHeadAnimationV2 : MonoBehaviour {
    [SerializeField]
    private Transform _headTransform;
    [SerializeField]
    private float _objectDistanceSee = 300f;
    [SerializeField]
    private LayerMask _objectLayerSee;

    private Animator anim;

    private void Start() {
      anim = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex) {
      if (_headTransform == null)
        return;

      Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(mouseRay, out hit, _objectDistanceSee, _objectLayerSee)) {
        Vector3 pos = Vector3.Lerp(_headTransform.position, hit.point, 0.5f);

        anim.SetLookAtPosition(pos);
        anim.SetLookAtWeight(0.5f);
      }
    }
  }
}