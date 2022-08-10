using System.Collections.Generic;
using UnityEngine.UIElements;

namespace VNEngine.Scripts.Editor.Service.Extensions
{
    public static class VisualElementExtensions
    {
        public static void GetAllChildrenOfType<TElementType>(this VisualElement element, ref HashSet<TElementType> result)
            where TElementType : VisualElement
        {
            var children = element.Children();
            foreach (var child in children)
            {
                if (child is TElementType c) result.Add(c);
                if (child.childCount == 0) continue;

                child.GetAllChildrenOfType(ref result);
            }
        }
    }
}