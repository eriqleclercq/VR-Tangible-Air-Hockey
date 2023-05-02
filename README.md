# VR-Tangible-Air-Hockey


This game is part of my disseration at the University of Bath, it is a virtual game of air hockey in VR that uses tangible haptics that can be used instead of traditional VR controllers. The game was built in Unity (2021.3.18f1) and Python 3.11.2.

## Dependencies:
### Python
OpenCV\
CVZone

### Unity
Oculus XR Plugin\
OpenXR Plugin\
XR Interaction Toolkit\
TextMeshPro

## How to run:
Should you wish to run this project you will need the following:
  * Oculus VR headset that supports hand tracking
  * 2 webcams that support 60fps 1080p
  * 3D print of the mallet that is pink
  * Table

Once you have acquired these things then will need to do the following:
  1. Measure the height of your table (in metric)
  2. Set the Y position of the table in the Unity scene to the height of your table
  3. Set the webcams such that each of them can track one axis of the mallet's movement 
  4. Run the Python script
  5. Run the Unity project
