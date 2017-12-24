﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EudiFramework
{
    [DefaultExecutionOrder(-9998)]
    public class EudiGameObjectManager : MonoBehaviour, IEudiGameObjectManager
    {
        public List<EudiComponentBehaviour> ComponentsList = new List<EudiComponentBehaviour>();
        private List<EudiComponentBehaviour> m_ComponentsWithFixedUpdate = new List<EudiComponentBehaviour>();
        private List<EudiComponentBehaviour> m_ComponentsWithUpdate = new List<EudiComponentBehaviour>();
        private List<EudiComponentBehaviour> m_ComponentsWithLateUpdate = new List<EudiComponentBehaviour>();

        public void OnNewEudiComponent(EudiComponentBehaviour component)
        {
            ComponentsList.Add(component);

            if (ReflectionUtility.IsOverride(component, "UnityFixedUpdate"))
                m_ComponentsWithFixedUpdate.Add(component);
            if (ReflectionUtility.IsOverride(component, "UnityUpdate"))
                m_ComponentsWithUpdate.Add(component);
            if (ReflectionUtility.IsOverride(component, "UnityLateUpdate"))
                m_ComponentsWithLateUpdate.Add(component);
        }

        public void OnRemoveEudiComponent(EudiComponentBehaviour component)
        {
            ComponentsList.Remove(component);
        }

        public void FixedUpdate()
        {
            var listCount = m_ComponentsWithFixedUpdate.Count;
            for (int i = 0; i < listCount; i++)
            {
                var item = m_ComponentsWithFixedUpdate[i];
                item._DoFixedUpdate();
            }
        }

        public void Update()
        {
            var listCount = m_ComponentsWithUpdate.Count;
            for (int i = 0; i < listCount; i++)
            {
                var item = m_ComponentsWithUpdate[i];
                item._DoUpdate();
            }
        }

        public void LateUpdate()
        {
            var listCount = m_ComponentsWithLateUpdate.Count;
            for (int i = 0; i < listCount; i++)
            {
                var item = m_ComponentsWithLateUpdate[i];
                item._DoLateUpdate();
            }
        }

        public void OnDestroy()
        {
            
        }
    }
}
