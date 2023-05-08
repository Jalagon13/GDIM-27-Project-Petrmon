using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer keyHiden;

        protected bool isAbleToInteract = true;

        public bool canInteract
        {
            get{
                return isAbleToInteract;
            }
        }

        protected virtual void Awake()
        {
            showKey();
        }

        public virtual void Interact()
        {
            keyHiden.color = new Color(.5f, .5f, .5f, 1f);

        }


        public virtual void showKey()
        {
            keyHiden.gameObject.SetActive(true);
            keyHiden.color = new Color(1f, 1f, 1f, 1f);

        }

        public virtual void hideKey()
        {
            keyHiden.color = new Color(1f, 1f, 1f, 1f);
            keyHiden.gameObject.SetActive(false);
        }


    }
}

