# first-person-pton-game
A short first-person perspective game focused around basic 3D movement and environment interactions. Designed to teach basics of Unity for COS Council Intro Workshop.

This project allows for both rigidbody and translation-based movement, to help explain the differences between the two.

The player consists of a parent GameObject with several script components. All of the inputs are processed through Unity's Input System
(which enables easy remapping and less peripheral dependence), and the main state variables (whether the player is grounded, the movement speed, etc.) are 
held within a PlayerInputManager component. Movement and camera look are handled by accessing the fields stored within the PlayerInputManager, to compartmentalize
different actions.
