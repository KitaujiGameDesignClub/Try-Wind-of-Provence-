using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PureAmaya.General
{
    /// <summary>
    /// ���UpdateЧ��
    /// </summary>
    [DisallowMultipleComponent]
    public class UpdateManager : MonoBehaviour
    {

        public static UpdateManager updateManager;

        public class UpdateEventClass : UnityEvent { }

        /// <summary>
        /// Start֮��,Update֮ǰ����һ��
        /// </summary>
        public UpdateEventClass LateStart = new();
        bool LateStartRun = false;

        public UpdateEventClass FastUpdate = new();
        /// <summary>
        /// �ٵ�LateUpdate������FastUpdateִ�к���LateUpdateִ��ǰִ�У�
        /// </summary>
        public UpdateEventClass FakeLateUpdate = new();
        /// <summary>
        /// ������MEC�ĵ���Update
        /// </summary>
        public UpdateEventClass SlowUpdate = new();
        private void Awake()
        {
            updateManager = this;

            FastUpdate.RemoveAllListeners();
            FakeLateUpdate.RemoveAllListeners();
            SlowUpdate.RemoveAllListeners();
        }

        private void Start()
        {
            // DontDestroyOnLoad(gameObject);
            StartCoroutine(SlowUpdatea());

            LateStartRun = true;
        }

        private void Update()
        {
            //���˸�if�ж�����������Ĵ�����˵�����ܵ���ʧӦ�ò���̫��
            if (LateStartRun)
            {
                LateStart.Invoke();
                LateStartRun = false;
            }

            FastUpdate.Invoke();

            FakeLateUpdate.Invoke();
        }

        private IEnumerator<float> SlowUpdatea()
        {
            while (true)
            {
                SlowUpdate.Invoke();
                yield return 0f;
            }
        }
    }

}