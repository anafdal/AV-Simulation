# VR-PAVIB: The Virtual Reality Pedestrian-Autonomous Vehicle Interactions Benchmark Suite

## Table of contents
* [General info](#general-info)
* [Setup](#setup)
* [Technologies](#technologies)


## General info
Autonomous vehicles (AV) is a merging theme for the advent era of future transportation.  In design, AVs are able to effectively assist humans in everyday tasks. However, one challenge prevents pedestrian-AV interactions research is that there are no public and standardized benchmarks to investigate different manners of interactions to communicate AVs’awareness and intent to pedestrians. To address the existing limitation, this proposed work aims to include general interactive interfaces on AV and introduce the VR-PAVIB benchmark for researchers to investigate the impact of different interface features on pedestrians’ perception of the AV behavior on the road.


## Setup
This is a first person view experience. The user can move using the usual a, w, s, d keyboard inputs.
The traffic logic depends upon what scenarios the vehicle is present in. A Behaviour Tree is utilized for decision making as it is seen in the algorithm below. There will be different behaviours for different situations:<br />
When the vehicle stops before a stop sign, it will only stop for a certain period of time specified by the user, unless there is a person walking on the crossing street which will cause the vehicle to only wait until the road is empty again.<br />
Stopping in front of a stoplight, will also cause the vehicle to behave in different ways as it depends on a different set of factors like which light is activated and if someone is passing through the crossroad. If a user is in the crossroad, the vehicle will stop no matter what, until the crossroad is empty again. If the light is red, the vehicle will stop and start again when it turns green. If the vehicle is about to turn right, in both red and green lights, it will check both streets before it turns.
<br />
 The vehicle is able to differentiate between these different scenarios by using raycasting. Through raycasting, the vehicle is informed on what object is ahead of it and what behaviour it should perform. The Navigation Mesh System is also utilized to guide the vehicles in their path and determine where they need to go next. In the end the project involves two main scenarios: user walking through a crossroad with a stop sign, and user walking through a stoplight intersection.
 
## Technologies 
Project is created with:
* Unity: 2019.2.2f1


So far this is an early prototype and work is still being done to develop the project further.
