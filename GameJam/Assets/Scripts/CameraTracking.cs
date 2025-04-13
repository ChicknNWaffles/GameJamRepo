using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour{

    public GameObject AttachedToCamera = null;
    private GameObject player;
    private Vector3 playerPos;
    private int LevelWidth;
    private bool panRight;
    private bool panLeft;

    public enum Section { 
        Left,
        Mid,
        Right,
    }

    // Start is called before the first frame update
    void Start(){
        player = Game.Instance.player;
        LevelWidth = Game.Instance.LevelWidth;
    }

    // Update is called once per frame
    void Update(){
        playerPos = player.transform.position;
        Section PSec = CheckPlayerSection();
        Section CExt = CheckCameraExtreme();
        CheckPanDirectionAbility(CExt, PSec);
        Section Dir = CheckHypotheticalPanDirction();
        Pan(Dir);
    }

    // checks to see which third of the screen
    // the player is in
    Section CheckPlayerSection() { 
        if (playerPos.x < transform.position.x - 3) {
            return Section.Left;
        }else if (playerPos.x > transform.position.x + 3) {
            return Section.Right;
        } else{
            return Section.Mid;
        }
    }

    // checks if the camera is at either extreeme left or right
    // mid means neighter extreme
    Section CheckCameraExtreme() { 
        if (transform.position.x <= 0) {
            return Section.Left;
        }else if (transform.position.x >= LevelWidth) {
            return Section.Right;
        }else {
            return Section.Mid;
        }
    }

    // checks which direction it should be allowed to
    // pan in, mid means both
    void CheckPanDirectionAbility(Section CExt, Section PSec){
        panLeft = true;
        panRight = true;
        if (CExt == Section.Left) { 
            panLeft = false;
            if (PSec == Section.Left) {
                panRight = false;
            }
        }else if (CExt == Section.Right) {
            panRight = false;
            if (PSec == Section.Right) {
                panLeft = false;
            }
        }else {
            panLeft = true;
            panRight = true;
        }
    }

    // Checks which direction the camera would pan in
    // if we do allow it to pan
    Section CheckHypotheticalPanDirction() {
        float Cx = transform.position.x;
        float Px = playerPos.x;
        if(Px < Cx) {
            return Section.Left;
        }else if (Px > Cx) {
            return Section.Right;
        }else {
            return Section.Mid;
        }
    }

    void Pan(Section Dir) { 
        if ((Dir == Section.Left && panLeft) || (Dir == Section.Right && panRight)) {
            float x = playerPos.x;
            float y = transform.position.y;
            float z = transform.position.z;
            Vector3 newPos = new(x, y, z);
            transform.position = newPos;
            if (AttachedToCamera != null) { 
                AttachedToCamera.transform.position = newPos;
            }
        }
    }
}
