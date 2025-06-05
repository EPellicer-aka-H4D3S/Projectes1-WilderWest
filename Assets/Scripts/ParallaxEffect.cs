using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Applies a parallax effect to a GameObject based on camera movement.
    /// </summary>
    public class ParallaxEffect : MonoBehaviour
    {
        /// <summary>
        /// Defines how a coordinate axis behaves during parallax movement
        /// </summary>
        
        [Tooltip("Parallax factor for Y axis between -1 and 2. Negative values make objects move in the opposite direction of the camera, positive values make objects follow the camera in a proportion. 0 means the object stays still in that axis.")]
        [Range(-1f, 2f)]
        [SerializeField] private float parallaxFactorX = 0f;

        [Tooltip("Parallax factor for Y axis between -1 and 2. Negative values make objects move in the opposite direction of the camera, positive values make objects follow the camera in a proportion. 0 means the object stays still in that axis.")]
        [Range(-1f, 2f)]
        [SerializeField] private float parallaxFactorY = 0f;
        
        [Tooltip("If true, the object will scroll infinitely on the X axis.")]
        [SerializeField] private bool infiniteScrollingX = false;
        
        [Tooltip("If true, the initial position is set to the position in the inspector. If false, the initial position is calculated based on parallax factors and camera position.")]
        [SerializeField] private bool useDesignTimePosition = true;

        [SerializeField] private GameObject objectToFollow;

        private Camera mainCamera;
        private Vector3 lastObjectToFollowPosition;
        private float textureUnitSizeX;
        private float textureUnitSizeY;
        private Vector3 designTimePosition; // Position set in the inspector
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            // Initialize components
            spriteRenderer = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main;
            
            // Check if the main camera is assigned
            if (!objectToFollow)
            {
                Debug.LogError("No main camera found. Please tag a camera as MainCamera.");
                enabled = false;
                return;
            }
            
            // Calculate the initial position based on parallax factors
            if (useDesignTimePosition)
            {
                // Store the position set in the inspector
                designTimePosition = transform.position;
                
                // Calculate the initial position based on parallax factors
                CalculateInitialPosition();
            }
            
            lastObjectToFollowPosition = objectToFollow.transform.position;

            // Calculate the size of the sprite if available
            if (spriteRenderer && spriteRenderer.sprite)
            {
                Sprite sprite = spriteRenderer.sprite;
                textureUnitSizeX = spriteRenderer.size.x * transform.localScale.x;
                textureUnitSizeY = spriteRenderer.size.y * transform.localScale.y;
            }
            else
            {
                // Default size if no sprite is available
                textureUnitSizeX = 1f;
                textureUnitSizeY = 1f;
            }
        }
        
        /// <summary>
        /// Calculates the initial position of the object so that when the camera is at its starting position,
        /// the object will be at the position set in the inspector.
        /// </summary>
        private void CalculateInitialPosition()
        {
            if (!objectToFollow) return;

            // get camera proportions in world space
            // float camHorizontalExtent = mainCamera.orthographicSize * Screen.width / Screen.height;
            // float camVerticalExtent = mainCamera.orthographicSize;
            
            // Calculate camera movement delta
            Vector3 deltaMovement = objectToFollow.transform.position - designTimePosition;
            Vector3 newPosition = designTimePosition;
            
            // Calculate parallax effect for X
            newPosition.x += deltaMovement.x * parallaxFactorX;
            
            // Calculate parallax effect for Y
            newPosition.y += deltaMovement.y * parallaxFactorY;
            
            // Apply the new position
            transform.position = newPosition;
        }

        private void LateUpdate()
        {
            HandleParallaxEffect();
        }
        
        private void HandleParallaxEffect()
        {
            if (!objectToFollow) return;

            // Calculate camera movement delta
            Vector3 deltaMovement = objectToFollow.transform.position - lastObjectToFollowPosition;
            Vector3 newPosition = transform.position;
            
            // Calculate parallax effect for X
            newPosition.x += deltaMovement.x * parallaxFactorX;
            
            // Calculate parallax effect for Y
            newPosition.y += deltaMovement.y * parallaxFactorY;

            // Apply the new position
            transform.position = newPosition;

            // Handle infinite scrolling for background elements
            if (infiniteScrollingX)
                HandleInfiniteHorizontalScrolling();

            // Update the last camera position
            lastObjectToFollowPosition = objectToFollow.transform.position;
        }

        private void HandleInfiniteHorizontalScrolling()
        {
            // Only apply if we have a sprite renderer
            if (!spriteRenderer) return;

            float camHorizontalExtent = mainCamera.orthographicSize * Screen.width / Screen.height;

            // Calculate the distance between the camera and the object on the X axis
            float offsetPositionX = mainCamera.transform.position.x - transform.position.x;

            // If the object is too far to the left, move it to the right
            if (offsetPositionX >= camHorizontalExtent + textureUnitSizeX / 2)
            {
                transform.position = new Vector3(
                    mainCamera.transform.position.x + textureUnitSizeX,
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }
}