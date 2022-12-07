# Joysticks

On screen joystick that uses New Input System control paths to send it's direction. 

> __WARNING__
>
> Don't enable/disable the joystick at runtime, this triggers device initialization in new input system which can cause frame drops.
> Instead, use `Joystick.Interactable`.

## Usage

1. Drag and drop the joystick prefab in your scene.
2. Create an `InputActions` assets and bind your controls to the same control path as the joystick.
