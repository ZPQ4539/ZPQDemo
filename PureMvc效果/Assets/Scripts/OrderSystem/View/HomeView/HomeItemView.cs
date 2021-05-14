using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OrderSystem
{
    public class HomeItemView : MonoBehaviour
    {
        // Start is called before the first frame update
        private Text text = null;
        private Image image = null;
        private HomeItem homeItem = null;
        public IList<Action<object>> actionList = new List<Action<object>>();


        private void Awake()
        {
            text = transform.Find("Id").GetComponent<Text>();
            image = transform.GetComponent<Image>();
        }

        public void InitHome(HomeItem home)
        {
            this.homeItem = home;
            UpdateState();
        }

        public void UpdateState()
        {
            if (homeItem == null)
            {
                return;
            }
            Color color = Color.white;
            if (this.homeItem.state.Equals(0))
            {
                color = Color.green;
            }
            else if (this.homeItem.state.Equals(1))
            {
                color = Color.yellow;
                StartCoroutine(StopBack(20f));
            }
            else if (this.homeItem.state.Equals(2))
            {
                color = Color.red;
                StartCoroutine(StopBack());
            }

            Debug.Log(homeItem.ToString());
            image.color = color;
            text.text = homeItem.ToString();
        }

        IEnumerator StopBack(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[homeItem.state].Invoke(homeItem);
        }


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

