using System;

namespace Ax.Engine.ECS
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class UniqueComponentAttribute : Attribute { }
}
