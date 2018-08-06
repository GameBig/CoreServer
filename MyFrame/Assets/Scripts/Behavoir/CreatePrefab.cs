using UnityEngine;
namespace Client
{
    public static class CreatePrefab 
    {
        private static GameObject prefab;
        private static GameObject Get()
        {
            if (prefab == null)
                prefab = Resources.Load<GameObject>("Cycle");
            return prefab;
        }
        public static GameObject Create()
        {
            return Object.Instantiate(Get());
        }
    }
}

