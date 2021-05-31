using System;
using System.Collections.Generic;

namespace PandaSharp.Framework.Services.Aspect
{
    public interface IRequestParameterAspectFactory
    {
        IList<IRequestParameterAspect> GetParameterAspects(Type type);
    }
}