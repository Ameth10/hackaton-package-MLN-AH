using UnityEngine;

namespace MLNFeedbacks.Demo
{
    /// <summary>
    /// Cible de la démo : quand on clique dessus, elle rapporte un point
    /// et lance son FeedbackPlayer. L'objet doit avoir un Collider.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ClickTarget : MonoBehaviour
    {
        [Tooltip("Le FeedbackPlayer à lancer. Vide = celui de cet objet.")]
        public FeedbackPlayer player;

        void Awake()
        {
            if (player == null)
                player = GetComponent<FeedbackPlayer>();
        }

        void Update()
        {
            if (!DemoInput.ClickedThisFrame(out Vector2 mousePosition))
                return;

            Camera camera = Camera.main;
            if (camera == null)
                return;

            Ray ray = camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                DemoController.AddPoint();
                if (player != null)
                    player.Play();   // la seule ligne qui utilise le package
            }
        }
    }
}
