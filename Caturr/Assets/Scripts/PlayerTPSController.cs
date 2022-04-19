using UnityEngine;
using UnityEngine.Events;

public class PlayerTPSController : MonoBehaviour
{
    public Camera cam;
    public UnityEvent onInteractionInput;
    private InputData input;
    private CharacterAnimBasedMovement characterMovement;

    public bool onInteractionZone { get; set; }

    void Start()
    {
        characterMovement = GetComponent<CharacterAnimBasedMovement>();
    }

    void Update()
    {
        //Get input from player
        input.getInput();

        // Use jump as action button only if the character is inside an InteractionZone
        if (onInteractionZone && input.jump)
        {
            onInteractionInput.Invoke();
        }
        else
        //Apply input to character
        characterMovement.moveCharacter(input.hMovement, input.vMovement, cam, input.jump, input.dash);
    }
}