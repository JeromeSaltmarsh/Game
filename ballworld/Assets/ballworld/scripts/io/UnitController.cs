﻿using UnityEngine;

public class UnitController : MonoBehaviour {

    public static UnitController instance;
    public Unit unit;
    public Weapon sword;
    public Weapon bow;

    public KeyCode glanceLeft = KeyCode.LeftShift;
    public KeyCode glanceRight = KeyCode.Space;
    public KeyCode runKey = KeyCode.W;
    public KeyCode backKey = KeyCode.S;
    public KeyCode strafeLeftKey = KeyCode.A;
    public KeyCode strafeRightKey = KeyCode.D;
    public float jumpSpeed = 1f;
    public float sensitivityX = 4;

    public bool lockMouse = false;
    public bool lockMovement = false;
    public bool lockWeapons = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
    }

    void readWeapons()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sword.gameObject.SetActive(true);
            sword.draw();
            if (bow.gameObject.activeInHierarchy)
            {
                bow.idle();
                bow.gameObject.SetActive(false);
            }
        }
        if (Input.GetMouseButton(0))
        {
            sword.draw();
        }

        if (Input.GetMouseButtonUp(0))
        {
            sword.release();
        }
        if (Input.GetMouseButton(1))
        {
            if (bow.gameObject.activeInHierarchy)
            {
                bow.draw();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            bow.gameObject.SetActive(true);
            if (sword.gameObject.activeInHierarchy)
            {
                sword.idle();
                sword.gameObject.SetActive(false);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (bow.gameObject.activeInHierarchy)
            {
                bow.release();
            }
        }
    }

    void readUnitMovement()
    {
        if (Input.GetKey(runKey))
        {
            unit.unitState = UnitState.RUNNING_FORWARD;

            if (Input.GetKey(strafeLeftKey))
            {
                unit.unitState = UnitState.RUNNING_FORWARD_LEFT;
            }
            else
            if (Input.GetKey(strafeRightKey))
            {
                unit.unitState = UnitState.RUNNING_FORWARD_RIGHT;
            }
        }
        else
        if (Input.GetKey(backKey))
        {
            unit.unitState = UnitState.RUNNING_BACKWARD;

            if (Input.GetKey(strafeLeftKey))
            {
                unit.unitState = UnitState.RUNNING_BACKWARDS_LEFT;
            }
            else
            if (Input.GetKey(strafeRightKey))
            {
                unit.unitState = UnitState.RUNNING_BACKWARDS_RIGHT;
            }
        }
        else
        if (Input.GetKey(strafeLeftKey))
        {
            unit.unitState = UnitState.STRAFING_LEFT;
        }
        else
        if (Input.GetKey(strafeRightKey))
        {
            unit.unitState = UnitState.STRAFING_RIGHT;
        }
        else
        {
            unit.unitState = UnitState.IDLE;
        }
    }

    void readMouseAim()
    {
        float x = Input.GetAxis("Mouse X");
        Vector3 eulerAngles = unit.transform.rotation.eulerAngles;
        eulerAngles.z += x * sensitivityX;
        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = eulerAngles;
        unit.transform.rotation = rotation;
    }

    void readGlance()
    {
        if (Input.GetKey(glanceRight))
        {
            bow.rotation = 90f;
            sword.rotation = 90f;
        }
        else if (Input.GetKey(glanceLeft))
        {
            bow.rotation = -90f;
            sword.rotation = -90f;
        }
        else
        {
            bow.rotation = 0;
            sword.rotation = 0;
        }
    }

    void readCameraZoom()
    {
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= (Input.GetAxis("Mouse ScrollWheel") * 10);
    }

    void Update () {

        if (!lockMovement)
        {
            readUnitMovement();
        }
        if (!lockMouse)
        {
            readMouseAim();
        }
        readGlance();
        readCameraZoom();        
    }
}
