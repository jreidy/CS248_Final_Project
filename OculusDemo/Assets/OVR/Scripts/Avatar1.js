#pragma strict

function Start () {

}

function Update () {
 if (Input.GetKey("w")) {
 animation["walk"].speed = 3.0;
 	animation.Play("walk");
 	} else {
    animation.Stop("walk");
    }
}