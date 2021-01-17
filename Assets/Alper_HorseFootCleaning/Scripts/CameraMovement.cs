using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Alper_HorseFootCleaning.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        private Camera cam;
        private bool isZoom = true;
        public Animator anim;
        private bool startZoom;
        private bool sceneChanged;
    
        private void Awake()
        {
            cam = Camera.main;
        }
    
        private void OnEnable()
        {
            GameManager.OnGameStart += StartZooming;
        }
        private void OnDisable()
        {
            GameManager.OnGameStart -= StartZooming;
        }
    
        private void Update()
        {
            if (isZoom && startZoom)
            {
                cam.orthographicSize -= .03f;
            }
            
            if (cam.orthographicSize < 3f)
            {
                
                StartCoroutine(ChangeAngle());
            }
    
            
        }
    
        IEnumerator ChangeAngle()
        {
            isZoom = false;
            startZoom = false;
            ChangeCameraAngle();
            yield break;
        }
        
        void ChangeCameraAngle()
        {
            transform.position = new Vector3(0, 1, .26f);
            transform.rotation = Quaternion.Euler(new Vector3(68,-180,0));
            Camera.main.orthographicSize = .3f;
            if (!anim) return;
            anim.SetTrigger("Fade");
            Destroy(anim,0.5f);
        }
    
        void StartZooming()
        {
            startZoom = true;
        }
    }
}

